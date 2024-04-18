using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    public class BindingYamlDataFeature
    {
        [YamlMember(Alias = "stat")]
        public bool Stat { get; set; }

        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "xname")]
        public string XName { get; set; }

        [YamlMember(Alias = "funcname")]
        public string FuncName { get; set; }

        [YamlMember(Alias = "arch")]
        public string Arch { get; set; }
    }
}