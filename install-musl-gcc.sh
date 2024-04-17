#!/bin/bash
set -e

arch=$1

aria2c -x16 https://musl.cc/$arch-cross.tgz

tar xvf $arch-cross.tgz