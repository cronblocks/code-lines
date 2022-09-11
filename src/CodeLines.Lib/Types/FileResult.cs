using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLines.Lib.Types
{
    public class FileResult
    {
        public string Filename { get; set; }
        
        public Language Language { get; set; }

        public ulong TotalLines { get; set; }
        public ulong BlankLines { get; set; }
        public ulong CommentLines { get; set; }
        public ulong CodeLines { get; set; }
    }
}
