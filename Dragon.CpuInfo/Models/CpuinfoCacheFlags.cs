using System;

namespace Dragon.CpuInfo.libCpuInfo
{
    /// <summary>
    /// Cpu cache flags
    /// </summary>
    [Flags]
    public enum CpuinfoCacheFlags : UInt32
    {
        /// <summary>
        /// Unified
        /// </summary>
        CacheUnified = 0x00000001,
        /// <summary>
        /// Inclusice
        /// </summary>
        CacheInclusivce = 0x00000002,
        /// <summary>
        /// Complex indexing
        /// </summary>
        ComplexIndexing = 0x00000004,
    }

}