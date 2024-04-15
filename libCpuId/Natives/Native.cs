using System;
using System.Runtime.InteropServices;

namespace libCpuInfo.Natives
{
    /// <summary>
    /// API https://github.com/pytorch/cpuinfo/blob/main/include/cpuinfo.h
    /// </summary>
    public static partial class CpuInfoNative
    {
        private const string libName = "cpuinfo-binding";

        private const CallingConvention callingConvention = CallingConvention.Cdecl;

        private const CharSet callingCharSet = CharSet.Auto;



        [DllImport(libName, CallingConvention = callingConvention, CharSet = callingCharSet)]
        public static extern UInt32 binding_cpuinfo_get_name(ref byte[] name);

        [DllImport(libName, CallingConvention = callingConvention, CharSet = callingCharSet)]
        public static extern cpuinfo_vendor binding_cpuinfo_vendor();

        [DllImport(libName, CallingConvention = callingConvention, CharSet = callingCharSet)]
        public static extern cpuinfo_uarch binding_cpuinfo_uarch();

        [DllImport(libName, CallingConvention = callingConvention, CharSet = callingCharSet)]
        public static extern bool binding_cpuinfo_initialize();

        [DllImport(libName, CallingConvention = callingConvention, CharSet = callingCharSet)]
        public static extern void binding_cpuinfo_deinilize();
    }
}