namespace CodeLines.Lib.Providers
{
    internal class FilesProvider
    {
        public FilesProvider(string dirname)
        {
            DirName = dirname;
        }

        public string DirName { get; }
    }
}
