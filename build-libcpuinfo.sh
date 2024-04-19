#!/bin/bash

workdir=${WORKDIR:-$PWD/build}
install_dir=${INSTALL_DIR:-$workdir/../Dragon.CpuInfo/runtimes/}
toolchain_dir=$workdir/toolchain
build_dir=$workdir/build

source_dir=${SOURCE_DIR:-$workdir/cpuid}
rid_dir=$workdir/rid

mkdir -p $toolchain_dir

if [ ! -e $source_dir/.git ]; then
    git clone https://mirror.ghproxy.com/https://github.com/pytorch/cpuinfo $source_dir
    
else
    cd $source_dir
    git pull
    cd $OLDPWD
fi


cpuinfo_version="$(cd $source_dir && git rev-parse HEAD)"
echo $cpuinfo_version


binding_source=$PWD/binding.c

CFLAGS="-Os -fPIC -pipe"

build_instance() {
    arch=$1
    os=$2
    toolchain=$3
    rid=$4
    local_build=$build_dir/$os/$arch
 
    local_cflags=$CFLAGS

    android_flags=""
    if [ "$os" == "Linux" ] || [ "$os" == "Android" ]; then
        libname=libcpuinfo-binding.so
        libexec_name=cpuinfo-binding
    fi
    if [ "$os" == "Windows" ]; then
        libname=cpuinfo-binding.dll
        libexec_name=cpuinfo-binding.exe
    fi
    if [ "$os" == "osx" ]; then
        libname=libcpuinfo-binding.dylib
        libexec_name=cpuinfo-binding
    fi
    libpath=$rid_dir/$rid/native/$libname
    libexec_path=$rid_dir/$rid/native/$libexec_name
    binding_definition="-DCOMPILE_MODE  -DRID_NAME=\"$rid\" -DCPUINFO_VERSION=\"$cpuinfo_version\""
    binding_link="-L$local_build -lcpuinfo"
    binding_flags="$CLFAGS -fPIC -I$source_dir/include $binding_source $binding_link $binding_definition"
    
    toolchain_compiler="notfound"
    android_abi
    android_level
    if [ "$android" == "true" ]; then
        export PATH=$PATH:$NDK_ROOT/toolchains/llvm/prebuilt/linux-x86_64/bin
        case $arch in 
            x86_64)
                android_abi="x86_64"
                toolchain_compiler="x86_64-linux-android21-clang"
            ;;
            x86)
                android_abi="x86"
                toolchain_compiler="i686-linux-android21-clang"
            ;;
            aarch64|arm64)
                android_abi="arm64-v8a"
                toolchain_compiler="aarch64-linux-android21-clang"
            ;;
            arm)
                android_abi="armeabi-v7a"
                toolchain_compiler="armv7a-linux-androideabi21-clang"
            ;;
        esac
        android_flags="$android_flags -DCMAKE_TOOLCHAIN_FILE=$NDK_ROOT/build/cmake/android.toolchain.cmake"
        android_flags="$android_flags -DANDROID_NDK=$NDK_ROOT"
        android_flags="$android_flags -DANDROID_ABI=$android_abi"
        android_flags="$android_flags -DANDROID_PLATFORM=android-21"
        android_flags="$android_flags -DANDROID_PIE=ON"
        android_flags="$android_flags -DANDROID_STL=c++_static"
        android_flags="$android_flags -DANDROID_CPP_FEATURES=exceptions"
    else
        toolchain_compiler=${toolchain}-gcc
        android_flags="$android_flags -DCMAKE_C_COMPILER=${toolchain_compiler}"
        android_flags="$android_flags -DCMAKE_SYSTEM_NAME=$os"
        android_flags="$android_flags -DCMAKE_SYSTEM_PROCESSOR=$arch"
    fi
    
    mkdir -p $local_build

    cd $local_build 
    cmake \
        -DCMAKE_INSTALL_PREFIX=/usr \
        -DCMAKE_C_FLAGS="$CFLAGS" \
        -DCPUINFO_LIBRARY_TYPE=static \
        -DCPUINFO_RUNTIME_TYPE=static \
        -DCPUINFO_BUILD_BENCHMARKS=OFF \
        -DCPUINFO_BUILD_UNIT_TESTS=OFF \
        -DCPUINFO_BUILD_MOCK_TESTS=OFF \
        -DCMAKE_BUILD_TYPE=Release \
        $android_flags $source_dir

    make -j
    mkdir -p $rid_dir/$rid/native/
    install_file="$(find $local_build -type f | grep -E 'so$|dll$|dylib$')"

    
    ${toolchain_compiler} $binding_flags -shared -o $libpath || exit -1
    #${toolchain_compiler} $binding_flags -o $libexec_path || (echo "unable to create exe" ; exit -1)

    chmod 777 $rid_dir/$rid/native/$libname
    #chmod 777 $rid_dir/$rid/native/$libexec_path
}


rm -rfv $rid_dir

# https://learn.microsoft.com/zh-cn/dotnet/core/rid-catalog

if [ "$musl" == "true" ]; then
    build_instance x86_64 Linux x86_64-linux-musl linux-musl-x64
    build_instance aarch64 Linux aarch64-linux-musl linux-musl-arm64
    build_instance armv6 Linux arm-linux-musleabi linux-musl-arm
    #build_instance riscv32 Linux riscv32-linux-musl linux-musl-rv32
    build_instance riscv64 Linux riscv64-linux-musl linux-musl-rv64
elif [ "$android" == "true" ]; then
    build_instance arm64 Android ndk android-arm64
    build_instance x86_64 Android ndk android-x64
    build_instance x86 Android ndk android-x86
    build_instance arm Android ndk android-arm
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
