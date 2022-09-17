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

            _processingNodes.AddRange(
                new ProcessingNode[] {
                    new ProcessingNode(Language.ArduinoSketch,   "ino",           @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.C,               "c,h",           @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.CPlusPlus,       "cxx,cpp,h,hpp", @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.CSharp,          "cs",            @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.GitIgnore,       "gitignore",     @"#",     @"",      @"",      _logger),
                    new ProcessingNode(Language.HTML,            "htm,html",      @"",      @"<!--",  @"-->",   _logger),
                    new ProcessingNode(Language.Java,            "java",          @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.Markdown,        "md",            @"",      @"<!--",  @"-->",   _logger),
                    new ProcessingNode(Language.Python,          "py",            @"#",     @"'''",   @"'''",   _logger),
                    new ProcessingNode(Language.PlainText,       "txt",           @"",      @"",      @"",      _logger),
                    new ProcessingNode(Language.XAML,            "xaml",          @"",      @"<!--",  @"-->",   _logger),
                    new ProcessingNode(Language.XML,             "xml",           @"",      @"<!--",  @"-->",   _logger),
                    new ProcessingNode(Language.Ada,             "ada,adb,ads",   @"--",    @"",      @"",      _logger),
                });
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
