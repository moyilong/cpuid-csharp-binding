using Dragon.CpuInfo.libCpuInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

[assembly: InternalsVisibleTo("cpuinfo-cli")]
[assembly: InternalsVisibleTo("libCpuId.Test")]

namespace Dragon.CpuInfo
{
    /// <summary>
    /// CpuInfo
    /// </summary>
    [SuppressMessage("Style", "IDE0301")]
    public static class CpuInfo
    {
        static CpuInfo()
        {
        }

        /// <summary>
        /// Cpu instruction set
        /// </summary>
        public static IEnumerable<string> CpuFeatures => from i in CpuFeatureMappers
                                                         where i.Stat
                                                         select i.Name;

        /// <summary>
        /// Cpu instruction set
        /// </summary>
        public static IEnumerable<CpuInstructionSetFeature> CpuFeatureMappers => from i in typeof(CpuInfoNative).GetMethods()
                                                                                 where i.Name.Contains("_has_")
                                                                                 let feature = string.Join('_', i.Name.Split('_').Skip(4))
                                                                                 let convName = InstructionNameConversion(feature)
                                                                                 let arch = i.Name.Split('_')[3]
                                                                                 let stat = (bool)i.Invoke(null, Array.Empty<object>())
                                                                                 select new CpuInstructionSetFeature
                                                                                 {
                                                                                     Arch = arch,
                                                                                     Stat = stat,
                                                                                     Name = convName,
                                                                                     OriginalName = feature
                                                                                 };

        /// <summary>
        /// Current Cpu instruct set
        /// </summary>
        public static IEnumerable<CpuInstructionSetFeature> CpuFeatureCurrentArch => from i in CpuFeatureMappers
                                                                                     where i.Arch == CpuArch
                                                                                     select i;

        /// <summary>
        /// CPU core arch
        /// </summary>
        public static CpuInfoUArch CpuUArch => CpuInfoNative.binding_cpuinfo_uarch();

        /// <summary>
        /// CPU Vendor
        /// </summary>
        public static cpuinfo_vendor CpuVendor => CpuInfoNative.binding_cpuinfo_vendor();

        /// <summary>
        /// Cpu arch
        /// </summary>
        [SuppressMessage("Style", "IDE0066")]
        public static string CpuArch
        {
            get
            {
                switch (RuntimeInformation.ProcessArchitecture)
                {
                    case Architecture.Arm:
                    case Architecture.Arm64:
                        return "arm";

                    case Architecture.X86:
                    case Architecture.X64:
                        return "x86";

                    default:
                        throw new PlatformNotSupportedException();
                }
            }
        }

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
        /// CPU Model
        /// </summary>
        public static string libVersion
        {
            get
            {
                byte[] buffer = new byte[1024];
                var len = CpuInfoNative.binding_get_version(buffer);
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

        private static string InstructionNameConversion(string name)
        {
            return name.Replace("_plus", "+").Replace("_", ".");
        }

        /// <summary>
        /// Cpu Cache info
        /// </summary>
        [SuppressMessage("Style", "IDE0090")]
        public static CpuCacheInfos CpuCacheInfo => new CpuCacheInfos
        {
            L1D = CpuInfoNative.binding_cpuinfo_get_l1d_caches(),
            L1I = CpuInfoNative.binding_cpuinfo_get_l1i_caches(),
            L2 = CpuInfoNative.binding_cpuinfo_get_l2_caches(),
            L3 = CpuInfoNative.binding_cpuinfo_get_l3_caches(),
            L4 = CpuInfoNative.binding_cpuinfo_get_l4_caches()
        };

        public static string CacheInfoToString(this CpuCacheInfo info)
        {
            return string.Join(' ', from i in info.GetType().GetFields()
                                    select $"{i.Name}={i.GetValue(info)}");
        }
    }

    public class CpuCacheInfos
    {
        /// <summary>
        /// L1 instruction
        /// </summary>
        public CpuCacheInfo L1I { get; internal set; }

        /// <summary>
        /// L1 data
        /// </summary>
        public CpuCacheInfo L1D { get; internal set; }
        /// <summary>
        /// L2
        /// </summary>
        public CpuCacheInfo L2 { get; internal set; }

        /// <summary>
        /// L3
        /// </summary>
        public CpuCacheInfo L3 { get; internal set; }

        /// <summary>
        /// L4
        /// </summary>
        public CpuCacheInfo L4 { get; internal set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, from i in typeof(CpuCacheInfos).GetProperties()
                                                    select $"{i.Name} >> {((CpuCacheInfo)i.GetValue(this)).CacheInfoToString()}");
        }
    }
}