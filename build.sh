#!/bin/bash
set -e

run()
{
    local docker_name=$1
    local docker_file=$2
    docker build -t build.image.$docker_name -f build-native/$docker_file --progress plain .
    docker run -i --rm \
        -v cpuid-source:/source \
        -e SOURCE_DIR=/source \
        -v ./Dragon.CpuInfo/runtimes:/install \
        -e INSTALL_DIR=/install \
        build.image.$docker_name /build-libcpuinfo.sh
}

run alpine Dockerfile.alpine
#run ubuntu Dockerfile.ubuntu
 run android Dockerfile.ndk