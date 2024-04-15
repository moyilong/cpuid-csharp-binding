#!/bin/bash

workdir=$PWD/build

build_dir=$workdir/build

source_dir=$build_dir/cpuid
rid_dir=$workdir/rid
if [ ! -e $source_dir ]; then
    git clone https://mirror.ghproxy.com/https://github.com/pytorch/cpuinfo $source_dir
fi

cat ./binding.c > build/binding.c
./binding-function.sh >> build/binding.c

cat - << EOF > build/binding.cs
using System;
using System.Runtime.InteropServices;

namespace libCpuInfo.Natives
{
    public static partial class CpuInfoNative
    {

EOF

./binding-function.sh | sed 's/uint32_t/UInt32/g' | while read line; do 
    funcname="binding_$(echo $line | awk -F ',' '{print $1}' | sed 's/FUNCTION_COPY(//g')"
    funcret="$(echo $line | awk -F ',' '{print $2}' | sed 's/);//g')"
cat - << EOF >> build/binding.cs
        [DllImport(libName,CallingConvention = callingConvention, CharSet = callingCharSet)]
        public static extern $funcret $funcname();
EOF
done

cat - << EOF >> build/binding.cs
    }
}
EOF

CFLAGS="-static-libgcc -O3 -fPIC"

build_instance() {
    local arch=$1
    local os=$2
    local toolchain=$3
    local rid=$4
 
    local_build=$build_dir/$os/$arch

    mkdir -p $local_build

    if [ "$os" == "Windows" ]; then
        local CFLAGS="-static $CFLAGS"
    fi

    cd $local_build 
    cmake --log-context \
        -DCPUINFO_LIBRARY_TYPE=static \
        -DCPUINFO_RUNTIME_TYPE=static \
        -DCMAKE_INSTALL_PREFIX=/usr \
        -DCPUINFO_BUILD_UNIT_TESTS=OFF \
        -DCPUINFO_BUILD_MOCK_TESTS=OFF \
        -DCMAKE_SYSTEM_NAME=$os \
        -DCMAKE_C_COMPILER=${toolchain}-gcc \
        -DCMAKE_SYSTEM_PROCESSOR=$arch \
        -DCPUINFO_BUILD_BENCHMARKS=off \
        -DCMAKE_C_FLAGS="$CFLAGS" \
        $source_dir

    make -j
    mkdir -p $rid_dir/$rid/native/
    install_file="$(find $local_build -type f | grep -E 'so$|dll$|dylib$')"
    echo "Install:$install_file"

    liborigname=libcpuinfo.so
    libname=libcpuinfo.so
    libbinding=libcpuinfo-binding.so
    libbind=libcpuinfo-bind
    if [ "$os" == "Windows" ]; then
        libname=cpuinfo.dll
        libbinding=cpuinfo-binding.dll
        liborigname=libcpuinfo.dll
        libbind=libcpuinfo-bind.exe
    fi

    if [ "$os" == "osx" ]; then
        libname=libcpuinfo.dylib
        libbinding=libcpuinfo-binding.dylib
        liborigname=libcpuinfo.dylib
    fi

    #cp $local_build/$liborigname $rid_dir/$rid/native/$libname

    ${toolchain}-gcc $CFLAGS \
        -shared -L$rid_dir/$rid/native -L$local_build -I$source_dir/include -fPIC \
        $workdir/binding.c -lcpuinfo \
        -o $rid_dir/$rid/native/$libbinding || exit -1

    #${toolchain}-gcc $CFLAGS \
    #    -L$local_build -L$local_build -I$source_dir/include -fPIC \
    #    $workdir/binding.c -lcpuinfo \
    #    -o $rid_dir/$rid/native/$libbind || exit -1
    #chmod a+x $rid_dir/$rid/native/$libbind
}
# https://learn.microsoft.com/zh-cn/dotnet/core/rid-catalog
# 
build_instance x86_64 Linux x86_64-linux-gnu linux-x64

# gcc-aarch64-linux-gnu
build_instance aarch64 Linux aarch64-linux-gnu linux-arm64

# gcc-arm-linux-gnueabi
build_instance armv6 Linux arm-linux-gnueabi linux-arm

# gcc-mingw-w64-x86-64
build_instance x86_64 Windows x86_64-w64-mingw32 win-x64

# gcc-mingw-w64-i686
build_instance i686 Windows i686-w64-mingw32 win-x86

rm -rfv $workdir/../libCpuId/runtimes/
mkdir -p $workdir/../libCpuId/runtimes/
cp -vr $rid_dir/* $workdir/../libCpuId/runtimes/