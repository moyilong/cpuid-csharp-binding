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
        private const string libName = "cpuinfo";

        [DllImport(libName)]
        public static extern bool binding_cpuinfo_initialize();

        [DllImport(libName)]
        public static extern void binding_cpuinfo_deinilize();

        [DllImport(libName)]
        public static extern cpuinfo_vendor binding_cpuinfo_vendor();

        [DllImport(libName)]
        public static extern cpuinfo_uarch binding_cpuinfo_uarch();

        [DllImport(libName)]
        public static extern Int32 binding_cpuinfo_get_name(byte[] buffer);
    }
}