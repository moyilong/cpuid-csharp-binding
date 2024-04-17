using System;

namespace Dragon.CpuInfo.libCpuInfo
{
    /// <summary>
    /// Cpu Cache Info
    /// </summary>
    [Serializable]
    public struct CpuCacheInfo
    {
        /// <summary>
        /// Cache size in bytes
        /// </summary>
        public UInt32 Size;

        /// <summary>
        /// Number of ways of associativity
        /// </summary>
        public UInt32 Associativity;

        /// <summary>
        /// Number of sets
        /// </summary>
        public UInt32 Sets;

        /// <summary>
        /// Number of partitions
        /// </summary>
        public UInt32 Partitons;

        /// <summary>
        /// Line size in bytes
        /// </summary>
        public UInt32 LineSize;

        /// <summary>
        /// Binary characteristics of the cache (unified cache, inclusive cache, cache with complex indexing)
        /// </summary>
        public CpuinfoCacheFlags Flags;

        /// <summary>
        /// Index of the first logical processor that shares this cache
        /// </summary>
        public UInt32 ProcessorStart;

        /// <summary>
        /// Number of logical processors that share this cache
        /// </summary>
        public UInt32 ProcessorCount;
    }
}