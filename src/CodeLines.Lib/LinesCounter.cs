using CodeLines.Lib.Types;
using System;

namespace CodeLines.Lib
{
    public class LinesCounter
    {
        private ProcessPipeline _pipeline;

        public LinesCounter(
            string dir_or_filename,
            Action<string> messageLinePrintFunc,
            LogLevel logLevel = LogLevel.Info)
        {
            DirOrFilename = dir_or_filename;
            LogLevel = logLevel;
            MessageLinePrintFunc = messageLinePrintFunc ?? throw new ArgumentNullException(nameof(messageLinePrintFunc));

            _pipeline = new ProcessPipeline(dir_or_filename, messageLinePrintFunc, logLevel);
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
