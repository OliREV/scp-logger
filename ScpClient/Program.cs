using System;

namespace ScpClient
{
    //A CLIENT WHICH ONLY CAN TEST SCPLOGGER BEHAVIOUR DON'T INCLUDE IN PRODUCTION CODE
    public class Program
    {
        static void Main(string[] args)
        {
            var readLine = Convert.ToString(Console.ReadKey());
            ScpLogger.ScpClient client = new ScpLogger.ScpClient();
            var _logger = client.SetupLogger();
            _logger.Info("Program started");
            _logger.Info("Client called SetupLogger function.");

            Console.WriteLine("Working on it..");

            _logger.Error("Failed to evaluate script");

            //Wait for command "sendlog" then upload logs
            if (readLine == Constants.EXIT_COMMAND)
            {
                Environment.Exit(0);
            }

            while (readLine != Constants.SEND_LOG_COMMAND)
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
