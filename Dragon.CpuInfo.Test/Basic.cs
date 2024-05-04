using Dragon.CpuInfo;
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
        public void CurrentFeature()
        {
            var yaml = BindingBridge.Parse();
            Console.WriteLine(string.Join(Environment.NewLine, from i in yaml.Cpu.Features
                                                               where i.Arch == yaml.ArchString
                                                               orderby i.Name
                                                               select $"{i.Name} / {i.XName} = {i.Stat}"));
        }

        [Test]
        public void CacheInfo()
        {
            Console.WriteLine(string.Join(Environment.NewLine, from b in BindingBridge.Parse().Cpu.Caches
                                                               group b by (b.Name, b.Size) into g
                                                               group g by g.Key.Name into h
                                                               select $"{h.Key} {string.Join('+', from c in h select $"{Math.Round(c.Key.Size / 1024.0f)}kb x {c.Count()}")}"));
        }

        [Test]
        public void CacheInfoReal()
        {
            Console.WriteLine(new JArray(from b in BindingBridge.Parse().Cpu.Caches
                                         select JObject.FromObject(b)).ToString());
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