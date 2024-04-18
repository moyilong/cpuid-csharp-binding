using Dragon.CpuInfo;
using Dragon.CpuInfo.libCpuInfo;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace libCpuId.Test
{
    public class BasicTest
    {
        [Test]
        public void LoadData()
        {
            Console.WriteLine(BindingBridge.GetYaml());
        }
        [Test]
        public void LoadObj()
        {
            Console.WriteLine(JObject.FromObject(BindingBridge.Parse()).ToString());
        }

        [Test]
        public void SizeUsed()
        {
            var definedSize = BindingBridge.copy_yaml_size();
            var usedSize = BindingBridge.GetYaml().Length;

            Console.WriteLine($"{usedSize} / {definedSize} / {Math.Round(100.0f * usedSize / definedSize)}");
        }

    }
}