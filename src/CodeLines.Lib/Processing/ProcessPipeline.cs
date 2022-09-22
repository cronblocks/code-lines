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

        public event Action ProcessingStarted;
        public event Action ProcessingFinished;
        public event Action<string> FileProcessingStarted;
        public event Action<string, FileResult> FileProcessingFinished;
        public event Action<string, string> FileProcessingError;
        public event Action<string> FileProcessingSkipped;

        public ProcessPipeline(string dirOrFilename, Logger logger, string skippedDirOrFilenames = "")
        {
            DirOrFilename = dirOrFilename ?? throw new ArgumentNullException(nameof(dirOrFilename));
            SkippedDirsOrFilenames = skippedDirOrFilenames;

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _filesProvider = new FilesProvider(DirOrFilename, SkippedDirsOrFilenames);
            _resultSet = new ResultSet();

            CreatePipeline();
        }

        public string DirOrFilename { get; }
        public string SkippedDirsOrFilenames { get; }

        internal ResultSet GetResult()
        {
            return _resultSet;
        }

        internal void Process()
        {
            ProcessingStarted?.Invoke();
            
            foreach (string filename in _filesProvider.NextFilename())
            {
                ProcessingNode node = GetProcessingNode(filename);

                if (node != null)
                {
                    _logger.Log($"Processing {filename}", LogLevel.Info);

                    FileProcessingStarted?.Invoke(filename);

                    TextLinesProvider textLinesProvider;

                    try
                    {
                        textLinesProvider = new TextLinesProvider(filename);
                    }
                    catch (Exception ex)
                    {
                        textLinesProvider = null;

                        _logger.Log($"ERROR! {ex.Message}", LogLevel.Error);

                        FileProcessingError?.Invoke(filename, ex.Message);
                    }

                    if (textLinesProvider != null)
                    {
                        FileResult fr = node.ProcessFile(filename, textLinesProvider);

                        UpdateResultSet(fr);

                        FileProcessingFinished?.Invoke(filename, fr);
                    }
                }
                else
                {
                    _logger.Log($"Skipping {filename}", LogLevel.Info);

                    FileProcessingSkipped?.Invoke(filename);
                }
            }

            ProcessingFinished?.Invoke();
        }

        private void UpdateResultSet(FileResult fileResult)
        {
            _resultSet.FileResults.Add(fileResult);

            bool srExists = false;

            foreach (SummaryResult sr in _resultSet.SummaryResults)
            {
                if (sr.Language == fileResult.Language)
                {
                    srExists = true;

                    sr.TotalLines += fileResult.TotalLines;
                    sr.BlankLines += fileResult.BlankLines;
                    sr.CommentLines += fileResult.CommentLines;
                    sr.CodeLines += fileResult.CodeLines;

                    break;
                }
            }

            if (!srExists)
            {
                _resultSet.SummaryResults.Add(
                    new SummaryResult()
                    {
                        Language = fileResult.Language,
                        TotalLines = fileResult.TotalLines,
                        BlankLines = fileResult.BlankLines,
                        CommentLines = fileResult.CommentLines,
                        CodeLines = fileResult.CodeLines
                    });
            }

            _resultSet.SummaryResults.Sort(
                (sr1, sr2) => {
                    if (sr1.Language < sr2.Language)
                        return -1;
                    else if (sr1.Language > sr2.Language)
                        return 1;
                    else
                        return 0;
                });
        }
    }
}
