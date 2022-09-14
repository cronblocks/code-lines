using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLines.Lib
{
    public class LinesCounter
    {
        public LinesCounter(string dir_or_filename)
        {
            DirOrFilename = dir_or_filename;
        }

        public string DirOrFilename { get; }
    }
}
