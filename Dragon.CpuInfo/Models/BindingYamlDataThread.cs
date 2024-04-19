using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{

    /// <summary>
    /// Cpu Processor
    /// </summary>
    public class BindingYamlDataThread
    {
        /// <summary>
        /// Id
        /// </summary>
        [YamlMember(Alias = "id")]
        public long Id { get; set; }
    }
}