using CodeLines.Lib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLines.Lib.Processing
{
    internal class ProcessingNode
    {
        public ProcessingNode(
            Language language,
            string fileExtensions,
            string singleLineCommentPattern, string multipleLineCommentPattern)
        {
            Language = language;
            FileExtensions = fileExtensions ?? throw new ArgumentNullException(nameof(fileExtensions));
            SingleLineCommentPattern = singleLineCommentPattern ?? throw new ArgumentNullException(nameof(singleLineCommentPattern));
            MultipleLineCommentPattern = multipleLineCommentPattern ?? throw new ArgumentNullException(nameof(multipleLineCommentPattern));
        }

        public Language Language { get; }
        public List<string> FileExtensions { get; }
        public string SingleLineCommentPattern { get; }
        public string MultipleLineCommentPattern { get; }

        public bool IsFileProcessable(string filename)
        {
            throw new NotImplementedException();
        }

        public FileResult ProcessFile()
        {
            throw new NotImplementedException();
        }
    }
}
