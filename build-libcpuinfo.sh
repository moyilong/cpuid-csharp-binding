#!/bin/bash

workdir=$PWD/build

build_dir=$workdir/build

source_dir=$workdir/cpuid
rid_dir=$workdir/rid

if [ ! -e $source_dir ]; then
    git clone https://mirror.ghproxy.com/https://github.com/pytorch/cpuinfo $source_dir
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


cat ./binding.c > build/binding.c
echo -e "\n\n\n" >> build/binding.c
filter_cpuinfo >> build/binding.c

cat - << EOF > build/binding.cs
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

CFLAGS="-static-libgcc -fPIC -pipe -O3"

build_instance() {
    local arch=$1
    local os=$2
    local toolchain=$3
    local rid=$4
 
    local_build=$build_dir/$os/$arch

    mkdir -p $local_build

    #if [ "$os" == "Windows" ]; then
    #    local CFLAGS="-static $CFLAGS"
    #fi

    cd $local_build 
    cmake \
        -DCMAKE_INSTALL_PREFIX=/usr \
        -DCMAKE_SYSTEM_NAME=$os \
        -DCMAKE_C_COMPILER=${toolchain}-gcc \
        -DCMAKE_SYSTEM_PROCESSOR=$arch \
        -DCMAKE_C_FLAGS="$CFLAGS" \
        -DCPUINFO_BUILD_BENCHMARKS=off \
        -DCPUINFO_LIBRARY_TYPE=static \
        -DCPUINFO_RUNTIME_TYPE=static \
        -DCPUINFO_BUILD_UNIT_TESTS=OFF \
        -DCPUINFO_BUILD_MOCK_TESTS=OFF \
        $source_dir

    make -j
    mkdir -p $rid_dir/$rid/native/
    install_file="$(find $local_build -type f | grep -E 'so$|dll$|dylib$')"
    echo "Install:$install_file"

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
    
    ${toolchain}-gcc $CFLAGS -shared -fPIC $workdir/binding.c -DCOMPILE_MODE \
        -DCPUINFO_VERSION="\"$cpuinfo_version\"" -DRID_NAME="\"$rid\""\
        -L$local_build -lcpuinfo_internals -lcpuinfo -I$source_dir/include \
        -o $rid_dir/$rid/native/$libname || exit -1

    chmod 777 $rid_dir/$rid/native/$libname
    ${toolchain}-nm $libpath > $libpath.nm
    ${toolchain}-objdump -x $libpath > $libpath.needed
}

rm -rfv $rid_dir

# https://learn.microsoft.com/zh-cn/dotnet/core/rid-catalog
# 
build_instance x86_64 Linux x86_64-linux-gnu linux-x64

# gcc-aarch64-linux-gnu
build_instance aarch64 Linux aarch64-linux-gnu linux-arm64

# gcc-arm-linux-gnueabi
build_instance armv6 Linux arm-linux-gnueabi linux-arm

# gcc-mingw-w64-i686
build_instance i686 Windows i686-w64-mingw32 win-x86

# gcc-mingw-w64-x86-64
build_instance x86_64 Windows x86_64-w64-mingw32 win-x64

# gcc-riscv64-linux-gnu
build_instance riscv64 Linux riscv64-linux-gnu linux-rv64



rm -rfv $workdir/../Dragon.CpuInfo/runtimes/
mkdir -p $workdir/../Dragon.CpuInfo/runtimes/
cp -vr $rid_dir/* $workdir/../Dragon.CpuInfo/runtimes/
chmod 777 -R $workdir/../Dragon.CpuInfo/runtimes/