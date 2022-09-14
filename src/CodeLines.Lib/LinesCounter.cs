using CodeLines.Lib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLines.Lib
{
    public class LinesCounter
    {
        public LinesCounter(
            string dir_or_filename,
            Action<string> messagePrint,
            LogLevel logLevel = LogLevel.Info)
        {
            DirOrFilename = dir_or_filename;
            LogLevel = logLevel;
            MessagePrint = messagePrint;
        }

        public string DirOrFilename { get; }
        public Action<string> MessagePrint { get; }
        public LogLevel LogLevel { get; }
    }
}
