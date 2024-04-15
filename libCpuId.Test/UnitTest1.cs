using Dragon.CpuInfo;
using Dragon.CpuInfo.libCpuInfo;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace libCpuId.Test
{
    public class Tests
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
                              where i.IsConstructor ==false
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
            Console.WriteLine(string.Join(Environment.NewLine,CpuInfo.CpuFeatures));
        }

        [Test]
        public void Vendor()
        {
            Console.WriteLine(CpuInfo.CpuVendor);
        }
        [Test]
        public void Uarch()
        {
            Console.WriteLine(CpuInfo.CpuUArch);
        }

        [Test]
        public void Model()
        {
            Console.WriteLine($"{CpuInfo.CpuModel} CPU:{CpuInfo.CpuCounts} Cores:{CpuInfo.CpuCores} Threads:{CpuInfo.CpuThreads}");
            Console.WriteLine($"Caches:{CpuInfo.CpuCaches} L1:{CpuInfo.CpuL1ICaches}I+{CpuInfo.CpuL1DCaches}D");
        }
    }

    public class Runtime
    {
        [Test]
        public void RuntimeArch()
        {
            Console.WriteLine(RuntimeInformation.OSArchitecture);
        }
    }
}