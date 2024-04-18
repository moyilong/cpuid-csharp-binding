#!/bin/bash
set -e

run()
{
    local docker_name=$1
    local docker_file=$2
    docker build -t build.image.$docker_name -f $docker_file --progress plain .
    docker run -i --rm \
        -v ./Dragon.CpuInfo/runtimes:/install \
        build.image.$docker_name /build-libcpuinfo.sh
}

run alpine Dockerfile.alpine
run ubuntu Dockerfile.ubuntu