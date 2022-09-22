using CodeLines.Lib;
using CodeLines.Lib.Exceptions;

Console.WriteLine("Code Lines!");

try
{
    LinesCounter counter = new LinesCounter(
        Path.Combine(
            Environment.CurrentDirectory,
            $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}" +
            $"..{Path.DirectorySeparatorChar}.."),
        Console.WriteLine,
        skippedDirOrFilenames: ".git,.svn,bin,obj");

    counter.Process();
    counter.PrintResult();
}
catch (NeitherFileNorDirectoryException ex)
{
    Console.WriteLine($"ERROR! Neither a file nor a directory: \"{ex.Name}\"");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR! \"{ex.Message}\"");
}

Console.WriteLine();
Console.WriteLine();
