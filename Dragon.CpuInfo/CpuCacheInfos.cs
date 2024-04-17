using Dragon.CpuInfo.libCpuInfo;
using System;
using System.Linq;

namespace Dragon.CpuInfo
{
    public class CpuCachePackage
    {
        public int Counts { get; set; }

        public long Size { get; set; }
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