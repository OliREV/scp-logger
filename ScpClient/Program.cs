using System;

namespace ScpClient
{
    //A CLIENT WHICH ONLY CAN TEST SCPLOGGER BEHAVIOUR DON'T INCLUDE IN PRODUCTION CODE
    public class Program : ScpLogger.ScpClient
    {
        static void Main(string[] args)
        {
            
            ScpLogger.ScpClient client = new ScpLogger.ScpClient();
            var _logger = client.SetupLogger();
            _logger.Info("Program started");
            _logger.Info("Client called SetupLogger function.");
            _logger.Info(GetRunningPrograms());

            Console.WriteLine("Working on it..");

            _logger.Error("Failed to evaluate script");

            if (Convert.ToString(Console.ReadLine()) == Constants.EXIT_COMMAND)
            {
                Environment.Exit(0);
            }

            while (Convert.ToString(Console.ReadLine()) != Constants.SEND_LOG_COMMAND)
            {
                Console.WriteLine(Constants.UNKNOWN_COMMAND_MESSAGE);
                Console.Write("scpclient@localhost:");
            }

            Console.WriteLine("Starting to send log...");
            _logger.UploadLog(_logger);
            Environment.Exit(0);
        }
    }
}
