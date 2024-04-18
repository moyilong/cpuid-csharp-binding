using Dragon.CpuInfo.libCpuInfo;
using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    public class BindingYamlDataCluster
    {
        [YamlMember(Alias = "id")]
        public long Id { get; set; }

        [YamlMember(Alias = "vendor")]
        public CpuInfoVendor Vendor { get; set; }

        [YamlMember(Alias = "uarch")]
        public CpuInfoUArch UArch { get; set; }

        [YamlMember(Alias = "cores")]
        public BindingYamlDataCore[] Cores { get; set; }
    }
}