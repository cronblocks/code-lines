# CodeLines - ***v1.3***

*CodeLines* - a utility application, counts the number of lines in a codebase containing code in different programming languages using a simple GUI or Console interface while scanning a given folder contents recursively.

<div align="center"><img src="./.bin/screenshot-2022-09-23 223938.png" width="100%" /></div>

Files and directories with the following names are ignored in the app:
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
