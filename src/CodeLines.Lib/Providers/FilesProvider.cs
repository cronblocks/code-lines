using System;
using System.IO;

namespace CodeLines.Lib.Providers
{
    internal class FilesProvider
    {
        public FilesProvider(string dirname)
        {
            DirName = dirname;
        }

        public string DirName { get; }

        public static bool IsDirectory(string dirname)
        {
            return Directory.Exists(dirname);
        }
    }
}
