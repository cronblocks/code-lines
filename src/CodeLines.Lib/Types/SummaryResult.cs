namespace CodeLines.Lib.Types
{
    public class SummaryResult
    {
        public Language Language { get; set; }

        public ulong TotalLines { get; set; }
        public ulong CommentLines { get; set; }
        public ulong CodeLines { get; set; }
    }
}
