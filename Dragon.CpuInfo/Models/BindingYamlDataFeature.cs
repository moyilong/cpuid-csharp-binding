using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    /// <summary>
    /// Binding Yaml feature data
    /// </summary>
    public class BindingYamlDataFeature
    {
        /// <summary>
        /// Stat (yes or no)
        /// </summary>
        [YamlMember(Alias = "stat")]
        public bool Stat { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Converted name
        /// </summary>
        [YamlMember(Alias = "xname")]
        public string XName { get; set; }

        /// <summary>
        /// Function name
        /// </summary>
        [YamlMember(Alias = "funcname")]
        public string FuncName { get; set; }

        /// <summary>
        /// From arch
        /// </summary>
        [YamlMember(Alias = "arch")]
        public string Arch { get; set; }
    }
}