using CodeLines.Lib.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeLines.Lib.Providers
{
    internal class FilesProvider
    {
        private string _next;

        public FilesProvider(string dirOrFilename, string skippedDirsOrFilenames = "")
        {
            Name = dirOrFilename;

            IsNameDirectory = IsDirectory(dirOrFilename);
            IsNameFile = IsFile(dirOrFilename);

            if (IsNameFile)
            {
                _next = dirOrFilename;
            }

            if (!IsNameDirectory && !IsNameFile)
            {
                throw new NeitherFileNorDirectoryException(dirOrFilename);
            }

            if (!string.IsNullOrEmpty(skippedDirsOrFilenames))
            {
                SkippedNames = new List<string>(
                    skippedDirsOrFilenames.Split(
                        new char[] { ',', ';', '|' }, System.StringSplitOptions.RemoveEmptyEntries));
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
                    if (!IsSkippable(filename))
                    {
                        yield return filename;
                    }
                }
            }
        }

        private bool IsSkippable(string filename)
        {
            if (!string.IsNullOrEmpty(filename) && SkippedNames != null)
            {
                foreach (string namePart in filename.Split(
                    new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries))
                {
                    foreach (string skippableName in SkippedNames)
                    {
                        if (namePart.Equals(skippableName, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public string Name { get; }
        public List<string> SkippedNames { get; }

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
