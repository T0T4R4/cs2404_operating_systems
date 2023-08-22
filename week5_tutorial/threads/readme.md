These files illustrates the concepts of multi-threading in different programming languages


# How to run C# Examples

## Install .NET SDK:

Make sure you have the .NET SDK installed on your system. You can download and install it from the official .NET website.

## Compile and Run:

Open a command prompt or terminal, navigate to the directory where the multithread.cs file is located.

Ensure the compiler `csc.exe` is accessible. If it is not found, include its path to your current environment :

`SET PATH=%PATH%;C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\Roslyn`

or in a powershell window (sometimes VS Code starts those instead of a traditionnal Command Prompt)

`$env:PATH += ";C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\Roslyn"`

Then compile and run the code:

`csc yourfile.cs` 

which will generate an exe with the same name, which you can run 

`threasds.exe`
