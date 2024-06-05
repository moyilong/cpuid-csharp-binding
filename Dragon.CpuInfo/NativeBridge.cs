using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Dragon.CpuInfo
{
    [SuppressMessage("Interoperability", "SYSLIB1054", Justification = "<挂起>")]
    internal static class NativeBridge
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

        /// <summary>
        /// Get Yaml by LibCall
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        internal static string GetYamlBySo()
        {
            byte[] buffer = new byte[copy_yaml_size()];

            Int32 size = copy_yaml_api(buffer, (UInt32)buffer.Length);
            if (size == -1)
                throw new Exception(Encoding.UTF8.GetString(buffer).Trim());
            else
            {
                return Encoding.UTF8.GetString(buffer.Take(size).ToArray());
            }
        }
    }
}