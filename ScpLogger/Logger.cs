using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WinSCP;

namespace ScpLogger
{
    public class Logger
    {
        public static string FileName = $"Log{DateTime.UtcNow.ToString("yyyy-dd-M--HH-mm-ss")}.txt";

        public static string HostName { get; set; }
        public static string LocalPath { get; set; }
        public static string RemotePath { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static int PortNumber { get; set; }

        private static List<string> LogSum = new List<string>();
        private static string GetExternalIp()
        {

            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            var externalIp = IPAddress.Parse(externalIpString);

            return externalIp.ToString();
        }

        public static void UploadLog()
        {
            ScpUploader uploader = new ScpUploader(HostName,UserName,Password,PortNumber,true);
            File.WriteAllLines(FileName, LogSum);
            uploader.Upload(LocalPath,RemotePath);
        }

        public static string Info(string message)
        {
            string result =
                $"{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{nameof(Info)}|| {message}";
            LogSum.Add(result);
            return result;
        }

        public static string Warning(string message)
        {
            string result =
                $"{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{nameof(Warning)}|| {message}";
            LogSum.Add(result);
            return result;
        }

        public static string Error(string message)
        {
            string result =
                $"{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{nameof(Error)}|| {message}";
            LogSum.Add(result);
            return result;
        }
    }
}
