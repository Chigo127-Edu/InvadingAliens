# Come ottenere il gioco - Compilazione da sorgente

Questa guida serve per ottenere l'eseguibile per una piattaforma al di fuori di quelle testate (Es. sistemi a 32 bit).

1. Scaricare il **[.NET SDK 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)** dal sito ufficiale Microsoft. Il .NET SDK si trova sulla parte sinistra.
   1. Scegliere la propria piattaforma e architettura, e installare il pacchetto desiderato. È possibile installare i binaries qualora si volesse evitare di voler installare ulteriori applicazioni.
      1. Se si desidera ottenere un installer, è necessario seguire la guida sul sito ufficiale (Esempio: "Package manager instructions").
      2. Se si desidera scaricare i binari, è necessario estrarli dall'archivio scaricato, e annotarsi il percorso dell'eseguibile `dotnet`.
         - Ad esempio: `C:\Users\Utente\Downloads\dotnet-sdk-10.0.101-win-x86\dotnet.exe` su Microsoft Windows.

2. Scaricare il source code del progetto, utilizzando l'apposito tasto verde `Code` in cima alla pagina, poi scegliendo `Download ZIP`; oppure dalle releases.
   1. Estrarre il codice sorgente, e annotarsi il percorso.
      - Ad esempio: `C:\Users\Utente\Downloads\InvadingAliens-main` su Microsoft Windows.

3. Aprire il prompt dei comandi (o una shell testuale), ed effettuare un CD (Change Directory) alla cartella del progetto.  
   Ad esempio: `cd C:\Users\Utente\Downloads\InvadingAliens-main\` su Microsoft Windows.
   1. Utilizzare il comando `dotnet publish`. 
      - Digitando `dotnet publish --help` è possibile informarsi per compilare binari per altre piattaforme, o con il .NET 10 Runtime integrato, aggiungendo `--sc`.
      - NOTA: per `dotnet` si intende il file `dotnet` o `dotnet.exe`, il cui percorso è stato annotato precedentemente.  
      Ad esempio, il comando intero da eseguire è `C:\Users\Utente\Downloads\dotnet-sdk-10.0.101-win-x86\dotnet.exe publish` se si intende di usare `dotnet publish`.

4. Una volta terminata la compilazione, effettuare il comando CD in `./TicTacToe/bin/Release/net10.0/`.
   - Nel caso in cui si è deciso di costruire un binario self-contained, ossia con il .NET Runtime integrato, effettuare il comando CD in `./TicTacToe/bin/Release/net10.0/`, e poi effettuare nuovamente CD, nella sottocartella creata.  
     - Ad esempio, su Windows a 32 bit è `win-x86`, pertanto il secondo comando sarà `cd ./win-x86/Publish`.

5. Digitare `InvadingAliens` (o `InvadingAliens.exe` su Windows) per iniziare a giocare.
