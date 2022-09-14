using System;
using System.IO;

namespace CodeLines.Lib.Providers
{
    internal class TextLinesProvider
    {
        public TextLinesProvider(string filename)
        {
            Filename = filename;

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }
        }

        public string Filename { get; }
        public ulong TotalLines { get; set; }
    }
}
