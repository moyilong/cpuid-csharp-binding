
namespace Dragon.CpuInfo
{
    /// <summary>
    /// Cpu Instruction set feature
    /// </summary>
    public sealed class CpuInstructionSetFeature
    {
        /// <summary>
        /// Readable name
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// libcpuinfo original name
        /// </summary>
        public string OriginalName { get; internal set; }

        /// <summary>
        /// Feature Stat
        /// </summary>
        public bool Stat { get; internal set; }

        /// <summary>
        /// Allowed Arch
        /// </summary>
        public string Arch { get; internal set; }

        public override string ToString()
        {
            return $"({Arch}){Name}={Stat}";
        }
    }
}