﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ScpLogger
{
    public class Logger
    {
        /// <summary>
        /// Format: "Log{DateTime.UtcNow:yyyy-dd-M--HH-mm-ss}.txt"
        /// </summary>
        public string FileName = $"Log{DateTime.UtcNow:yyyy-dd-M--HH-mm-ss}.txt";
        /// <summary>
        /// HostName format is something like: {user}@{serverAddress}, Port needs to be defined in the PortNumber property.
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// File which needs to be uploaded.
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// The path of the remote server wehre you can save the file. e.g. "/home/logs/"
        /// </summary>
        public string RemotePath { get; set; }
        /// <summary>
        /// Name of the user for the server
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Value of the password for the user
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Port number in integer form.
        /// </summary>
        public int PortNumber { get; set; }
        internal List<string> LogSum = new List<string>();

        /// <summary>
        /// Determines whether the file needs to be saved to a remote machine or only locally. True if only locally.
        /// </summary>
        public bool LogOnlyToLocalPath { get; set; }

        private static string _assemblyName;
        /// <summary>
        /// Assembly name to determine the logging environment "Unknown program" if not set.
        /// </summary>
        public string AssemblyName
        {
            get => _assemblyName;
            set => _assemblyName = string.IsNullOrEmpty(value) ? "Unknown program" : value;
        }

        public Logger()
        {
            AssemblyName = "";
        }

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
        public string Error(string message)
        {
            string result =
                $"{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{AssemblyName}||{nameof(Error)}|| {message}";
            LogSum.Add(result);

            return result;
        }
    }
}
