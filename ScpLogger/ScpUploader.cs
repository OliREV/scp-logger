using System;
using WinSCP;

namespace ScpLogger
{
    internal class ScpUploader
    {
        private static string HostName { get; set; }
        private static string UserName { get; set; }
        private static string Password { get; set; }
        private static int PortNumber { get; set; }
        private static bool GiveUpSecurity { get; set; }

        public ScpUploader(string hostName,string userName,string password,int portNumber,bool giveUpSecurity)
        {
            HostName= hostName;
            UserName = userName;
            Password = password;
            PortNumber = portNumber;
            GiveUpSecurity = giveUpSecurity;
        }

        public void Upload(string localPath,string remotePath)
        {

            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Scp,
                HostName = HostName,
                UserName = UserName,
                Password = Password,
                PortNumber = PortNumber,
                GiveUpSecurityAndAcceptAnySshHostKey = GiveUpSecurity
            };

            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);

                // Upload files
                TransferOptions transferOptions = new TransferOptions();
                transferOptions.TransferMode = TransferMode.Binary;

                TransferOperationResult transferResult;
                transferResult =
                    session.PutFiles(localPath, remotePath, false, transferOptions);

                // Throw on any error
                transferResult.Check();

                // Print results
                foreach (TransferEventArgs transfer in transferResult.Transfers)
                {
                    Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                }
            }
        }
    }
}