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

#define CPUINFO_VERSION_STR "Binding " RID_NAME " " __DATE__ " " __TIME__ " libcpuinfo: git+" CPUINFO_VERSION

#if CPUINFO_ARCH_X86 || CPUINFO_ARCH_X86_64
#define ARCH_STRING "x86"
#elif CPUINFO_ARCH_ARM64 || CPUINFO_ARCH_ARM || CPUINFO_ARCH_ASMJS
#define ARCH_STRING "arm"
#elif CPUINFO_ARCH_PPC64
#define ARCH_STRING "ppc"
#elif CPUINFO_ARCH_WASM || CPUINFO_ARCH_WASMSIMD
#define ARCH_STRING "wasm"
#elif CPUINFO_ARCH_RISCV32 || CPUINFO_ARCH_RISCV64
#define ARCH_STRING "riscv64"
#else
#error "unknow arch!"
#endif

#define YAML_SIZE 65536

void put_yaml_stri(char *yamlBuffer, const char *key, const char *val, int tab)
{
	char buffer[64] = {0x00};
	tab = tab * 2;
	if (key[0] == '-')
		tab -= 2;
	if (tab < 0)
		tab = 0;
	for (int n = 0; n < tab; n++)
	{
		buffer[n] = ' ';
	}

	char output[128] = {0x00};
	sprintf(output, "%s%s: %s\n", buffer, key, val);
	strcpy(yamlBuffer + strlen(yamlBuffer), output);
}
void put_yaml_bool(char *yamlBuffer, const char *key, const bool val, int tab)
{
	put_yaml_stri(yamlBuffer, key, val ? "true" : "false", tab);
}

void put_yaml_inte(char *yamlBuffer, const char *key, const uint32_t val, int tab)
{
	char str[64] = {0x00};
	sprintf(str, "%u", val);
	put_yaml_stri(yamlBuffer, key, str, tab);
}


API_EXPORT uint32_t copy_yaml_size()
{
	return YAML_SIZE;
}

void copy_cache_info(char *yaml, const char *name, int tab, struct cpuinfo_cache *cache)
{
	put_yaml_stri(yaml, "- name", name, tab);
#define cache_cpy(vname) \
	put_yaml_inte(yaml, #vname, cache->vname, tab)
	cache_cpy(size);
	cache_cpy(associativity);
	cache_cpy(sets);
	cache_cpy(partitions);
	cache_cpy(line_size);
	cache_cpy(flags);
	cache_cpy(processor_start);
	cache_cpy(processor_count);
}

uint32_t copy_yaml(char *yaml)
{
	if (!cpuinfo_initialize())
	{
		strcpy(yaml, "unable to init!");
		return -1;
	}
	put_yaml_stri(yaml, "lib_version", "\"" CPUINFO_VERSION_STR "\"", 0);
	put_yaml_stri(yaml, "arch_string", ARCH_STRING, 0);
	put_yaml_stri(yaml, "cpu", "", 0);
	put_yaml_stri(yaml, "model", cpuinfo_get_processors()->package->name, 1);
	put_yaml_inte(yaml, "slotes", cpuinfo_get_clusters_count(), 1);
	put_yaml_inte(yaml, "cores", cpuinfo_get_packages()->core_count, 1);
	put_yaml_inte(yaml, "threads", cpuinfo_get_processors_count(), 1);
	put_yaml_inte(yaml, "vendor", (uint32_t)(cpuinfo_get_cluster(0)->vendor), 1);
	put_yaml_inte(yaml, "uarch", (uint32_t)(cpuinfo_get_cluster(0)->uarch), 1);
	put_yaml_stri(yaml, "caches", "", 1);
	for (int n = 0; n < cpuinfo_get_l1i_caches_count(); n++)
	{
		copy_cache_info(yaml, "l1i", 2, cpuinfo_get_l1d_cache(n));
	}
#define cache_copy_opt(name)                                      \
	for (int n = 0; n < cpuinfo_get_##name##_caches_count(); n++) \
		copy_cache_info(yaml, #name, 2, cpuinfo_get_##name##_cache(n));

	cache_copy_opt(l1d);
	cache_copy_opt(l1i);
	cache_copy_opt(l2);
	cache_copy_opt(l3);
	cache_copy_opt(l4);
	put_yaml_stri(yaml, "clusters", "", 1);
	for (int cluster = 0; cluster < cpuinfo_get_clusters_count(); cluster++)
	{
		struct cpuinfo_cluster *clu = cpuinfo_get_cluster(cluster);
		if (clu == NULL)
			break;

		put_yaml_inte(yaml, "- id", clu->cluster_id, 2);
		put_yaml_inte(yaml, "vendor", clu->vendor, 2);
		put_yaml_inte(yaml, "uarch", clu->uarch, 2);
		put_yaml_stri(yaml, "cores", "", 2);

		for (int proc = 0; proc < clu->core_count; proc++)
		{
			struct cpuinfo_core *core = cpuinfo_get_core(proc + clu->core_start);
			put_yaml_inte(yaml, "- id", core->core_id, 3);
			put_yaml_inte(yaml, "vendor", core->vendor, 3);
			put_yaml_inte(yaml, "uarch", core->uarch, 3);
			put_yaml_stri(yaml, "threads", "", 3);
			for (int th = 0; th < core->processor_count; th++)
			{
				struct cpuinfo_processor *thread = cpuinfo_get_processor(th);
				put_yaml_inte(yaml, "- id", thread->smt_id, 4);
			}
		}
	}

	put_yaml_stri(yaml, "features", "", 1);
#define copy_feature(arch, name, xname, func)  \
	put_yaml_stri(yaml, "- arch", #arch, 2);   \
	put_yaml_bool(yaml, "stat", func(), 2);    \
	put_yaml_stri(yaml, "funcname", #func, 2); \
	put_yaml_stri(yaml, "name", #name, 2);     \
	put_yaml_stri(yaml, "xname", xname, 2);
	copy_feature(x86, rdtsc, "rdtsc", cpuinfo_has_x86_rdtsc);
	copy_feature(x86, rdtscp, "rdtscp", cpuinfo_has_x86_rdtscp);
	copy_feature(x86, rdpid, "rdpid", cpuinfo_has_x86_rdpid);
	copy_feature(x86, clzero, "clzero", cpuinfo_has_x86_clzero);
	copy_feature(x86, mwait, "mwait", cpuinfo_has_x86_mwait);
	copy_feature(x86, mwaitx, "mwaitx", cpuinfo_has_x86_mwaitx);
	copy_feature(x86, fxsave, "fxsave", cpuinfo_has_x86_fxsave);
	copy_feature(x86, xsave, "xsave", cpuinfo_has_x86_xsave);
	copy_feature(x86, fpu, "fpu", cpuinfo_has_x86_fpu);
	copy_feature(x86, mmx, "mmx", cpuinfo_has_x86_mmx);
	copy_feature(x86, mmx_plus, "mmx+", cpuinfo_has_x86_mmx_plus);
	copy_feature(x86, 3dnow, "3dnow", cpuinfo_has_x86_3dnow);
	copy_feature(x86, 3dnow_plus, "3dnow+", cpuinfo_has_x86_3dnow_plus);
	copy_feature(x86, 3dnow_geode, "3dnow.geode", cpuinfo_has_x86_3dnow_geode);
	copy_feature(x86, prefetch, "prefetch", cpuinfo_has_x86_prefetch);
	copy_feature(x86, prefetchw, "prefetchw", cpuinfo_has_x86_prefetchw);
	copy_feature(x86, prefetchwt1, "prefetchwt1", cpuinfo_has_x86_prefetchwt1);
	copy_feature(x86, daz, "daz", cpuinfo_has_x86_daz);
	copy_feature(x86, sse, "sse", cpuinfo_has_x86_sse);
	copy_feature(x86, sse2, "sse2", cpuinfo_has_x86_sse2);
	copy_feature(x86, sse3, "sse3", cpuinfo_has_x86_sse3);
	copy_feature(x86, ssse3, "ssse3", cpuinfo_has_x86_ssse3);
	copy_feature(x86, sse4_1, "sse4.1", cpuinfo_has_x86_sse4_1);
	copy_feature(x86, sse4_2, "sse4.2", cpuinfo_has_x86_sse4_2);
	copy_feature(x86, sse4a, "sse4a", cpuinfo_has_x86_sse4a);
	copy_feature(x86, misaligned_sse, "misaligned.sse", cpuinfo_has_x86_misaligned_sse);
	copy_feature(x86, avx, "avx", cpuinfo_has_x86_avx);
	copy_feature(x86, avxvnni, "avxvnni", cpuinfo_has_x86_avxvnni);
	copy_feature(x86, fma3, "fma3", cpuinfo_has_x86_fma3);
	copy_feature(x86, fma4, "fma4", cpuinfo_has_x86_fma4);
	copy_feature(x86, xop, "xop", cpuinfo_has_x86_xop);
	copy_feature(x86, f16c, "f16c", cpuinfo_has_x86_f16c);
	copy_feature(x86, avx2, "avx2", cpuinfo_has_x86_avx2);
	copy_feature(x86, avx512f, "avx512f", cpuinfo_has_x86_avx512f);
	copy_feature(x86, avx512pf, "avx512pf", cpuinfo_has_x86_avx512pf);
	copy_feature(x86, avx512er, "avx512er", cpuinfo_has_x86_avx512er);
	copy_feature(x86, avx512cd, "avx512cd", cpuinfo_has_x86_avx512cd);
	copy_feature(x86, avx512dq, "avx512dq", cpuinfo_has_x86_avx512dq);
	copy_feature(x86, avx512bw, "avx512bw", cpuinfo_has_x86_avx512bw);
	copy_feature(x86, avx512vl, "avx512vl", cpuinfo_has_x86_avx512vl);
	copy_feature(x86, avx512ifma, "avx512ifma", cpuinfo_has_x86_avx512ifma);
	copy_feature(x86, avx512vbmi, "avx512vbmi", cpuinfo_has_x86_avx512vbmi);
	copy_feature(x86, avx512vbmi2, "avx512vbmi2", cpuinfo_has_x86_avx512vbmi2);
	copy_feature(x86, avx512bitalg, "avx512bitalg", cpuinfo_has_x86_avx512bitalg);
	copy_feature(x86, avx512vpopcntdq, "avx512vpopcntdq", cpuinfo_has_x86_avx512vpopcntdq);
	copy_feature(x86, avx512vnni, "avx512vnni", cpuinfo_has_x86_avx512vnni);
	copy_feature(x86, avx512bf16, "avx512bf16", cpuinfo_has_x86_avx512bf16);
	copy_feature(x86, avx512fp16, "avx512fp16", cpuinfo_has_x86_avx512fp16);
	copy_feature(x86, avx512vp2intersect, "avx512vp2intersect", cpuinfo_has_x86_avx512vp2intersect);
	copy_feature(x86, avx512_4vnniw, "avx512.4vnniw", cpuinfo_has_x86_avx512_4vnniw);
	copy_feature(x86, avx512_4fmaps, "avx512.4fmaps", cpuinfo_has_x86_avx512_4fmaps);
	copy_feature(x86, amx_bf16, "amx.bf16", cpuinfo_has_x86_amx_bf16);
	copy_feature(x86, amx_tile, "amx.tile", cpuinfo_has_x86_amx_tile);
	copy_feature(x86, amx_int8, "amx.int8", cpuinfo_has_x86_amx_int8);
	copy_feature(x86, amx_fp16, "amx.fp16", cpuinfo_has_x86_amx_fp16);
	copy_feature(x86, hle, "hle", cpuinfo_has_x86_hle);
	copy_feature(x86, rtm, "rtm", cpuinfo_has_x86_rtm);
	copy_feature(x86, xtest, "xtest", cpuinfo_has_x86_xtest);
	copy_feature(x86, mpx, "mpx", cpuinfo_has_x86_mpx);
	copy_feature(x86, cmov, "cmov", cpuinfo_has_x86_cmov);
	copy_feature(x86, cmpxchg8b, "cmpxchg8b", cpuinfo_has_x86_cmpxchg8b);
	copy_feature(x86, cmpxchg16b, "cmpxchg16b", cpuinfo_has_x86_cmpxchg16b);
	copy_feature(x86, clwb, "clwb", cpuinfo_has_x86_clwb);
	copy_feature(x86, movbe, "movbe", cpuinfo_has_x86_movbe);
	copy_feature(x86, lahf_sahf, "lahf.sahf", cpuinfo_has_x86_lahf_sahf);
	copy_feature(x86, lzcnt, "lzcnt", cpuinfo_has_x86_lzcnt);
	copy_feature(x86, popcnt, "popcnt", cpuinfo_has_x86_popcnt);
	copy_feature(x86, tbm, "tbm", cpuinfo_has_x86_tbm);
	copy_feature(x86, bmi, "bmi", cpuinfo_has_x86_bmi);
	copy_feature(x86, bmi2, "bmi2", cpuinfo_has_x86_bmi2);
	copy_feature(x86, adx, "adx", cpuinfo_has_x86_adx);
	copy_feature(x86, aes, "aes", cpuinfo_has_x86_aes);
	copy_feature(x86, vaes, "vaes", cpuinfo_has_x86_vaes);
	copy_feature(x86, pclmulqdq, "pclmulqdq", cpuinfo_has_x86_pclmulqdq);
	copy_feature(x86, vpclmulqdq, "vpclmulqdq", cpuinfo_has_x86_vpclmulqdq);
	copy_feature(x86, gfni, "gfni", cpuinfo_has_x86_gfni);
	copy_feature(x86, rdrand, "rdrand", cpuinfo_has_x86_rdrand);
	copy_feature(x86, rdseed, "rdseed", cpuinfo_has_x86_rdseed);
	copy_feature(x86, sha, "sha", cpuinfo_has_x86_sha);
	copy_feature(arm, thumb, "thumb", cpuinfo_has_arm_thumb);
	copy_feature(arm, thumb2, "thumb2", cpuinfo_has_arm_thumb2);
	copy_feature(arm, v5e, "v5e", cpuinfo_has_arm_v5e);
	copy_feature(arm, v6, "v6", cpuinfo_has_arm_v6);
	copy_feature(arm, v6k, "v6k", cpuinfo_has_arm_v6k);
	copy_feature(arm, v7, "v7", cpuinfo_has_arm_v7);
	copy_feature(arm, v7mp, "v7mp", cpuinfo_has_arm_v7mp);
	copy_feature(arm, v8, "v8", cpuinfo_has_arm_v8);
	copy_feature(arm, idiv, "idiv", cpuinfo_has_arm_idiv);
	copy_feature(arm, vfpv2, "vfpv2", cpuinfo_has_arm_vfpv2);
	copy_feature(arm, vfpv3, "vfpv3", cpuinfo_has_arm_vfpv3);
	copy_feature(arm, vfpv3_d32, "vfpv3.d32", cpuinfo_has_arm_vfpv3_d32);
	copy_feature(arm, vfpv3_fp16, "vfpv3.fp16", cpuinfo_has_arm_vfpv3_fp16);
	copy_feature(arm, vfpv3_fp16_d32, "vfpv3.fp16.d32", cpuinfo_has_arm_vfpv3_fp16_d32);
	copy_feature(arm, vfpv4, "vfpv4", cpuinfo_has_arm_vfpv4);
	copy_feature(arm, vfpv4_d32, "vfpv4.d32", cpuinfo_has_arm_vfpv4_d32);
	copy_feature(arm, fp16_arith, "fp16.arith", cpuinfo_has_arm_fp16_arith);
	copy_feature(arm, bf16, "bf16", cpuinfo_has_arm_bf16);
	copy_feature(arm, wmmx, "wmmx", cpuinfo_has_arm_wmmx);
	copy_feature(arm, wmmx2, "wmmx2", cpuinfo_has_arm_wmmx2);
	copy_feature(arm, neon, "neon", cpuinfo_has_arm_neon);
	copy_feature(arm, neon_fp16, "neon.fp16", cpuinfo_has_arm_neon_fp16);
	copy_feature(arm, neon_fma, "neon.fma", cpuinfo_has_arm_neon_fma);
	copy_feature(arm, neon_v8, "neon.v8", cpuinfo_has_arm_neon_v8);
	copy_feature(arm, atomics, "atomics", cpuinfo_has_arm_atomics);
	copy_feature(arm, neon_rdm, "neon.rdm", cpuinfo_has_arm_neon_rdm);
	copy_feature(arm, neon_fp16_arith, "neon.fp16.arith", cpuinfo_has_arm_neon_fp16_arith);
	copy_feature(arm, fhm, "fhm", cpuinfo_has_arm_fhm);
	copy_feature(arm, neon_dot, "neon.dot", cpuinfo_has_arm_neon_dot);
	copy_feature(arm, neon_bf16, "neon.bf16", cpuinfo_has_arm_neon_bf16);
	copy_feature(arm, jscvt, "jscvt", cpuinfo_has_arm_jscvt);
	copy_feature(arm, fcma, "fcma", cpuinfo_has_arm_fcma);
	copy_feature(arm, i8mm, "i8mm", cpuinfo_has_arm_i8mm);
	copy_feature(arm, aes, "aes", cpuinfo_has_arm_aes);
	copy_feature(arm, sha1, "sha1", cpuinfo_has_arm_sha1);
	copy_feature(arm, sha2, "sha2", cpuinfo_has_arm_sha2);
	copy_feature(arm, pmull, "pmull", cpuinfo_has_arm_pmull);
	copy_feature(arm, crc32, "crc32", cpuinfo_has_arm_crc32);
	copy_feature(arm, sve, "sve", cpuinfo_has_arm_sve);
	copy_feature(arm, sve_bf16, "sve.bf16", cpuinfo_has_arm_sve_bf16);
	copy_feature(arm, sve2, "sve2", cpuinfo_has_arm_sve2);
	copy_feature(riscv, i, "i", cpuinfo_has_riscv_i);
	copy_feature(riscv, e, "e", cpuinfo_has_riscv_e);
	copy_feature(riscv, m, "m", cpuinfo_has_riscv_m);
	copy_feature(riscv, a, "a", cpuinfo_has_riscv_a);
	copy_feature(riscv, f, "f", cpuinfo_has_riscv_f);
	copy_feature(riscv, d, "d", cpuinfo_has_riscv_d);
	copy_feature(riscv, g, "g", cpuinfo_has_riscv_g);
	copy_feature(riscv, c, "c", cpuinfo_has_riscv_c);
	copy_feature(riscv, v, "v", cpuinfo_has_riscv_v);
	cpuinfo_deinitialize();
	return strlen(yaml);
}
API_EXPORT uint32_t copy_yaml_api(uint8_t *yaml, uint32_t max)
{
	if (max < 32768)
	{
		strcpy(yaml, "buffer too small");
		return -1;
	}
	else
	{
		return copy_yaml(yaml);
	}
}
void main()
{
	char yaml[YAML_SIZE] = {0x00};
	copy_yaml(yaml);
	printf("%s", yaml);
}

API_EXPORT void binding_cpuinfo_deinitialize()
{
	cpuinfo_deinitialize();
}

API_EXPORT bool binding_cpuinfo_initialize()
{
	cpuinfo_initialize();
}
