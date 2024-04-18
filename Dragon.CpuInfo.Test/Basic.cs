using Dragon.CpuInfo;
using Dragon.CpuInfo.libCpuInfo;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace libCpuId.Test
{
    public class BasicTest
    {
        [SetUp]
        public void SetUp()
        {
            if (!CpuInfoNative.binding_cpuinfo_initialize())
                Assert.Fail();
        }

        private static readonly MethodInfo[] Methods = (from i in typeof(CpuInfoNative).GetMethods()
                                                        where i.IsConstructor == false
                                                        where i.IsStatic
                                                        where i.Name.Contains("_has_")
                                                        where i.GetParameters().Length == 0
                                                        where i.ReturnType != typeof(void)
                                                        select i).ToArray();

        [TestCaseSource(nameof(Methods))]
        public void FeaturesApi(MethodInfo i)
        {
            Console.WriteLine($"{i.Name} = {i.Invoke(null, [])?.ToString()}");
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
            if (!(from i in CpuInfoManaged.CpuFeatureCurrentArch
                  where !i.Stat
                  select true).Any())
                Assert.Fail();
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