﻿using CodeLines.Lib.Types;
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
