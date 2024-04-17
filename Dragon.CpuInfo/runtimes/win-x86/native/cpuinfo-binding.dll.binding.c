#include <inttypes.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#ifdef COMPILE_MODE
#include "cpuinfo.h"
#else
#include "build/cpuid/include/cpuinfo.h"
#endif

#ifdef _WIN32
#define DLL_EXPORT __declspec(dllexport) __cdecl
#else
#define DLL_EXPORT
#endif

#define API_EXPORT CPUINFO_ABI

#define FUNCTION_COPY(source, type) \
    API_EXPORT type binding_##source() { return source(); }

#define STRUCT_API_COPY(native,type) \
    API_EXPORT type binding_##native() { type ret; memcpy(&ret,native(),sizeof(type)); }

#define STRUCT_API_ARG_COPY(native,type) \
	API_EXPORT type binding_##native(uint32_t index) {type ret ; memcpy(&ret,native(index),sizeof(type));}

#define CPUINFO_VERSION_STR "Binding " RID_NAME " " __DATE__ " " __TIME__ " libcpuinfo: git+" CPUINFO_VERSION

API_EXPORT int32_t binding_get_version(uint8_t* version) {
	if (version == NULL)
		return -1;
	strcpy(version, CPUINFO_VERSION_STR);
	return strlen(version);
}

API_EXPORT int32_t binding_cpuinfo_get_name(uint8_t* name)
{
	if (name == NULL)
		return -1;
	strcpy(name, cpuinfo_get_processors()->package->name);
	return strlen(name);
}

API_EXPORT enum cpuinfo_vendor binding_cpuinfo_vendor()
{
	return cpuinfo_get_processors()->core->vendor;
}

API_EXPORT enum cpuinfo_uarch binding_cpuinfo_uarch()
{
	return cpuinfo_get_processors()->core->uarch;
}

API_EXPORT bool binding_cpuinfo_initialize()
{
	cpuinfo_deinitialize();
	return cpuinfo_initialize();
}

API_EXPORT void binding_cpuinfo_deinilize()
{
	cpuinfo_deinitialize();
}

STRUCT_API_COPY(cpuinfo_get_processors, struct cpuinfo_processor);
STRUCT_API_COPY(cpuinfo_get_cores, struct cpuinfo_core);
STRUCT_API_COPY(cpuinfo_get_clusters, struct cpuinfo_cluster);
STRUCT_API_COPY(cpuinfo_get_packages, struct cpuinfo_package);
STRUCT_API_COPY(cpuinfo_get_uarchs, struct cpuinfo_uarch_info);

STRUCT_API_COPY(cpuinfo_get_l1i_caches, struct cpuinfo_cache);
STRUCT_API_COPY(cpuinfo_get_l1d_caches, struct cpuinfo_cache);
STRUCT_API_COPY(cpuinfo_get_l2_caches, struct cpuinfo_cache);
STRUCT_API_COPY(cpuinfo_get_l3_caches, struct cpuinfo_cache);
STRUCT_API_COPY(cpuinfo_get_l4_caches, struct cpuinfo_cache);



STRUCT_API_ARG_COPY(cpuinfo_get_processor, struct cpuinfo_processor);
STRUCT_API_ARG_COPY(cpuinfo_get_core, struct cpuinfo_core);
STRUCT_API_ARG_COPY(cpuinfo_get_cluster, struct cpuinfo_cluster);
STRUCT_API_ARG_COPY(cpuinfo_get_package, struct cpuinfo_package);
STRUCT_API_ARG_COPY(cpuinfo_get_uarch, struct cpuinfo_uarch_info);


STRUCT_API_ARG_COPY(cpuinfo_get_l1i_cache, struct cpuinfo_cache);
STRUCT_API_ARG_COPY(cpuinfo_get_l1d_cache, struct cpuinfo_cache);
STRUCT_API_ARG_COPY(cpuinfo_get_l2_cache, struct cpuinfo_cache);
STRUCT_API_ARG_COPY(cpuinfo_get_l3_cache, struct cpuinfo_cache);
STRUCT_API_ARG_COPY(cpuinfo_get_l4_cache, struct cpuinfo_cache);



FUNCTION_COPY(cpuinfo_has_x86_rdtsc,bool);
FUNCTION_COPY(cpuinfo_has_x86_rdtscp,bool);
FUNCTION_COPY(cpuinfo_has_x86_rdpid,bool);
FUNCTION_COPY(cpuinfo_has_x86_clzero,bool);
FUNCTION_COPY(cpuinfo_has_x86_mwait,bool);
FUNCTION_COPY(cpuinfo_has_x86_mwaitx,bool);
FUNCTION_COPY(cpuinfo_has_x86_fxsave,bool);
FUNCTION_COPY(cpuinfo_has_x86_xsave,bool);
FUNCTION_COPY(cpuinfo_has_x86_fpu,bool);
FUNCTION_COPY(cpuinfo_has_x86_mmx,bool);
FUNCTION_COPY(cpuinfo_has_x86_mmx_plus,bool);
FUNCTION_COPY(cpuinfo_has_x86_3dnow,bool);
FUNCTION_COPY(cpuinfo_has_x86_3dnow_plus,bool);
FUNCTION_COPY(cpuinfo_has_x86_3dnow_geode,bool);
FUNCTION_COPY(cpuinfo_has_x86_prefetch,bool);
FUNCTION_COPY(cpuinfo_has_x86_prefetchw,bool);
FUNCTION_COPY(cpuinfo_has_x86_prefetchwt1,bool);
FUNCTION_COPY(cpuinfo_has_x86_daz,bool);
FUNCTION_COPY(cpuinfo_has_x86_sse,bool);
FUNCTION_COPY(cpuinfo_has_x86_sse2,bool);
FUNCTION_COPY(cpuinfo_has_x86_sse3,bool);
FUNCTION_COPY(cpuinfo_has_x86_ssse3,bool);
FUNCTION_COPY(cpuinfo_has_x86_sse4_1,bool);
FUNCTION_COPY(cpuinfo_has_x86_sse4_2,bool);
FUNCTION_COPY(cpuinfo_has_x86_sse4a,bool);
FUNCTION_COPY(cpuinfo_has_x86_misaligned_sse,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx,bool);
FUNCTION_COPY(cpuinfo_has_x86_avxvnni,bool);
FUNCTION_COPY(cpuinfo_has_x86_fma3,bool);
FUNCTION_COPY(cpuinfo_has_x86_fma4,bool);
FUNCTION_COPY(cpuinfo_has_x86_xop,bool);
FUNCTION_COPY(cpuinfo_has_x86_f16c,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx2,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512f,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512pf,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512er,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512cd,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512dq,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512bw,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512vl,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512ifma,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512vbmi,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512vbmi2,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512bitalg,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512vpopcntdq,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512vnni,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512bf16,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512fp16,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512vp2intersect,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512_4vnniw,bool);
FUNCTION_COPY(cpuinfo_has_x86_avx512_4fmaps,bool);
FUNCTION_COPY(cpuinfo_has_x86_amx_bf16,bool);
FUNCTION_COPY(cpuinfo_has_x86_amx_tile,bool);
FUNCTION_COPY(cpuinfo_has_x86_amx_int8,bool);
FUNCTION_COPY(cpuinfo_has_x86_amx_fp16,bool);
FUNCTION_COPY(cpuinfo_has_x86_hle,bool);
FUNCTION_COPY(cpuinfo_has_x86_rtm,bool);
FUNCTION_COPY(cpuinfo_has_x86_xtest,bool);
FUNCTION_COPY(cpuinfo_has_x86_mpx,bool);
FUNCTION_COPY(cpuinfo_has_x86_cmov,bool);
FUNCTION_COPY(cpuinfo_has_x86_cmpxchg8b,bool);
FUNCTION_COPY(cpuinfo_has_x86_cmpxchg16b,bool);
FUNCTION_COPY(cpuinfo_has_x86_clwb,bool);
FUNCTION_COPY(cpuinfo_has_x86_movbe,bool);
FUNCTION_COPY(cpuinfo_has_x86_lahf_sahf,bool);
FUNCTION_COPY(cpuinfo_has_x86_lzcnt,bool);
FUNCTION_COPY(cpuinfo_has_x86_popcnt,bool);
FUNCTION_COPY(cpuinfo_has_x86_tbm,bool);
FUNCTION_COPY(cpuinfo_has_x86_bmi,bool);
FUNCTION_COPY(cpuinfo_has_x86_bmi2,bool);
FUNCTION_COPY(cpuinfo_has_x86_adx,bool);
FUNCTION_COPY(cpuinfo_has_x86_aes,bool);
FUNCTION_COPY(cpuinfo_has_x86_vaes,bool);
FUNCTION_COPY(cpuinfo_has_x86_pclmulqdq,bool);
FUNCTION_COPY(cpuinfo_has_x86_vpclmulqdq,bool);
FUNCTION_COPY(cpuinfo_has_x86_gfni,bool);
FUNCTION_COPY(cpuinfo_has_x86_rdrand,bool);
FUNCTION_COPY(cpuinfo_has_x86_rdseed,bool);
FUNCTION_COPY(cpuinfo_has_x86_sha,bool);
FUNCTION_COPY(cpuinfo_has_arm_thumb,bool);
FUNCTION_COPY(cpuinfo_has_arm_thumb2,bool);
FUNCTION_COPY(cpuinfo_has_arm_v5e,bool);
FUNCTION_COPY(cpuinfo_has_arm_v6,bool);
FUNCTION_COPY(cpuinfo_has_arm_v6k,bool);
FUNCTION_COPY(cpuinfo_has_arm_v7,bool);
FUNCTION_COPY(cpuinfo_has_arm_v7mp,bool);
FUNCTION_COPY(cpuinfo_has_arm_v8,bool);
FUNCTION_COPY(cpuinfo_has_arm_idiv,bool);
FUNCTION_COPY(cpuinfo_has_arm_vfpv2,bool);
FUNCTION_COPY(cpuinfo_has_arm_vfpv3,bool);
FUNCTION_COPY(cpuinfo_has_arm_vfpv3_d32,bool);
FUNCTION_COPY(cpuinfo_has_arm_vfpv3_fp16,bool);
FUNCTION_COPY(cpuinfo_has_arm_vfpv3_fp16_d32,bool);
FUNCTION_COPY(cpuinfo_has_arm_vfpv4,bool);
FUNCTION_COPY(cpuinfo_has_arm_vfpv4_d32,bool);
FUNCTION_COPY(cpuinfo_has_arm_fp16_arith,bool);
FUNCTION_COPY(cpuinfo_has_arm_bf16,bool);
FUNCTION_COPY(cpuinfo_has_arm_wmmx,bool);
FUNCTION_COPY(cpuinfo_has_arm_wmmx2,bool);
FUNCTION_COPY(cpuinfo_has_arm_neon,bool);
FUNCTION_COPY(cpuinfo_has_arm_neon_fp16,bool);
FUNCTION_COPY(cpuinfo_has_arm_neon_fma,bool);
FUNCTION_COPY(cpuinfo_has_arm_neon_v8,bool);
FUNCTION_COPY(cpuinfo_has_arm_atomics,bool);
FUNCTION_COPY(cpuinfo_has_arm_neon_rdm,bool);
FUNCTION_COPY(cpuinfo_has_arm_neon_fp16_arith,bool);
FUNCTION_COPY(cpuinfo_has_arm_fhm,bool);
FUNCTION_COPY(cpuinfo_has_arm_neon_dot,bool);
FUNCTION_COPY(cpuinfo_has_arm_neon_bf16,bool);
FUNCTION_COPY(cpuinfo_has_arm_jscvt,bool);
FUNCTION_COPY(cpuinfo_has_arm_fcma,bool);
FUNCTION_COPY(cpuinfo_has_arm_i8mm,bool);
FUNCTION_COPY(cpuinfo_has_arm_aes,bool);
FUNCTION_COPY(cpuinfo_has_arm_sha1,bool);
FUNCTION_COPY(cpuinfo_has_arm_sha2,bool);
FUNCTION_COPY(cpuinfo_has_arm_pmull,bool);
FUNCTION_COPY(cpuinfo_has_arm_crc32,bool);
FUNCTION_COPY(cpuinfo_has_arm_sve,bool);
FUNCTION_COPY(cpuinfo_has_arm_sve_bf16,bool);
FUNCTION_COPY(cpuinfo_has_arm_sve2,bool);
FUNCTION_COPY(cpuinfo_has_riscv_i,bool);
FUNCTION_COPY(cpuinfo_has_riscv_e,bool);
FUNCTION_COPY(cpuinfo_has_riscv_m,bool);
FUNCTION_COPY(cpuinfo_has_riscv_a,bool);
FUNCTION_COPY(cpuinfo_has_riscv_f,bool);
FUNCTION_COPY(cpuinfo_has_riscv_d,bool);
FUNCTION_COPY(cpuinfo_has_riscv_g,bool);
FUNCTION_COPY(cpuinfo_has_riscv_c,bool);
FUNCTION_COPY(cpuinfo_has_riscv_v,bool);
FUNCTION_COPY(cpuinfo_get_processors_count,uint32_t);
FUNCTION_COPY(cpuinfo_get_cores_count,uint32_t);
FUNCTION_COPY(cpuinfo_get_clusters_count,uint32_t);
FUNCTION_COPY(cpuinfo_get_packages_count,uint32_t);
FUNCTION_COPY(cpuinfo_get_uarchs_count,uint32_t);
FUNCTION_COPY(cpuinfo_get_l1i_caches_count,uint32_t);
FUNCTION_COPY(cpuinfo_get_l1d_caches_count,uint32_t);
FUNCTION_COPY(cpuinfo_get_l2_caches_count,uint32_t);
FUNCTION_COPY(cpuinfo_get_l3_caches_count,uint32_t);
FUNCTION_COPY(cpuinfo_get_l4_caches_count,uint32_t);
FUNCTION_COPY(cpuinfo_get_max_cache_size,uint32_t);
FUNCTION_COPY(cpuinfo_get_current_uarch_index,uint32_t);
