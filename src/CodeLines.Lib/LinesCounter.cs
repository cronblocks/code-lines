using CodeLines.Lib.Types;
using System;

namespace CodeLines.Lib
{
    public class LinesCounter
    {
        private ProcessPipeline _pipeline;
        private Logger _logger;

        public LinesCounter(
            string dir_or_filename,
            Action<string> messageLinePrintFunc,
            LogLevel logLevel = LogLevel.Info)
        {
            DirOrFilename = dir_or_filename ?? throw new ArgumentNullException(nameof(dir_or_filename));
            MessageLinePrintFunc = messageLinePrintFunc ?? throw new ArgumentNullException(nameof(messageLinePrintFunc));
            LogLevel = logLevel;

            _logger = new Logger(logLevel, messageLinePrintFunc);

            _pipeline = new ProcessPipeline(dir_or_filename, _logger);
        }

        public void Process()
        {
            _pipeline.Process();
        }

        public void PrintResult()
        {
            ResultSet rs = GetResult();

            MessageLinePrintFunc("Summary:");

            if (rs.SummaryResults.Count == 0)
            {
                MessageLinePrintFunc("    No code lines found");
                return;
            }

            MessageLinePrintFunc($"    "      +
                    $"{"Language":-10} : "    +
                    $"{"Total Lines":-14} "   +
                    $"{"Blank Lines":-14} "   +
                    $"{"Comment Lines":-14} " +
                    $"{"Code Lines":-14}");

            foreach (SummaryResult summaryResult in rs.SummaryResults)
            {
                MessageLinePrintFunc($"    "             +
                    $"{summaryResult.Language:-10} : "   +
                    $"{summaryResult.TotalLines:-14} "   +
                    $"{summaryResult.BlankLines:-14} "   +
                    $"{summaryResult.CommentLines:-14} " +
                    $"{summaryResult.CodeLines:-14}");
            }
        }

        public ResultSet GetResult()
        {
            return _pipeline.GetResult();
        }

        public string DirOrFilename { get; }
        public Action<string> MessageLinePrintFunc { get; }
        public LogLevel LogLevel { get; }
    }
}
