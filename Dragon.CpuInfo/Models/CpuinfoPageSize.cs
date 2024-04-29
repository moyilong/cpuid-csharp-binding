namespace Dragon.CpuInfo.libCpuInfo
{
    /// <summary>
    /// Cpuinfo Page size
    /// </summary>
    public enum CpuinfoPageSize
    {
        /// <summary>
        /// 4k
        /// </summary>
        page_4k = 0x1000,
        /// <summary>
        /// 1m
        /// </summary>
        page_1m = 0x100000,
        /// <summary>
        /// 2m
        /// </summary>
        page_2m = 0x200000,
        /// <summary>
        /// 4m
        /// </summary>
        page_4m = 0x400000,
        /// <summary>
        /// 16m
        /// </summary>
        page_16m = 0x1000000,
        /// <summary>
        /// 1g
        /// </summary>
        page_1g = 0x40000000
    }
}