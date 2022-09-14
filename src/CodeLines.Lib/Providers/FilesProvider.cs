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

            if (IsNameFile)
            {
                _next = dirname;
            }
        }

        public string NextFilename()
        {
            if (IsNameFile)
            {
                string retval = _next;
                _next = null;
                return retval;
            }
            else if (IsNameDirectory)
            {

            }

            return null;
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
