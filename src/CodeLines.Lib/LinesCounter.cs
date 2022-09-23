using CodeLines.Lib.Helpers;
using CodeLines.Lib.Processing;
using CodeLines.Lib.Types;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeLines.Lib
{
    public class LinesCounter
    {
        private ProcessPipeline _pipeline;
        private Logger _logger;

        public LinesCounter(
            string dirOrFilename,
            Action<string> messageLinePrintFunc,
            LogLevel logLevel = LogLevel.Info,
            string skippedDirOrFilenames = "")
        {
            DirOrFilename = CleanedPath(dirOrFilename) ?? throw new ArgumentNullException(nameof(dirOrFilename));
            MessageLinePrintFunc = messageLinePrintFunc ?? throw new ArgumentNullException(nameof(messageLinePrintFunc));
            LogLevel = logLevel;

            _logger = new Logger(logLevel, messageLinePrintFunc);

            _pipeline = new ProcessPipeline(dirOrFilename, _logger, skippedDirOrFilenames);
        }

        private string CleanedPath(string dirOrFilename)
        {
            if (!string.IsNullOrEmpty(dirOrFilename))
            {
                List<string> pathParts = new List<string>(
                    dirOrFilename.Split(new char[] { '/', '\\' },
                    StringSplitOptions.RemoveEmptyEntries));

                List<int> indicesToBeRemoved = new List<int>();
                int itbr = -1;
                int currIndex = 0;

                foreach (string pathPart in pathParts)
                {
                    if (pathPart != "..")
                    {
                        itbr = -1;
                    }
                    else
                    {
                        indicesToBeRemoved.Add(currIndex);

                        if (itbr == -1)
                        {
                            itbr = currIndex - 1;
                        }
                        else
                        {
                            itbr--;
                        }

                        if (itbr >= 0)
                        {
                            indicesToBeRemoved.Add(itbr);
                        }
                    }

                    currIndex++;
                }

                for (int i = 0; i < indicesToBeRemoved.Count; i++)
                {
                    int indexToBeRemoved = indicesToBeRemoved[i];

                    pathParts.RemoveAt(indexToBeRemoved);

                    for (int internalIndex = i + 1; internalIndex < indicesToBeRemoved.Count; internalIndex++)
                    {
                        if (indicesToBeRemoved[internalIndex] > indexToBeRemoved)
                        {
                            indicesToBeRemoved[internalIndex] = indicesToBeRemoved[internalIndex] - 1;
                        }
                    }
                }
            }

            return dirOrFilename;
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

            MessageLinePrintFunc("    ----------------------------------------------------------------------------------------------------");
            
            MessageLinePrintFunc($"    | "      +
                    $"{"Language",-20} | "      +
                    $"{"Total Lines",-16} | "   +
                    $"{"Blank Lines",-16} | "   +
                    $"{"Comment Lines",-16} | " +
                    $"{"Code Lines",-16} |");

            MessageLinePrintFunc("    |----------------------|------------------|------------------|------------------|------------------|");

            foreach (SummaryResult summaryResult in rs.SummaryResults)
            {
                MessageLinePrintFunc($"    | "               +
                    $"{summaryResult.Language.StringName(),-20} | "   +
                    $"{summaryResult.TotalLines,16:N0} | "   +
                    $"{summaryResult.BlankLines,16:N0} | "   +
                    $"{summaryResult.CommentLines,16:N0} | " +
                    $"{summaryResult.CodeLines,16:N0} |");
            }

            MessageLinePrintFunc("    ----------------------------------------------------------------------------------------------------");
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
