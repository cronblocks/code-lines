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
        private Logger _logger;

        public ProcessPipeline(string dir_or_filename, Logger logger)
        {
            DirOrFilename = dir_or_filename;

            _logger = logger;

            _filesProvider = new FilesProvider(DirOrFilename);
        }

        public string DirOrFilename { get; }

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
