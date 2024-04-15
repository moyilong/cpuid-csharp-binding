// See https://aka.ms/new-console-template for more information
using Dragon.CpuInfo.libCpuInfo;
using System;
using System.Runtime.InteropServices;

namespace libCpuInfo.Test.Cli
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                Console.WriteLine("Hello, World! " + RuntimeInformation.OSArchitecture);

                CpuInfoNative.binding_cpuinfo_initialize();

            }catch(Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }
    }
}