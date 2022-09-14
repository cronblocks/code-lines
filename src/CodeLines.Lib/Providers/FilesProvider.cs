using CodeLines.Lib.Exceptions;
using System.Collections.Generic;
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

            if (!IsNameDirectory && !IsNameFile)
            {
                throw new NeitherFileNorDirectoryException(dirname);
            }
        }

        public IEnumerable<string> NextFilename()
        {
            if (IsNameFile)
            {
                string retval = _next;
                _next = null;

                yield return retval;
            }
            else if (IsNameDirectory)
            {
                foreach (string filename in Directory.EnumerateFiles(Name, "*", SearchOption.AllDirectories))
                {
                    yield return filename;
                }
            }
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
