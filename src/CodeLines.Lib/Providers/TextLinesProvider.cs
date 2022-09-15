using System.Collections.Generic;
using System.IO;

namespace CodeLines.Lib.Providers
{
    internal class TextLinesProvider
    {
        private ulong _totalLines;
        private bool _isTotalLinesCounted;

        public TextLinesProvider(string filename)
        {
            Filename = filename;

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            _totalLines = 0;
            _isTotalLinesCounted = false;
        }

        public IEnumerable<string> NextLine()
        {
            ulong total = 0;

            foreach (string line in File.ReadLines(Filename))
            {
                total++;
                yield return line;
            }

            _totalLines = total;
            _isTotalLinesCounted = true;
        }

        public string Filename { get; }
        public ulong TotalLines { get {
                if (_isTotalLinesCounted)
                    return _totalLines;

                _totalLines = GetTotalLines();
                _isTotalLinesCounted = true;

                return _totalLines;
            } }

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
