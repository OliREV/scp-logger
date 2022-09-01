using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WinSCP;

namespace ScpLogger
{
    public class Logger
    {
        public static string FileName = $"Log{DateTime.UtcNow:yyyy-dd-M--HH-mm-ss}.txt";

        /// <summary>
        /// Determines whether the file needs to be saved to a remote machine or only locally. True if only locally.
        /// </summary>
        public static bool LogOnlyToLocalPath { get; set; }

        private static string _assemblyName = "";
        /// <summary>
        /// Assembly name to determine the logging environment "Unknown program" if not set.
        /// </summary>
        public static string AssemblyName
        {
            get
            { 
               return _assemblyName;
            }
            set
            {
                if (value == null)
                    _assemblyName = "Unknown program";
                else
                    _assemblyName = value;
            }
        }
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
            ScpUploader uploader = new ScpUploader(HostName, UserName, Password, PortNumber, true);
            File.WriteAllLines(FileName, LogSum);
            if (LogOnlyToLocalPath)
                File.WriteAllLines(FileName, LogSum);
            else
                uploader.Upload(LocalPath, RemotePath);
        }

        public static string Info(string message)
        {
            string result =
                $"{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{AssemblyName}||{nameof(Info)}|| {message}";
            LogSum.Add(result);
            return result;
        }

        public static string Warning(string message)
        {
            string result =
                $"{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{AssemblyName}||{nameof(Warning)}|| {message}";
            LogSum.Add(result);
            return result;
        }

        public static string Error(string message)
        {
            string result =
                $"{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{AssemblyName}||{nameof(Error)}|| {message}";
            LogSum.Add(result);
            return result;
        }
    }
}
