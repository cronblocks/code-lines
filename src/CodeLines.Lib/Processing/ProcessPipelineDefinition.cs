using CodeLines.Lib.Types;
using System.Collections.Generic;

namespace CodeLines.Lib.Processing
{
    internal partial class ProcessPipeline
    {
        private List<ProcessingNode> _processingNodes;

        private void CreatePipeline()
        {
            _processingNodes = new List<ProcessingNode>();

            _processingNodes.Add(new ProcessingNode(Language.C,         "c,h",       @"//", @"/*", @"*/", _logger));
            _processingNodes.Add(new ProcessingNode(Language.CPlusPlus, "cpp,h,hpp", @"//", @"/*", @"*/", _logger));
            _processingNodes.Add(new ProcessingNode(Language.CSharp,    "cs",        @"//", @"/*", @"*/", _logger));
        }

        private ProcessingNode GetProcessingNode(string filename)
        {
            foreach (ProcessingNode node in _processingNodes)
            {
                if (node.IsFileProcessable(filename))
                {
                    return node;
                }
            }

            return null;
        }
    }
}
