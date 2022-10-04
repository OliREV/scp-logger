
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
            };
            return logger;
        }
    }
}
