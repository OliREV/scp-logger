
using System.Diagnostics;
using System;
using System.Text;

namespace ScpLogger
{
    public class ScpClient
    {
        public Logger SetupLogger()
        {
            Logger logger=new Logger
            {
                HostName = "adminpatent@78.92.254.124",
                UserName = "adminpatent",
                Password = "admin",
                RemotePath = "/home/adminpatent/passwordmanagerlogs/",
                PortNumber = 2222,
                AssemblyName = $"{nameof(ScpClient)}",
                SaveLogFile = false
            };
            return logger;
        }

        public static string GetRunningPrograms()
        {
            var sb = new StringBuilder();
            Process[] processes = Process.GetProcesses();
            sb.Append("Currently running programs:\n");
            foreach (Process p in processes)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                {
                    sb.Append($"{p.MainWindowTitle}\n");
                }
            }
            return sb.ToString();
        }
    }
}
