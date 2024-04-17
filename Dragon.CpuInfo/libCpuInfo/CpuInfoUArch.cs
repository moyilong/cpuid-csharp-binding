namespace Dragon.CpuInfo.libCpuInfo
{
    /**
     * Processor microarchitecture
     *
     * Processors with different microarchitectures often have different instruction
     * performance characteristics, and may have dramatically different pipeline
     * organization.
     */

    public enum CpuInfoUArch
    {
        /** Microarchitecture is unknown, or the library failed to get
           information about the microarchitecture from OS */
        cpuinfo_uarch_unknown = 0,

        /** Pentium and Pentium MMX microarchitecture. */
        cpuinfo_uarch_p5 = 0x00100100,
        /** Intel Quark microarchitecture. */
        cpuinfo_uarch_quark = 0x00100101,

        /** Pentium Pro, Pentium II, and Pentium III. */
        cpuinfo_uarch_p6 = 0x00100200,
        /** Pentium M. */
        cpuinfo_uarch_dothan = 0x00100201,
        /** Intel Core microarchitecture. */
        cpuinfo_uarch_yonah = 0x00100202,
        /** Intel Core 2 microarchitecture on 65 nm process. */
        cpuinfo_uarch_conroe = 0x00100203,
        /** Intel Core 2 microarchitecture on 45 nm process. */
        cpuinfo_uarch_penryn = 0x00100204,
        /** Intel Nehalem and Westmere microarchitectures (Core i3/i5/i7 1st
           gen). */
        cpuinfo_uarch_nehalem = 0x00100205,
        /** Intel Sandy Bridge microarchitecture (Core i3/i5/i7 2nd gen). */
        cpuinfo_uarch_sandy_bridge = 0x00100206,
        /** Intel Ivy Bridge microarchitecture (Core i3/i5/i7 3rd gen). */
        cpuinfo_uarch_ivy_bridge = 0x00100207,
        /** Intel Haswell microarchitecture (Core i3/i5/i7 4th gen). */
        cpuinfo_uarch_haswell = 0x00100208,
        /** Intel Broadwell microarchitecture. */
        cpuinfo_uarch_broadwell = 0x00100209,
        /** Intel Sky Lake microarchitecture (14 nm, including
           Kaby/Coffee/Whiskey/Amber/Comet/Cascade/Cooper Lake). */
        cpuinfo_uarch_sky_lake = 0x0010020A,
        /** DEPRECATED (Intel Kaby Lake microarchitecture). */
        cpuinfo_uarch_kaby_lake = 0x0010020A,
        /** Intel Palm Cove microarchitecture (10 nm, Cannon Lake). */
        cpuinfo_uarch_palm_cove = 0x0010020B,
        /** Intel Sunny Cove microarchitecture (10 nm, Ice Lake). */
        cpuinfo_uarch_sunny_cove = 0x0010020C,

        /** Pentium 4 with Willamette, Northwood, or Foster cores. */
        cpuinfo_uarch_willamette = 0x00100300,
        /** Pentium 4 with Prescott and later cores. */
        cpuinfo_uarch_prescott = 0x00100301,

        /** Intel Atom on 45 nm process. */
        cpuinfo_uarch_bonnell = 0x00100400,
        /** Intel Atom on 32 nm process. */
        cpuinfo_uarch_saltwell = 0x00100401,
        /** Intel Silvermont microarchitecture (22 nm out-of-order Atom). */
        cpuinfo_uarch_silvermont = 0x00100402,
        /** Intel Airmont microarchitecture (14 nm out-of-order Atom). */
        cpuinfo_uarch_airmont = 0x00100403,
        /** Intel Goldmont microarchitecture (Denverton, Apollo Lake). */
        cpuinfo_uarch_goldmont = 0x00100404,
        /** Intel Goldmont Plus microarchitecture (Gemini Lake). */
        cpuinfo_uarch_goldmont_plus = 0x00100405,

        /** Intel Knights Ferry HPC boards. */
        cpuinfo_uarch_knights_ferry = 0x00100500,
        /** Intel Knights Corner HPC boards (aka Xeon Phi). */
        cpuinfo_uarch_knights_corner = 0x00100501,
        /** Intel Knights Landing microarchitecture (second-gen MIC). */
        cpuinfo_uarch_knights_landing = 0x00100502,
        /** Intel Knights Hill microarchitecture (third-gen MIC). */
        cpuinfo_uarch_knights_hill = 0x00100503,
        /** Intel Knights Mill Xeon Phi. */
        cpuinfo_uarch_knights_mill = 0x00100504,

        /** Intel/Marvell XScale series. */
        cpuinfo_uarch_xscale = 0x00100600,

        /** AMD K5. */
        cpuinfo_uarch_k5 = 0x00200100,
        /** AMD K6 and alike. */
        cpuinfo_uarch_k6 = 0x00200101,
        /** AMD Athlon and Duron. */
        cpuinfo_uarch_k7 = 0x00200102,
        /** AMD Athlon 64, Opteron 64. */
        cpuinfo_uarch_k8 = 0x00200103,
        /** AMD Family 10h (Barcelona, Istambul, Magny-Cours). */
        cpuinfo_uarch_k10 = 0x00200104,
        /**
         * AMD Bulldozer microarchitecture
         * Zambezi FX-series CPUs, Zurich, Valencia and Interlagos Opteron CPUs.
         */
        cpuinfo_uarch_bulldozer = 0x00200105,
        /**
         * AMD Piledriver microarchitecture
         * Vishera FX-series CPUs, Trinity and Richland APUs, Delhi, Seoul, Abu
         * Dhabi Opteron CPUs.
         */
        cpuinfo_uarch_piledriver = 0x00200106,
        /** AMD Steamroller microarchitecture (Kaveri APUs). */
        cpuinfo_uarch_steamroller = 0x00200107,
        /** AMD Excavator microarchitecture (Carizzo APUs). */
        cpuinfo_uarch_excavator = 0x00200108,
        /** AMD Zen microarchitecture (12/14 nm Ryzen and EPYC CPUs). */
        cpuinfo_uarch_zen = 0x00200109,
        /** AMD Zen 2 microarchitecture (7 nm Ryzen and EPYC CPUs). */
        cpuinfo_uarch_zen2 = 0x0020010A,
        /** AMD Zen 3 microarchitecture. */
        cpuinfo_uarch_zen3 = 0x0020010B,
        /** AMD Zen 4 microarchitecture. */
        cpuinfo_uarch_zen4 = 0x0020010C,

        /** NSC Geode and AMD Geode GX and LX. */
        cpuinfo_uarch_geode = 0x00200200,
        /** AMD Bobcat mobile microarchitecture. */
        cpuinfo_uarch_bobcat = 0x00200201,
        /** AMD Jaguar mobile microarchitecture. */
        cpuinfo_uarch_jaguar = 0x00200202,
        /** AMD Puma mobile microarchitecture. */
        cpuinfo_uarch_puma = 0x00200203,

        /** ARM7 series. */
        cpuinfo_uarch_arm7 = 0x00300100,
        /** ARM9 series. */
        cpuinfo_uarch_arm9 = 0x00300101,
        /** ARM 1136, ARM 1156, ARM 1176, or ARM 11MPCore. */
        cpuinfo_uarch_arm11 = 0x00300102,

        /** ARM Cortex-A5. */
        cpuinfo_uarch_cortex_a5 = 0x00300205,
        /** ARM Cortex-A7. */
        cpuinfo_uarch_cortex_a7 = 0x00300207,
        /** ARM Cortex-A8. */
        cpuinfo_uarch_cortex_a8 = 0x00300208,
        /** ARM Cortex-A9. */
        cpuinfo_uarch_cortex_a9 = 0x00300209,
        /** ARM Cortex-A12. */
        cpuinfo_uarch_cortex_a12 = 0x00300212,
        /** ARM Cortex-A15. */
        cpuinfo_uarch_cortex_a15 = 0x00300215,
        /** ARM Cortex-A17. */
        cpuinfo_uarch_cortex_a17 = 0x00300217,

        /** ARM Cortex-A32. */
        cpuinfo_uarch_cortex_a32 = 0x00300332,
        /** ARM Cortex-A35. */
        cpuinfo_uarch_cortex_a35 = 0x00300335,
        /** ARM Cortex-A53. */
        cpuinfo_uarch_cortex_a53 = 0x00300353,
        /** ARM Cortex-A55 revision 0 (restricted dual-issue capabilities
           compared to revision 1+). */
        cpuinfo_uarch_cortex_a55r0 = 0x00300354,
        /** ARM Cortex-A55. */
        cpuinfo_uarch_cortex_a55 = 0x00300355,
        /** ARM Cortex-A57. */
        cpuinfo_uarch_cortex_a57 = 0x00300357,
        /** ARM Cortex-A65. */
        cpuinfo_uarch_cortex_a65 = 0x00300365,
        /** ARM Cortex-A72. */
        cpuinfo_uarch_cortex_a72 = 0x00300372,
        /** ARM Cortex-A73. */
        cpuinfo_uarch_cortex_a73 = 0x00300373,
        /** ARM Cortex-A75. */
        cpuinfo_uarch_cortex_a75 = 0x00300375,
        /** ARM Cortex-A76. */
        cpuinfo_uarch_cortex_a76 = 0x00300376,
        /** ARM Cortex-A77. */
        cpuinfo_uarch_cortex_a77 = 0x00300377,
        /** ARM Cortex-A78. */
        cpuinfo_uarch_cortex_a78 = 0x00300378,

        /** ARM Neoverse N1. */
        cpuinfo_uarch_neoverse_n1 = 0x00300400,
        /** ARM Neoverse E1. */
        cpuinfo_uarch_neoverse_e1 = 0x00300401,
        /** ARM Neoverse V1. */
        cpuinfo_uarch_neoverse_v1 = 0x00300402,
        /** ARM Neoverse N2. */
        cpuinfo_uarch_neoverse_n2 = 0x00300403,
        /** ARM Neoverse V2. */
        cpuinfo_uarch_neoverse_v2 = 0x00300404,

        /** ARM Cortex-X1. */
        cpuinfo_uarch_cortex_x1 = 0x00300501,
        /** ARM Cortex-X2. */
        cpuinfo_uarch_cortex_x2 = 0x00300502,
        /** ARM Cortex-X3. */
        cpuinfo_uarch_cortex_x3 = 0x00300503,

        /** ARM Cortex-A510. */
        cpuinfo_uarch_cortex_a510 = 0x00300551,
        /** ARM Cortex-A710. */
        cpuinfo_uarch_cortex_a710 = 0x00300571,
        /** ARM Cortex-A715. */
        cpuinfo_uarch_cortex_a715 = 0x00300572,

        /** Qualcomm Scorpion. */
        cpuinfo_uarch_scorpion = 0x00400100,
        /** Qualcomm Krait. */
        cpuinfo_uarch_krait = 0x00400101,
        /** Qualcomm Kryo. */
        cpuinfo_uarch_kryo = 0x00400102,
        /** Qualcomm Falkor. */
        cpuinfo_uarch_falkor = 0x00400103,
        /** Qualcomm Saphira. */
        cpuinfo_uarch_saphira = 0x00400104,

        /** Nvidia Denver. */
        cpuinfo_uarch_denver = 0x00500100,
        /** Nvidia Denver 2. */
        cpuinfo_uarch_denver2 = 0x00500101,
        /** Nvidia Carmel. */
        cpuinfo_uarch_carmel = 0x00500102,

        /** Samsung Exynos M1 (Exynos 8890 big cores). */
        cpuinfo_uarch_exynos_m1 = 0x00600100,
        /** Samsung Exynos M2 (Exynos 8895 big cores). */
        cpuinfo_uarch_exynos_m2 = 0x00600101,
        /** Samsung Exynos M3 (Exynos 9810 big cores). */
        cpuinfo_uarch_exynos_m3 = 0x00600102,
        /** Samsung Exynos M4 (Exynos 9820 big cores). */
        cpuinfo_uarch_exynos_m4 = 0x00600103,
        /** Samsung Exynos M5 (Exynos 9830 big cores). */
        cpuinfo_uarch_exynos_m5 = 0x00600104,

        /* Deprecated synonym for Cortex-A76 */
        cpuinfo_uarch_cortex_a76ae = 0x00300376,
        /* Deprecated names for Exynos. */
        cpuinfo_uarch_mongoose_m1 = 0x00600100,
        cpuinfo_uarch_mongoose_m2 = 0x00600101,
        cpuinfo_uarch_meerkat_m3 = 0x00600102,
        cpuinfo_uarch_meerkat_m4 = 0x00600103,

        /** Apple A6 and A6X processors. */
        cpuinfo_uarch_swift = 0x00700100,
        /** Apple A7 processor. */
        cpuinfo_uarch_cyclone = 0x00700101,
        /** Apple A8 and A8X processor. */
        cpuinfo_uarch_typhoon = 0x00700102,
        /** Apple A9 and A9X processor. */
        cpuinfo_uarch_twister = 0x00700103,
        /** Apple A10 and A10X processor. */
        cpuinfo_uarch_hurricane = 0x00700104,
        /** Apple A11 processor (big cores). */
        cpuinfo_uarch_monsoon = 0x00700105,
        /** Apple A11 processor (little cores). */
        cpuinfo_uarch_mistral = 0x00700106,
        /** Apple A12 processor (big cores). */
        cpuinfo_uarch_vortex = 0x00700107,
        /** Apple A12 processor (little cores). */
        cpuinfo_uarch_tempest = 0x00700108,
        /** Apple A13 processor (big cores). */
        cpuinfo_uarch_lightning = 0x00700109,
        /** Apple A13 processor (little cores). */
        cpuinfo_uarch_thunder = 0x0070010A,
        /** Apple A14 / M1 processor (big cores). */
        cpuinfo_uarch_firestorm = 0x0070010B,
        /** Apple A14 / M1 processor (little cores). */
        cpuinfo_uarch_icestorm = 0x0070010C,
        /** Apple A15 / M2 processor (big cores). */
        cpuinfo_uarch_avalanche = 0x0070010D,
        /** Apple A15 / M2 processor (little cores). */
        cpuinfo_uarch_blizzard = 0x0070010E,

        /** Cavium ThunderX. */
        cpuinfo_uarch_thunderx = 0x00800100,
        /** Cavium ThunderX2 (originally Broadcom Vulkan). */
        cpuinfo_uarch_thunderx2 = 0x00800200,

        /** Marvell PJ4. */
        cpuinfo_uarch_pj4 = 0x00900100,

        /** Broadcom Brahma B15. */
        cpuinfo_uarch_brahma_b15 = 0x00A00100,
        /** Broadcom Brahma B53. */
        cpuinfo_uarch_brahma_b53 = 0x00A00101,

        /** Applied Micro X-Gene. */
        cpuinfo_uarch_xgene = 0x00B00100,

        /* Hygon Dhyana (a modification of AMD Zen for Chinese market). */
        cpuinfo_uarch_dhyana = 0x01000100,

        /** HiSilicon TaiShan v110 (Huawei Kunpeng 920 series processors). */
        cpuinfo_uarch_taishan_v110 = 0x00C00100,
    };
}