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
        private ResultSet _resultSet;

        public ProcessPipeline(string dir_or_filename, Logger logger)
        {
            DirOrFilename = dir_or_filename ?? throw new ArgumentNullException(nameof(dir_or_filename));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _filesProvider = new FilesProvider(DirOrFilename);
            _resultSet = new ResultSet();
        }

        public string DirOrFilename { get; }

        internal ResultSet GetResult()
        {
            return _resultSet;
        }

        internal void Process()
        {
            foreach (string filename in _filesProvider.NextFilename())
            {
                _logger.Log($"Processing {filename}", LogLevel.Info);
            }
        }
    }
}
