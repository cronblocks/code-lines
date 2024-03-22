# CodeLines - ***v1.3***

*CodeLines* is a utility application that counts total number of lines in a given codebase that contains source code written in different programming languages. User can use a simple GUI or a Console interface for scanning a given folder's contents recursively.

<div align="center"><img src="./.bin/screenshot-2022-09-23 223938.png" width="100%" /></div>

Files and directories that are ignored in recursive scan:
  - *.git*
  - *.svn*
  - *.vs*
  - *bin*
  - *obj*

For ignoring these items, following lines are reproduced from *MainWindow.xaml.cs*:
```csharp
LinesCounter counter =
        new LinesCounter(targetPath, PrintOutputLine,
                skippedDirOrFilenames: ".git,.svn,.vs,bin,obj");
```
