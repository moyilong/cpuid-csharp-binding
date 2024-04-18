using Dragon.CpuInfo.libCpuInfo;
using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    public class BindingYamlDataCpu
    {
        [YamlMember(Alias = "model")]
        public string Model { get; set; }

        [YamlMember(Alias = "slotes")]
        public int Slotes { get; set; }

        [YamlMember(Alias = "cores")]
        public int Cores { get; set; }

        [YamlMember(Alias = "threads")]
        public int Threads { get; set; }

        [YamlMember(Alias = "vendor")]
        public CpuInfoVendor Vendor { get; set; }

        [YamlMember(Alias = "uarch")]
        public CpuInfoUArch UArch { get; set; }

        [YamlMember(Alias = "caches")]
        public BindingYamlDataCache[] Caches { get; set; }

        [YamlMember(Alias = "clusters")]
        public BindingYamlDataCluster[] Clusters { get; set; }

        [YamlMember(Alias = "features")]
        public BindingYamlDataFeature[] Features { get; set; }
    }
}