using YamlDotNet.Serialization;

namespace Dragon.CpuInfo.Models
{
    /// <summary>
    /// libcpuinfo data
    /// </summary>
    public class BindingYamlData
    {
        /// <summary>
        /// libCpuinfo and binding-bridge version
        /// </summary>
        [YamlMember(Alias = "lib_version")]
        public string LibVersion { get; set; }
        /// <summary>
        /// Compile RID
        /// </summary>
        [YamlMember(Alias = "compile_rid")]
        public string CompileRid{ get; set; }

        /// <summary>
        /// Compile Time
        /// </summary>
        [YamlMember(Alias ="compile_time")]
        public string CompileTime { get; set; }

        /// <summary>
        /// arch string
        /// </summary>
        [YamlMember(Alias = "arch_string")]
        public string ArchString { get; set; }

        /// <summary>
        /// Cpu
        /// </summary>
        [YamlMember(Alias = "cpu")]
        public BindingYamlDataCpu Cpu { get; set; }
    }
}