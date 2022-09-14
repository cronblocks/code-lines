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
            Action<string> messageLinePrintFunc,
            LogLevel logLevel = LogLevel.Info)
        {
            DirOrFilename = dir_or_filename;
            LogLevel = logLevel;
            MessageLinePrintFunc = messageLinePrintFunc;
        }

        public void Process()
        {

        }

        public void PrintResult()
        {

        }

        public ResultSet GetResult()
        {
            return null;
        }

        public string DirOrFilename { get; }
        public Action<string> MessageLinePrintFunc { get; }
        public LogLevel LogLevel { get; }
    }
}
