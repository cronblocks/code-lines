using System;

namespace CodeLines.Lib.Exceptions
{
    public class NeitherFileNorDirectoryException : Exception
    {
        public NeitherFileNorDirectoryException(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
