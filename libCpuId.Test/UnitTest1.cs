using libCpuInfo.Natives;
using System;
using System.Linq;
using System.Text;

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
                              where i.GetParameters().Length == 0
                              where i.ReturnType != typeof(void)
                              select i)
            {
                Console.WriteLine($"{i.Name} = {i.Invoke(null,new object[] { } )?.ToString()}");
            }
        }

        
        [Test]
        public void CpuName()
        {
            byte[] buffer = new byte[1024];
            var len = CpuInfoNative.binding_cpuinfo_get_name(ref buffer);
            Console.WriteLine(Encoding.UTF8.GetString(buffer.Take((int)len).ToArray()));
        }
    }
}