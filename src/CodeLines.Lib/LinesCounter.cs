﻿using CodeLines.Lib.Helpers;
using CodeLines.Lib.Processing;
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

            MessageLinePrintFunc($"    "        +
                    $"{"Language",-20} : "      +
                    $"{"Total Lines",-16} : "   +
                    $"{"Blank Lines",-16} : "   +
                    $"{"Comment Lines",-16} : " +
                    $"{"Code Lines",-16} :");

            foreach (SummaryResult summaryResult in rs.SummaryResults)
            {
                MessageLinePrintFunc($"    "             +
                    $"{summaryResult.Language.StringName(),-20} : "   +
                    $"{summaryResult.TotalLines,-16} : "   +
                    $"{summaryResult.BlankLines,-16} : "   +
                    $"{summaryResult.CommentLines,-16} : " +
                    $"{summaryResult.CodeLines,-16} :");
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
