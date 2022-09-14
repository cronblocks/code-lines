using CodeLines.Lib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLines.Lib
{
    public class LinesCounter
    {
        public LinesCounter(string dir_or_filename, LogLevel logLevel)
        {
            DirOrFilename = dir_or_filename;
            LogLevel = logLevel;
        }

        public string DirOrFilename { get; }
        public LogLevel LogLevel { get; }
    }
}
