using Dragon.CpuInfo.libCpuInfo;
using System;
using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    /// <summary>
    /// Cpu Info
    /// </summary>
    public class BindingYamlDataCpu
    {
        /// <summary>
        /// Model
        /// </summary>
        [YamlMember(Alias = "model")]
        public string Model { get; set; }

        /// <summary>
        /// Slotes count
        /// </summary>
        [YamlMember(Alias = "slotes")]
        public int Slotes { get; set; }

        /// <summary>
        /// Cores count
        /// </summary>
        [YamlMember(Alias = "cores")]
        public int Cores { get; set; }

        /// <summary>
        /// Threads (Processors) Count
        /// </summary>
        [YamlMember(Alias = "threads")]
        public int Threads { get; set; }

        /// <summary>
        /// Vendor
        /// </summary>
        [YamlMember(Alias = "vendor")]
        public CpuInfoVendor Vendor { get; set; }

        /// <summary>
        /// UArch
        /// </summary>
        [YamlMember(Alias = "uarch")]
        public CpuInfoUArch UArch { get; set; }

        /// <summary>
        /// Caches
        /// </summary>
        [YamlMember(Alias = "caches")]
        [Obsolete("cache info is still in test")]
        public BindingYamlDataCache[] Caches { get; set; }

        /// <summary>
        /// Clusters
        /// </summary>
        [YamlMember(Alias = "clusters")]
        public BindingYamlDataCluster[] Clusters { get; set; }

        /// <summary>
        /// Features
        /// </summary>
        [YamlMember(Alias = "features")]
        public BindingYamlDataFeature[] Features { get; set; }
    }
}