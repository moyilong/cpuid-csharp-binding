using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{

    public class BindingYamlDataThread
    {
        [YamlMember(Alias = "id")]
        public long Id { get; set; }
    }
}