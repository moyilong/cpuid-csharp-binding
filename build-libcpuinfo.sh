#!/bin/bash

workdir=${WORKDIR:-$PWD/build}
install_dir=${INSTALL_DIR:-$workdir/../Dragon.CpuInfo/runtimes/}
toolchain_dir=$workdir/toolchain
build_dir=$workdir/build

source_dir=$workdir/cpuid
rid_dir=$workdir/rid

mkdir -p $toolchain_dir

if [ ! -e $source_dir ]; then
    git clone https://mirror.ghproxy.com/https://github.com/pytorch/cpuinfo $source_dir
    cd $source_dir
    git checkout 5de5c70fedc26e4477d14fdaad0e4eb5f354400b
    cd $OLDPWD
fi


cpuinfo_version="$(cd $source_dir && git rev-parse HEAD)"
echo $cpuinfo_version

filter_cpuinfo(){
    cat $source_dir/include/cpuinfo.h | \
        grep '(void)' | sed 's/(void)//g' | \
        grep -E '^static inline' | \
        awk '{print  "FUNCTION_COPY(" $(NF-1) "," $(NF-2) ");" }'

    cat $source_dir/include/cpuinfo.h | \
        grep '(void)' | sed 's/(void)//g' | tr -d ';' | \
        grep -E '^uint32_t CPUINFO_ABI' | \
        awk '{print  "FUNCTION_COPY(" $(3) "," $(1) ");" }'
}

filter_cpuinfo_chsarp(){
    filter_cpuinfo | sed 's/uint32_t/UInt32/g' | while read line; do 
        local funcname="binding_$(echo $line | awk -F ',' '{print $1}' | sed 's/FUNCTION_COPY(//g')"
        local funcret="$(echo $line | awk -F ',' '{print $2}' | sed 's/);//g')"
        cat - << EOF
        [DllImport(libName)]
        public static extern $funcret $funcname();
EOF
    done
}


cat ./binding.c > $workdir/binding.c
echo -e "\n\n\n" >> $workdir/binding.c
filter_cpuinfo >> $workdir/binding.c

cat - << EOF > $workdir/binding.cs
using System;
using System.Runtime.InteropServices;

namespace Dragon.CpuInfo.libCpuInfo
{
    public static partial class CpuInfoNative
    {
$(filter_cpuinfo_chsarp)
    }
}
EOF

CFLAGS="-static-libgcc -Os -fPIC -pipe"

build_instance() {
    local arch=$1
    local os=$2
    local toolchain=$3
    local rid=$4
    local local_build=$build_dir/$os/$arch
 
    local local_cflags=$CFLAGS

    if [ "$musl" == "true" ]; then
        local_cflags="-static $CFLAGS"
    fi
    
    mkdir -p $local_build

    cd $local_build 
    cmake \
        -DCMAKE_INSTALL_PREFIX=/usr \
        -DCMAKE_SYSTEM_NAME=$os \
        -DCMAKE_C_FLAGS="$local_cflags" \
        -DCMAKE_C_COMPILER=${toolchain}-gcc \
        -DCMAKE_SYSTEM_PROCESSOR=$arch \
        -DCPUINFO_LIBRARY_TYPE=static \
        -DCPUINFO_RUNTIME_TYPE=static \
        -DCPUINFO_BUILD_BENCHMARKS=OFF \
        -DCPUINFO_BUILD_UNIT_TESTS=OFF \
        -DCPUINFO_BUILD_MOCK_TESTS=OFF \
        $source_dir

    make -j
    mkdir -p $rid_dir/$rid/native/
    install_file="$(find $local_build -type f | grep -E 'so$|dll$|dylib$')"

    if [ "$os" == "Linux" ]; then
        libname=libcpuinfo-binding.so
    fi
    if [ "$os" == "Windows" ]; then
        libname=cpuinfo-binding.dll
    fi
    if [ "$os" == "osx" ]; then
        libname=libcpuinfo-binding.dylib
    fi

    local libpath=$rid_dir/$rid/native/$libname

    #cp $local_build/$liborigname $rid_dir/$rid/native/$libname
    
    ${toolchain}-gcc $local_cflags -shared -fPIC $workdir/binding.c -DCOMPILE_MODE \
        -DCPUINFO_VERSION="\"$cpuinfo_version\"" -DRID_NAME="\"$rid\""\
        -L$local_build -lcpuinfo_internals -lcpuinfo -I$source_dir/include \
        -o $rid_dir/$rid/native/$libname || exit -1

    chmod 777 $rid_dir/$rid/native/$libname
    ${toolchain}-objdump -x $libpath | grep NEEDED > $libpath.needed
    ldd $libpath >> $libpath.needed
    cp $workdir/binding.c $libpath.binding.c
}

rm -rfv $rid_dir

# https://learn.microsoft.com/zh-cn/dotnet/core/rid-catalog

if [ "$musl" == "true" ]; then
    build_instance x86_64 Linux x86_64-linux-musl linux-musl-x64

    build_instance aarch64 Linux aarch64-linux-musl linux-musl-arm64

    build_instance armv6 Linux arm-linux-musleabi linux-musl-arm

    #build_instance riscv32 Linux riscv32-linux-musl linux-musl-rv32

    build_instance riscv64 Linux riscv64-linux-musl linux-musl-rv64
else
    build_instance x86_64 Linux x86_64-linux-gnu linux-x64

    build_instance aarch64 Linux aarch64-linux-gnu linux-arm64

    build_instance armv6 Linux arm-linux-gnueabi linux-arm

    build_instance riscv64 Linux riscv64-linux-gnu linux-rv64

    build_instance i686 Windows i686-w64-mingw32 win-x86

    build_instance x86_64 Windows x86_64-w64-mingw32 win-x64
fi

mkdir -p $install_dir
cp -vr $rid_dir/* $install_dir
chmod 777 -R $install_dir