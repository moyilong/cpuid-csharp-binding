#!/bin/bash


cat build/cpuid/include/cpuinfo.h | grep _has_ | grep cpuinfo_ | grep inline | awk '{print $(NF-1)}' | sed 's/(void)//g' | while read line ; do
    arch=$(echo $line | awk -F '_' '{print $3}')
    name=$(echo $line | sed "s/cpuinfo_has_${arch}_//g")
    xname=$(echo $name | sed -e 's/_plus/+/g' -e 's/_/./g')
    echo "copy_feature($arch,\"$xname\",$line);"
done