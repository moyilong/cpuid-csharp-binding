using Dragon.CpuInfo.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using YamlDotNet.Serialization;

[assembly: InternalsVisibleTo("Dragon.CpuInfo.Test")]

namespace Dragon.CpuInfo
{
    /// <summary>
    /// Binding bridge API
    /// </summary>
    public static class BindingBridge
    {
        private static string dataCache = null;

        internal static string GetYaml()
        {
            if (dataCache == null)
            {
                if (UseLibCall)
                {
                }
                else
                {
                    dataCache = NativeBridge.GetYamlBySo();
                }
            }
            return dataCache;
        }

        private static bool? forceUseLibCall = false;

        /// <summary>
        /// Use Libcall or Execute Call
        /// </summary>
        public static bool UseLibCall
        {
            get
            {
                if (forceUseLibCall != null)
                {
                    return forceUseLibCall == true;
                }
                else
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        return true;
                    return false;
                }
            }
        }

        private static string ProcCallGetYaml()
        {
            var location = Assembly.GetExecutingAssembly().Location ?? Assembly.GetEntryAssembly().Location;

            var path = Path.GetDirectoryName(location);

            string osname;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                osname = "win";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                osname = "linux";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                osname = "osx";
            else
                throw new PlatformNotSupportedException();

            var ridPath = Path.Combine(path, "runtimes", "native", $"{osname}-{RuntimeInformation.OSArchitecture.ToString().ToLower()}", "cpuinfo-binding");

            using var cmd = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ridPath,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow=true,
                    UseShellExecute=false
                }
            };
            cmd.Start();
            cmd.WaitForExit();
            return cmd.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// Clear data cache
        /// </summary>
        public static void ClearCache()
        {
            dataCache = null;
        }

        private static readonly Deserializer deserializer = new();

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