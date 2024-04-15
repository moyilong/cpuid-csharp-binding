#!/bin/bash


cat build/build/cpuid/include/cpuinfo.h | \
    grep '(void)' | sed 's/(void)//g' | \
    grep -E '^static inline' | \
    awk '{print  "FUNCTION_COPY(" $(NF-1) "," $(NF-2) ");" }'

cat build/build/cpuid/include/cpuinfo.h | \
    grep '(void)' | sed 's/(void)//g' | tr -d ';' | \
    grep -E '^uint32_t CPUINFO_ABI' | \
    awk '{print  "FUNCTION_COPY(" $(3) "," $(1) ");" }'

