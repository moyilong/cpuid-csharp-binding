using Dragon.CpuInfo.libCpuInfo;
using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    /// <summary>
    /// Cpu Core
    /// </summary>
    public class BindingYamlDataCore
    {
        /// <summary>
        /// Id
        /// </summary>
        [YamlMember(Alias = "id")]
        public long Id { get; set; }

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
        /// Threads(Processors)
        /// </summary>
        [YamlMember(Alias = "threads")]
        public BindingYamlDataThread[] Threads { get; set; }
    }
}