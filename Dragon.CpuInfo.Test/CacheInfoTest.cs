using Dragon.CpuInfo;
using Dragon.CpuInfo.libCpuInfo;
using Newtonsoft.Json.Linq;
using System;

namespace libCpuId.Test
{
    public class CacheInfoTest
    {
        [SetUp]
        public void SetUp()
        {
            if (!CpuInfoNative.binding_cpuinfo_initialize())
                Assert.Fail();
        }

        [Test]
        public void CacheInfo()
        {
            Console.WriteLine(CpuInfoManaged.CpuCacheInfo.ToString());
        }

#pragma warning disable CS8974 // 将方法组转换为非委托类型

        private static readonly object[][] LoadCacheInfoDatas = [
            ["l1d",CpuInfoNative.binding_cpuinfo_get_l1d_cache,CpuInfoNative.binding_cpuinfo_get_l1d_caches_count],
            ["l1i", CpuInfoNative.binding_cpuinfo_get_l1i_cache,CpuInfoNative.binding_cpuinfo_get_l1i_caches_count],
            ["l2", CpuInfoNative.binding_cpuinfo_get_l2_cache,CpuInfoNative.binding_cpuinfo_get_l2_caches_count],
            ["l3", CpuInfoNative.binding_cpuinfo_get_l3_cache,CpuInfoNative.binding_cpuinfo_get_l3_caches_count],
            ["l4", CpuInfoNative.binding_cpuinfo_get_l4_cache,CpuInfoNative.binding_cpuinfo_get_l4_caches_count],
            ];

#pragma warning restore CS8974 // 将方法组转换为非委托类型

        [TestCaseSource(nameof(LoadCacheInfoDatas))]
        public void LoadCacheInfo(string name, Func<UInt32, CpuCacheInfo> caches, Func<UInt32> counter)
        {
            JArray array = [];
            for (UInt32 n = 0; n < counter(); n++)
                array.Add(JObject.FromObject(caches(n)));
            Console.WriteLine($"{name} Count: {counter()}");
            Console.WriteLine(array.ToString());
        }

        [TestCaseSource(nameof(LoadCacheInfoDatas))]
        public void LoadCacheInfo2(string name, Func<UInt32, CpuCacheInfo> caches, Func<UInt32> counter)
        {
            Console.WriteLine(name + " " + CpuInfoManaged.ComputeCache(caches, counter));
        }
    }
}