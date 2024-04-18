using System;
using System.Runtime.InteropServices;

namespace Dragon.CpuInfo.libCpuInfo
{
    /// <summary>
    /// Native Api
    /// https://github.com/pytorch/cpuinfo/blob/main/include/cpuinfo.h
    /// </summary>
    public static partial class CpuInfoNative
    {
        private const string libName = "cpuinfo-binding";

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_initialize();

        [DllImport(libName)]
        public static extern void binding_cpuinfo_deinilize();

        [DllImport(libName)]
        public static extern CpuInfoVendor binding_cpuinfo_vendor();

        [DllImport(libName)]
        public static extern CpuInfoUArch binding_cpuinfo_uarch();

        [DllImport(libName)]
        public static extern Int32 binding_cpuinfo_get_name(byte[] buffer);

        [DllImport(libName)]
        public static extern Int32 binding_get_version(byte[] buffer);

        [DllImport(libName)]
        public static extern CpuCacheInfo binding_cpuinfo_get_l1d_caches();

        [DllImport(libName)]
        public static extern CpuCacheInfo binding_cpuinfo_get_l1i_caches();

        [DllImport(libName)]
        public static extern CpuCacheInfo binding_cpuinfo_get_l3_caches();

        [DllImport(libName)]
        public static extern CpuCacheInfo binding_cpuinfo_get_l4_caches();

        [DllImport(libName)]
        public static extern CpuCacheInfo binding_cpuinfo_get_l2_caches();

        [DllImport(libName)]
        public static extern CpuCacheInfo binding_cpuinfo_get_l1d_cache(UInt32 index);

        [DllImport(libName)]
        public static extern CpuCacheInfo binding_cpuinfo_get_l1i_cache(UInt32 index);

        [DllImport(libName)]
        public static extern CpuCacheInfo binding_cpuinfo_get_l3_cache(UInt32 index);

        [DllImport(libName)]
        public static extern CpuCacheInfo binding_cpuinfo_get_l4_cache(UInt32 index);

        [DllImport(libName)]
        public static extern CpuCacheInfo binding_cpuinfo_get_l2_cache(UInt32 index);

        #region Generated

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_rdtsc();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_rdtscp();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_rdpid();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_clzero();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_mwait();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_mwaitx();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_fxsave();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_xsave();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_fpu();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_mmx();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_mmx_plus();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_3dnow();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_3dnow_plus();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_3dnow_geode();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_prefetch();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_prefetchw();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_prefetchwt1();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_daz();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_sse();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_sse2();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_sse3();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_ssse3();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_sse4_1();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_sse4_2();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_sse4a();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_misaligned_sse();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avxvnni();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_fma3();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_fma4();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_xop();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_f16c();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx2();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512f();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512pf();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512er();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512cd();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512dq();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512bw();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512vl();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512ifma();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512vbmi();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512vbmi2();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512bitalg();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512vpopcntdq();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512vnni();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512bf16();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512fp16();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512vp2intersect();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512_4vnniw();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_avx512_4fmaps();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_amx_bf16();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_amx_tile();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_amx_int8();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_amx_fp16();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_hle();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_rtm();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_xtest();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_mpx();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_cmov();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_cmpxchg8b();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_cmpxchg16b();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_clwb();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_movbe();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_lahf_sahf();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_lzcnt();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_popcnt();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_tbm();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_bmi();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_bmi2();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_adx();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_aes();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_vaes();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_pclmulqdq();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_vpclmulqdq();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_gfni();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_rdrand();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_rdseed();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_x86_sha();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_thumb();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_thumb2();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_v5e();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_v6();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_v6k();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_v7();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_v7mp();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_v8();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_idiv();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_vfpv2();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_vfpv3();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_vfpv3_d32();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_vfpv3_fp16();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_vfpv3_fp16_d32();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_vfpv4();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_vfpv4_d32();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_fp16_arith();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_bf16();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_wmmx();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_wmmx2();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_neon();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_neon_fp16();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_neon_fma();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_neon_v8();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_atomics();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_neon_rdm();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_neon_fp16_arith();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_fhm();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_neon_dot();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_neon_bf16();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_jscvt();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_fcma();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_i8mm();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_aes();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_sha1();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_sha2();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_pmull();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_crc32();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_sve();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_sve_bf16();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_arm_sve2();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_riscv_i();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_riscv_e();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_riscv_m();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_riscv_a();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_riscv_f();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_riscv_d();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_riscv_g();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_riscv_c();

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_has_riscv_v();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_processors_count();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_cores_count();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_clusters_count();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_packages_count();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_uarchs_count();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_l1i_caches_count();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_l1d_caches_count();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_l2_caches_count();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_l3_caches_count();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_l4_caches_count();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_max_cache_size();

        [DllImport(libName)]
        public static extern UInt32 binding_cpuinfo_get_current_uarch_index();

        #endregion Generated
    }
}