using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Korduene
{
    public static class DotNetInfo
    {
        public static string SdkVersion { get; set; }
        public static string SdkPath { get; set; }
        public static string PacksPath { get; set; }

        static DotNetInfo()
        {
            GetVersion();
            GetSdkPath();
            GetPacksPath();
        }

        public static string GetPackPath(string sdk)
        {
            var name = GetPackDirBySDK(sdk);
            var desktopPackDir = System.IO.Path.Combine(PacksPath, name);
            var dirs = new Dictionary<long, DirectoryInfo>();

            foreach (var dir in new DirectoryInfo(desktopPackDir).GetDirectories())
            {
                dirs.Add(long.Parse(new string(dir.Name.Where(x => char.IsDigit(x)).ToArray())), dir);
            }

            return System.IO.Path.Combine(PacksPath, name, dirs.OrderByDescending(x => x.Key).FirstOrDefault().Value.Name);
        }

        private static void GetVersion()
        {
            var process = Process.Start(new ProcessStartInfo("cmd", "/c dotnet --version") { RedirectStandardOutput = true, CreateNoWindow = true, UseShellExecute = false });

            while (!process.StandardOutput.EndOfStream)
            {
                SdkVersion = process.StandardOutput.ReadToEnd().Trim();
            }
        }

        private static void GetSdkPath()
        {
            var path = string.Empty;
            var process = Process.Start(new ProcessStartInfo("cmd", "/c dotnet --list-sdks") { RedirectStandardOutput = true, CreateNoWindow = true });

            while (!process.StandardOutput.EndOfStream)
            {
                path += process.StandardOutput.ReadLine() + Environment.NewLine;
            }

            path = path.Split(Environment.NewLine).FirstOrDefault(x => x.StartsWith(SdkVersion));

            SdkPath = path.Substring(SdkVersion.Length, path.Length - SdkVersion.Length).Trim().Trim('[', ']');
        }

        private static void GetPacksPath()
        {
            PacksPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(SdkPath), "packs");
        }

        private static string GetPackDirBySDK(string sdk)
        {
            switch (sdk)
            {
                case "Microsoft.NET.Sdk":
                    return "Microsoft.NETCore.App.Ref";
                case "Microsoft.NET.Sdk.WindowsDesktop":
                    return "Microsoft.WindowsDesktop.App.Ref";
                default:
                    return "Microsoft.NETCore.App.Ref";
            }
        }
    }
}
