using CodeLines.Lib.Providers;
using CodeLines.Lib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLines.Lib
{
    internal class ProcessPipeline
    {
        private FilesProvider _filesProvider;

        public ProcessPipeline(
            string dir_or_filename,
            Action<string> messageLinePrintFunc,
            LogLevel logLevel = LogLevel.Info)
        {
            DirOrFilename = dir_or_filename;
            LogLevel = logLevel;
            MessageLinePrintFunc = messageLinePrintFunc;

            _filesProvider = new FilesProvider(DirOrFilename);
        }

        public string DirOrFilename { get; }
        public Action<string> MessageLinePrintFunc { get; }
        public LogLevel LogLevel { get; }

        internal ResultSet GetResult()
        {
            throw new NotImplementedException();
        }

        internal void Process()
        {
            foreach (string filename in _filesProvider.NextFilename())
            {

            }
        }
    }
}
