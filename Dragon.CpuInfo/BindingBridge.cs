using Dragon.CpuInfo.libCpuInfo;
using Dragon.CpuInfo.Models;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using YamlDotNet.Serialization;

[assembly: InternalsVisibleTo("Dragon.CpuInfo.Test")]

namespace Dragon.CpuInfo
{
    public static class BindingBridge
    {
        private const string dllName = "cpuinfo-binding";

        [DllImport(dllName)]
        internal static extern Int32 copy_yaml_api(byte[] yaml, UInt32 max);

        [DllImport(dllName)]
        internal static extern UInt32 copy_yaml_size();

        private static string dataCache = null;

        internal static string GetYaml()
        {
            if (dataCache == null)
            {
                byte[] buffer = new byte[copy_yaml_size()];

                Int32 size = copy_yaml_api(buffer, (UInt32)buffer.Length);
                if (size == -1)
                    throw new Exception(Encoding.UTF8.GetString(buffer).Trim());
                else
                {
                    dataCache = Encoding.UTF8.GetString(buffer.Take(size).ToArray());
                }
            }
            return dataCache;
        }

        /// <summary>
        /// Get Yaml Data
        /// </summary>
        /// <returns></returns>
        public static BindingYamlData Parse()
        {
            Deserializer deserializer = new();

            return deserializer.Deserialize<BindingYamlData>(GetYaml());
        }
    }
}