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
    /// <summary>
    /// Binding bridge API
    /// </summary>
    public static class BindingBridge
    {
        private const string dllName = "cpuinfo-binding";

        /// <summary>
        /// Get YAML data api
        /// </summary>
        /// <param name="yaml">yaml write buffer</param>
        /// <param name="max">max size</param>
        /// <returns></returns>
        [DllImport(dllName)]
        internal static extern Int32 copy_yaml_api(byte[] yaml, UInt32 max);

        /// <summary>
        /// Get YAML buffer size
        /// </summary>
        /// <returns></returns>
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
        /// Clear data cache
        /// </summary>
        public static void ClearCache()
        {
            dataCache = null;
        }

        private static Deserializer deserializer = new();

        /// <summary>
        /// Get Yaml Data
        /// </summary>
        /// <returns></returns>
        public static BindingYamlData Parse()
        {
            return deserializer.Deserialize<BindingYamlData>(GetYaml());
        }
    }
}