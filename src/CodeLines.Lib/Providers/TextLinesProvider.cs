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

            TotalLines = GetTotalLines();
        }

        public string Filename { get; }
        public ulong TotalLines { get; set; }

        private ulong GetTotalLines()
        {
            ulong total = 0;

            foreach (string line in File.ReadLines(Filename))
            {
                total++;
            }

            return total;
        }
    }
}
