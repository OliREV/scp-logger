using System;
using ScpLogger;

namespace ScpClient
{
    //A CLIENT WHICH ONLY CAN TEST SCPLOGGER BEHAVIOUR DON'T INCLUDE IN PRODUCTION CODE
    internal class Program
    {
        static void Main(string[] args)
        {
            ScpLogger.ScpClient client = new ScpLogger.ScpClient();
            var _logger = client.SetupLogger();
            _logger.Info("Program started");

            _logger.Info("Client called SetupLogger function.");

            Console.WriteLine("Working on it..");

            _logger.Error("Failed to evaulate script");

            _logger.UploadLog(_logger);

            Console.ReadLine();
        }
    }
}
