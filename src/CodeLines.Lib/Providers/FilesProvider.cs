using System;
using System.IO;

namespace CodeLines.Lib.Providers
{
    internal class FilesProvider
    {
        public FilesProvider(string dirname)
        {
            Name = dirname;

            IsNameDir = FilesProvider.IsDirectory(dirname);
        }

        public string Name { get; }

        public bool IsNameDir { get; }
        public bool IsNameFile { get; }

        public static bool IsDirectory(string dirname)
        {
            return Directory.Exists(dirname);
        }

        public static bool IsFile(string filename)
        {
            return File.Exists(filename);
        }
    }
}
