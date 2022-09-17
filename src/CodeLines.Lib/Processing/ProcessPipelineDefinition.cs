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
                    //-------------------- A --------------------//
                    new ProcessingNode(Language.Ada,             "ada,adb,ads",   @"--",    @"",      @"",      _logger),
                    new ProcessingNode(Language.ArduinoSketch,   "ino",           @"//",    @"/*",    @"*/",    _logger),

                    //-------------------- B --------------------//

                    //-------------------- C --------------------//
                    new ProcessingNode(Language.C,               "c,h",           @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.CPlusPlus,       "cxx,cpp,h,hpp", @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.CSharp,          "cs",            @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.CSS,             "css",           @"",      @"/*",    @"*/",    _logger),

                    //-------------------- D --------------------//

                    //-------------------- E --------------------//

                    //-------------------- F --------------------//

                    //-------------------- G --------------------//
                    new ProcessingNode(Language.GitIgnore,       "gitignore",     @"#",     @"",      @"",      _logger),
                    new ProcessingNode(Language.Go,              "go",            @"//",    @"/*",    @"*/",    _logger),

                    //-------------------- H --------------------//
                    new ProcessingNode(Language.HTML,            "htm,html",      @"",      @"<!--",  @"-->",   _logger),

                    //-------------------- I --------------------//

                    //-------------------- J --------------------//
                    new ProcessingNode(Language.Java,            "java",          @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.JavaScript,      "js",            @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.JSON,            "json",          @"",      @"",      @"",      _logger),

                    //-------------------- K --------------------//

                    //-------------------- L --------------------//

                    //-------------------- M --------------------//
                    new ProcessingNode(Language.Markdown,        "md",            @"",      @"<!--",  @"-->",   _logger),

                    //-------------------- N --------------------//

                    //-------------------- O --------------------//
                    new ProcessingNode(Language.ObjectiveC,        "m,mm",        @"",      @"/*",    @"*/",    _logger),

                    //-------------------- P --------------------//
                    new ProcessingNode(Language.Pascal,          "pp,pas",        @"//",    @"{",     @"}",     _logger),
                    new ProcessingNode(Language.PHP,             "php",           @"//",    @"/*",    @"*/",    _logger),
                    new ProcessingNode(Language.Python,          "py",            @"#",     @"'''",   @"'''",   _logger),

                    //-------------------- Q --------------------//

                    //-------------------- R --------------------//

                    //-------------------- S --------------------//
                    new ProcessingNode(Language.ShellScript,     "sh,csh,bash",   @"#",     @"",      @"",      _logger),
                    new ProcessingNode(Language.Swift,           "swift",         @"//",    @"/*",    @"*/",    _logger),

                    //-------------------- T --------------------//
                    new ProcessingNode(Language.PlainText,       "txt",           @"",      @"",      @"",      _logger),

                    //-------------------- U --------------------//

                    //-------------------- V --------------------//

                    //-------------------- W --------------------//

                    //-------------------- X --------------------//
                    new ProcessingNode(Language.XAML,            "xaml",          @"",      @"<!--",  @"-->",   _logger),
                    new ProcessingNode(Language.XML,             "xml",           @"",      @"<!--",  @"-->",   _logger),

                    //-------------------- Y --------------------//

                    //-------------------- Z --------------------//

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
