using Dragon.CpuInfo.libCpuInfo;
using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    /// <summary>
    /// Cpu cluster info
    /// </summary>
    public class BindingYamlDataCluster
    {
        /// <summary>
        /// Cluster Id
        /// </summary>
        [YamlMember(Alias = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Cpu Vendor
        /// </summary>
        [YamlMember(Alias = "vendor")]
        public CpuInfoVendor Vendor { get; set; }

        /// <summary>
        /// Cpu Uarch
        /// </summary>
        [YamlMember(Alias = "uarch")]
        public CpuInfoUArch UArch { get; set; }

        /// <summary>
        /// Cpu Cores
        /// </summary>
        [YamlMember(Alias = "cores")]
        public BindingYamlDataCore[] Cores { get; set; }
    }
}