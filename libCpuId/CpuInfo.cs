using Dragon.CpuInfo.libCpuInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("cpuinfo-cli")]
[assembly: InternalsVisibleTo("libCpuId.Test")]

namespace Dragon.CpuInfo
{
    public static class CpuInfo
    {
        static CpuInfo()
        {
        }

        /// <summary>
        /// Cpu instruction set
        /// </summary>
        [SuppressMessage("Style", "IDE0301", Justification = "<挂起>")]
        public static IEnumerable<string> CpuFeatures => from i in typeof(CpuInfoNative).GetMethods()
                                                         where i.Name.Contains("_has_")
                                                         let feature = string.Join('_', i.Name.Split('_').Skip(4))
                                                         where (bool)i.Invoke(null, Array.Empty<object>())
                                                         select feature;

        /// <summary>
        /// CPU core arch
        /// </summary>
        public static cpuinfo_uarch CpuUArch => CpuInfoNative.binding_cpuinfo_uarch();

        /// <summary>
        /// CPU Vendor
        /// </summary>
        public static cpuinfo_vendor CpuVendor => CpuInfoNative.binding_cpuinfo_vendor();

        /// <summary>
        /// CPU Model
        /// </summary>
        public static string CpuModel
        {
            get
            {
                byte[] buffer = new byte[1024];
                var len = CpuInfoNative.binding_cpuinfo_get_name(buffer);
                if (len == -1)
                    return "invalid-result";
                return Encoding.ASCII.GetString(buffer.Take(len).ToArray()).Trim();
            }
        }

        /// <summary>
        /// Count of CPU (slote)
        /// </summary>
        public static int CpuCounts => (int)CpuInfoNative.binding_cpuinfo_get_packages_count();

        /// <summary>
        /// Count of Cores
        /// </summary>
        public static int CpuCores => (int)CpuInfoNative.binding_cpuinfo_get_cores_count();

        /// <summary>
        /// Count of Threads
        /// </summary>
        public static int CpuThreads => (int)CpuInfoNative.binding_cpuinfo_get_processors_count();

        /// <summary>
        /// Total Cache of CPU
        /// </summary>
        public static UInt32 CpuCaches => CpuInfoNative.binding_cpuinfo_get_max_cache_size();

        public static UInt32 CpuL1ICaches => CpuInfoNative.binding_cpuinfo_get_l1i_caches_count();
        public static UInt32 CpuL1DCaches => CpuInfoNative.binding_cpuinfo_get_l1d_caches_count();
    }
}