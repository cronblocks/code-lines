using CodeLines.Lib.Types;
using System;

namespace CodeLines.Lib.Processing
{
    internal class Logger
    {
        public Logger(LogLevel logLevel, Action<string> messageLinePrintFunc)
        {
            LogLevel = logLevel;
            MessageLinePrintFunc = messageLinePrintFunc ?? throw new ArgumentNullException(nameof(messageLinePrintFunc));
        }

        public void Log(string message, LogLevel logLevel = LogLevel.Info)
        {
            if (logLevel >= LogLevel)
            {
                MessageLinePrintFunc(message);
            }
        }

        private LogLevel LogLevel { get; }
        private Action<string> MessageLinePrintFunc { get; }
    }
}
