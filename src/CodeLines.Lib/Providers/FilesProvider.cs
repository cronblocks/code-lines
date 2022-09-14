using System.IO;

namespace CodeLines.Lib.Providers
{
    internal class FilesProvider
    {
        private string _next;

        public FilesProvider(string dirname)
        {
            Name = dirname;

            IsNameDirectory = IsDirectory(dirname);
            IsNameFile = IsFile(dirname);
        }

        public string Name { get; }

        public bool IsNameDirectory { get; }
        public bool IsNameFile { get; }

        private bool IsDirectory(string dirname)
        {
            return Directory.Exists(dirname);
        }

        private bool IsFile(string filename)
        {
            return File.Exists(filename);
        }
    }
}
