using Dragon.CpuInfo.libCpuInfo;
using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    public class BindingYamlDataCache
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "size")]
        public long Size { get; set; }

        [YamlMember(Alias = "associativity")]
        public long Associativity { get; set; }

        [YamlMember(Alias = "sets")]
        public long Sets { get; set; }

        [YamlMember(Alias = "partitions")]
        public long Partitions { get; set; }

        [YamlMember(Alias = "line_size")]
        public long LineSize { get; set; }

        [YamlMember(Alias = "flags")]
        public CpuinfoCacheFlags Flags { get; set; }

        [YamlMember(Alias = "processor_start")]
        public long ProcessorStart { get; set; }

        [YamlMember(Alias = "processor_count")]
        public long ProcessorCount { get; set; }
    }
}