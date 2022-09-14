﻿using CodeLines.Lib.Helpers;
using CodeLines.Lib.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeLines.Lib.Processing
{
    internal class ProcessingNode
    {
        public ProcessingNode(
            Language language,
            string fileExtensions,
            string singleLineCommentPattern, string multipleLineCommentPattern,
            Logger logger)
        {
            Language = language;
            FileExtensions = fileExtensions ?? throw new ArgumentNullException(nameof(fileExtensions));
            SingleLineCommentPattern = singleLineCommentPattern ?? throw new ArgumentNullException(nameof(singleLineCommentPattern));
            MultipleLineCommentPattern = multipleLineCommentPattern ?? throw new ArgumentNullException(nameof(multipleLineCommentPattern));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (string.IsNullOrEmpty(FileExtensions))
            {
                logger.Log($"Invalid file extension \"{fileExtensions}\"", LogLevel.Warn);
            }
            else
            {
                string[] extensions = fileExtensions.Split(new char[] { ';', '|', ',', '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                FileExtensionsList = new List<string>();
                foreach (string ext in extensions)
                {
                    if (ext.Trim().StartsWith("."))
                    {
                        FileExtensionsList.Add(ext.Trim());
                    }
                    else
                    {
                        FileExtensionsList.Add("." + ext.Trim());
                    }
                }
            }
        }

        public Language Language { get; }
        public string FileExtensions { get; }
        public List<string> FileExtensionsList { get; }
        public string SingleLineCommentPattern { get; }
        public string MultipleLineCommentPattern { get; }
        public Logger Logger { get; }

        public bool IsFileProcessable(string filename)
        {
            filename = filename.Trim();

            if (!File.Exists(filename))
            {
                return false;
            }

            foreach (string ext in FileExtensionsList)
            {
                if (filename.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public FileResult ProcessFile()
        {
            throw new NotImplementedException();
        }
    }
}
