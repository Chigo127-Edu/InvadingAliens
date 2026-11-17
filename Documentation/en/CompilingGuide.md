# How to get the game - compiling from source

This guide is useful to get the executable for a non-tested platform (E.g. 32-bit systems).

1. Download **[.NET SDK 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)** from Microsoft's official website. .NET SDK is on the left.
   1. Choose your own platform and architecture, so install the desired package. It is possible to download standalone binaries when it is wanted to avoid installing new stuff.
      1. If an installer is needed, it is necessary to follow the official website's guide (E.g. "Package manager instructions").
      2. If a binary executable is needed, it is necessary to extract from the downloaded archive, and take note about the `dotnet` executable path.
         - For example: `C:\Users\Utente\Downloads\dotnet-sdk-10.0.101-win-x86\dotnet.exe` on Microsoft Windows.

2. Download the project's source code, using the green button `Code` at the top, then choosing "Download ZIP", otherwise from releases.
   1. Extract source code, and take note about the path.
      - For example: `C:\Users\Utente\Downloads\InvadingAliens-main` on Microsoft Windows.

3. Open the command prompt (or a text shell), and CD (change directory) to the project's folder.
   For example: `cd C:\Users\Utente\Downloads\InvadingAliens-main\` on Microsoft Windows.
   1. Use the following command: `dotnet publish`.
      - By typing `dotnet publish --help` it is possible to get information about compiling for other platforms, or with .NET 10 Runtime integrated, by adding `--sc`.
      - NOTICE: `dotnet` is the file `dotnet` or `dotnet.exe`. Its path is the one saved earlier (in your notes).
      For example, the entire command to be run is `C:\Users\Utente\Downloads\dotnet-sdk-10.0.101-win-x86\dotnet.exe publish` if the command intended to be runned is `dotnet publish`.

4. Once finished compiling, change directory to `./TicTacToe/bin/Release/net10.0/`.
   - In case the built binaries are self-contained (so with .NET runtime integrated), change directory to `./TicTacToe/bin/Release/net10.0/`, then change directory again to the new subdirectory.
     - For example, on 32-bit Windows it is `win-x86`, so the second command will be `cd ./win-x86/Publish`.
    
5. Type `InvadingAliens` (or `InvadingAliens` on Windows) to start playing.
