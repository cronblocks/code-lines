using System;
using System.IO;

namespace CodeLines.Lib.Providers
{
    internal class FilesProvider
    {
        public FilesProvider(string dirname)
        {
            Name = dirname;

            IsDir = FilesProvider.IsDirectory(dirname);
        }

        public string Name { get; }

        public bool IsDir { get; }
        public bool IsFile { get; }

        public static bool IsDirectory(string dirname)
        {
            return Directory.Exists(dirname);
        }
    }
}
