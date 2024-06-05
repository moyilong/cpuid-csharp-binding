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
        /// <summary>
        /// Cache of program data
        /// </summary>
        private static string dataCache = null;

        internal static string GetYaml()
        {
            if (dataCache == null)
            {
                if (UseLibCall)
                {

                    dataCache = NativeBridge.GetYamlBySo();
                }
                else
                {
                    dataCache = ProcCallGetYaml();
                    
                }
            }
            return dataCache;
        }

        /// <summary>
        /// Use Libcall or Execute Call
        /// </summary>
        public static bool UseLibCall
        {
            get
            {
#if NET5_0_OR_GREATER
                if (OperatingSystem.IsLinux())
                    return false;
                return true;
#else
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    return false;
                return true;
#endif
            }
        }

        private static string ProcCallGetYaml()
        {
            var location = Assembly.GetExecutingAssembly().Location ?? Assembly.GetEntryAssembly().Location?? Environment.CurrentDirectory;

            var path = Path.GetDirectoryName(location);

            var ridPath = Path.Combine(path, "runtimes", $"linux-{RuntimeInformation.OSArchitecture.ToString().ToLower()}","native", "cpuinfo-binding");

            using var cmd = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ridPath,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
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