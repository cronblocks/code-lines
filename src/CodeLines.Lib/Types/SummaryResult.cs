namespace CodeLines.Lib.Types
{
    public class SummaryResult
    {
        public Language Language { get; }

        public ulong TotalLines { get; }
        public ulong CommentLines { get; }
        public ulong CodeLines { get; }
    }
}
