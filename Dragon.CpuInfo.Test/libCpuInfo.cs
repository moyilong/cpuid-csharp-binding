using Dragon.CpuInfo;
using Dragon.CpuInfo.libCpuInfo;
using System;
using System.Linq;

namespace libCpuId.Test
{
    public class libCpuInfo
    {
        [SetUp]
        public void SetUp()
        {
            if (!CpuInfoNative.binding_cpuinfo_initialize())
                Assert.Fail();
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

        [Test]
        public void CacheInfo()
        {
            Console.WriteLine(CpuInfoManaged.CpuCacheInfo.ToString());
        }

        private static readonly CpuInfoVendor[] vendors = Enum.GetValues<CpuInfoVendor>().ToArray();

        [TestCaseSource(nameof(vendors))]
        public void VendorToString(CpuInfoVendor vendor)
        {
            Console.WriteLine(vendor.GetCpuVendorString());
        }



        private static readonly CpuInfoUArch[] uarchs = Enum.GetValues<CpuInfoUArch>().ToArray();

        [TestCaseSource(nameof(uarchs))]
        public void UarchToString(CpuInfoUArch vendor)
        {
            Console.WriteLine(vendor.GetCpuUarchString());
        }
    }
}