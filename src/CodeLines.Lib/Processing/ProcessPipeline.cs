using CodeLines.Lib.Helpers;
using CodeLines.Lib.Providers;
using CodeLines.Lib.Types;
using System;

namespace CodeLines.Lib.Processing
{
    internal partial class ProcessPipeline
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

            CreatePipeline();
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
                ProcessingNode node = GetProcessingNode(filename);

                if (node != null)
                {
                    _logger.Log($"Processing {filename}", LogLevel.Info);

                    TextLinesProvider textLinesProvider;
                    try
                    {
                        textLinesProvider = new TextLinesProvider(filename);
                    }
                    catch (Exception ex)
                    {
                        textLinesProvider = null;
                        _logger.Log($"ERROR! {ex.Message}", LogLevel.Error);
                    }

                    if (textLinesProvider != null)
                    {
                        FileResult fileResult = node.ProcessFile(textLinesProvider);
                    }
                }
                else
                {
                    _logger.Log($"Skipping {filename}", LogLevel.Info);
                }
            }
        }
    }
}
