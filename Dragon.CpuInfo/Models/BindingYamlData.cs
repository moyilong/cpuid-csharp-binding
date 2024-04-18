using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    public class BindingYamlData
    {
        [YamlMember(Alias = "lib_version")]
        public string LibVersion { get; set; }

        [YamlMember(Alias = "arch_string")]
        public string ArchString { get; set; }

        [YamlMember(Alias = "cpu")]
        public BindingYamlDataCpu Cpu { get; set; }
    }
}