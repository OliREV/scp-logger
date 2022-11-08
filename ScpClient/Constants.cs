using System.Diagnostics.CodeAnalysis;

namespace ScpClient
{
    [ExcludeFromCodeCoverage]
    public class Constants
    {
        public const string SEND_LOG_COMMAND = "sendlog";
        public const string EXIT_COMMAND = "exit";
        public const string UNKNOWN_COMMAND_MESSAGE = "Unknown command. Available commands:" +
                                                       "\n'exit' | Exits the program without doing anything." +
                                                       "\n'sendlog' | Starts to upload the previously written logs.";
    }
}
