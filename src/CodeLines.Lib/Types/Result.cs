using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLines.Lib.Types
{
    public class Result
    {
        public Language Language { get; }
        public ulong TotalLines { get; }
        public ulong CommentLines { get; }
        public ulong CodeLines { get; }
    }
}
