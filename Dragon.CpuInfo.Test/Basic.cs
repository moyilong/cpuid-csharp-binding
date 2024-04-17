using Dragon.CpuInfo;
using Dragon.CpuInfo.libCpuInfo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace libCpuId.Test
{
    public class CacheInfoTest
    {
        [SetUp]
        public void SetUp()
        {
            CpuInfoManaged.Initialize();
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

    public class BasicTest
    {
        [SetUp]
        public void SetUp()
        {
            CpuInfoManaged.Initialize();
        }

        [Test]
        public void FastGet()
        {
            foreach (var i in from i in typeof(CpuInfoNative).GetMethods()
                              where i.IsConstructor == false
                              where i.IsStatic
                              where i.GetParameters().Length == 0
                              where i.ReturnType != typeof(void)
                              select i)
            {
                Console.WriteLine($"{i.Name} = {i.Invoke(null, [])?.ToString()}");
            }
        }

        [Test]
        public void Features()
        {
            Console.WriteLine(string.Join(Environment.NewLine, CpuInfoManaged.CpuFeatures));
        }

        [Test]
        public void FeatureMapper()
        {
            foreach (var i in CpuInfoManaged.CpuFeatureMappers)
            {
                Console.WriteLine(i.ToString());
            }
        }

        [Test]
        public void CurrentFeatureMapper()
        {
            foreach (var i in CpuInfoManaged.CpuFeatureCurrentArch)
            {
                Console.WriteLine(i.ToString());
            }
        }

        [Test]
        public void Vendor()
        {
            Console.WriteLine(CpuInfoManaged.CpuVendor);
        }

        [Test]
        public void Uarch()
        {
            Console.WriteLine(CpuInfoManaged.CpuUArch);
        }

        [Test]
        public void Model()
        {
            Console.WriteLine($"{CpuInfoManaged.CpuModel} CPU:{CpuInfoManaged.CpuCounts} Cores:{CpuInfoManaged.CpuCores} Threads:{CpuInfoManaged.CpuThreads}");
            Console.WriteLine($"Caches:{CpuInfoManaged.CpuCaches}");
            Console.WriteLine(CpuInfoManaged.BindingVersion);
        }

        private static readonly CpuInfoVendor[] vendors = [.. Enum.GetValues<CpuInfoVendor>()];

        [TestCaseSource(nameof(vendors))]
        public void VendorToString(CpuInfoVendor vendor)
        {
            Console.WriteLine(vendor.GetCpuVendorString());
        }

        private static readonly CpuInfoUArch[] uarchs = [.. Enum.GetValues<CpuInfoUArch>()];

        [TestCaseSource(nameof(uarchs))]
        public void UarchToString(CpuInfoUArch vendor)
        {
            Console.WriteLine(vendor.GetCpuUarchString());
        }
    }
}