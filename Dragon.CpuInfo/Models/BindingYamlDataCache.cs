using Dragon.CpuInfo.libCpuInfo;
using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    /// <summary>
    /// Cache info
    /// </summary>
    public class BindingYamlDataCache
    {
        /// <summary>
        /// Name
        /// </summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Size of Cache
        /// </summary>
        [YamlMember(Alias = "size")]
        public long Size { get; set; }

        /// <summary>
        /// Associativity
        /// </summary>
        [YamlMember(Alias = "associativity")]
        public long Associativity { get; set; }

        /// <summary>
        /// Sets
        /// </summary>
        [YamlMember(Alias = "sets")]
        public long Sets { get; set; }

        /// <summary>
        /// Partitions
        /// </summary>
        [YamlMember(Alias = "partitions")]
        public long Partitions { get; set; }

        /// <summary>
        /// LineSize
        /// </summary>
        [YamlMember(Alias = "line_size")]
        public long LineSize { get; set; }

        /// <summary>
        /// Flags
        /// </summary>
        [YamlMember(Alias = "flags")]
        public CpuinfoCacheFlags Flags { get; set; }

        /// <summary>
        /// Used Processor start
        /// </summary>
        [YamlMember(Alias = "processor_start")]
        public long ProcessorStart { get; set; }

        /// <summary>
        /// Used processor count
        /// </summary>
        [YamlMember(Alias = "processor_count")]
        public long ProcessorCount { get; set; }
    }
}