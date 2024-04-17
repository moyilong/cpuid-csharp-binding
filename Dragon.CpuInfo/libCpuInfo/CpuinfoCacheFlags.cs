using System;

namespace Dragon.CpuInfo.libCpuInfo
{
    [Flags]
    public enum CpuinfoCacheFlags : UInt32
    {
        CacheUnified = 0x00000001,
        CacheInclusivce = 0x00000002,
        ComplexIndexing = 0x00000004,
    }

}