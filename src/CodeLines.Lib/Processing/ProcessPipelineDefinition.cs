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

            _processingNodes.Add(new ProcessingNode(Language.ArduinoSketch,   "ino",           @"//",    @"/*",    @"*/",    _logger));
            _processingNodes.Add(new ProcessingNode(Language.C,               "c,h",           @"//",    @"/*",    @"*/",    _logger));
            _processingNodes.Add(new ProcessingNode(Language.CPlusPlus,       "cxx,cpp,h,hpp", @"//",    @"/*",    @"*/",    _logger));
            _processingNodes.Add(new ProcessingNode(Language.CSharp,          "cs",            @"//",    @"/*",    @"*/",    _logger));
            _processingNodes.Add(new ProcessingNode(Language.GitIgnore,       "gitignore",     @"#",     @"",      @"",      _logger));
            _processingNodes.Add(new ProcessingNode(Language.Java,            "java",          @"//",    @"/*",    @"*/",    _logger));
            _processingNodes.Add(new ProcessingNode(Language.Markdown,        "md",            @"",      @"<!--",  @"-->",   _logger));
            _processingNodes.Add(new ProcessingNode(Language.Python,          "py",            @"#",     @"'''",   @"'''",   _logger));
            _processingNodes.Add(new ProcessingNode(Language.PlainText,       "txt",           @"",      @"",      @"",      _logger));
            _processingNodes.Add(new ProcessingNode(Language.XAML,            "xaml",          @"",      @"<!--",  @"-->",   _logger));
            _processingNodes.Add(new ProcessingNode(Language.XML,             "xml",           @"",      @"<!--",  @"-->",   _logger));
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
