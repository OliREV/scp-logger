using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ScpLogger
{
    public class Logger
    {
        public string FileName = $"Log{DateTime.UtcNow:yyyy-dd-M--HH-mm-ss}.txt";
        /// <summary>
        /// Format: 
        /// </summary>
        public string HostName { get; set; }
        public string LocalPath { get; set; }
        public string RemotePath { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PortNumber { get; set; }
        internal List<string> LogSum = new List<string>();

        /// <summary>
        /// Determines whether the file needs to be saved to a remote machine or only locally. True if only locally.
        /// </summary>
        public bool LogOnlyToLocalPath { get; set; }

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
        public Logger() { }

        private static string GetExternalIp()
        {

            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            var externalIp = IPAddress.Parse(externalIpString);

            return externalIp.ToString();
        }

        /// <summary>
        /// Uploads the existing logs to the location. If LogOnlyToLocalPath set to true then the program Debug folder will contain the log by default.
        /// You can change that by setting FileName variable to another path. Containing the filename too.
        /// </summary>
        public void UploadLog(Logger logger)
        {
            File.WriteAllLines(logger.FileName, logger.LogSum);
            if (logger.LogOnlyToLocalPath)
                File.WriteAllLines(logger.FileName, logger.LogSum);
            else
            {
                var uploader = new ScpUploader(logger.HostName, logger.UserName, logger.Password, logger.PortNumber, true);
                uploader.Upload(logger.LocalPath??FileName, logger.RemotePath);
            }
        }

        /// <summary>
        /// Log an Info level severity message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string Info(string message)
        {
            string result =
                $"{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{AssemblyName}||{nameof(Info)}|| {message}";
            LogSum.Add(result);
            return result;
        }
        /// <summary>
        /// Log an Warn level severity message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string Warning(string message)
        {
            string result =
                $"{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{AssemblyName}||{nameof(Warning)}|| {message}";
            LogSum.Add(result);
            return result;
        }
        /// <summary>
        /// Log an Error level severity message. Also throws an Error
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string Error(string message, Exception exception)
        {
            string result =
                $"{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{AssemblyName}||{nameof(Error)}|| {message}";
            LogSum.Add(result);

            throw exception;
        }
    }
}
