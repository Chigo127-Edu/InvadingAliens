# InvadingAliens
######
## 0. Indice
1. Cos è InvadingAliens
2. Come si scarica InvadingAliens
3. Come si gioca ad InvadingAliens
4. Come funziona InvadingAliens
5. Conclusione
######

## 1. Cos è InvadingAliens
InvadingAliens è un gioco Open-Source ispirato al noto SpaceInvaders. La nostra versione fa utilizzo di [RetroGameFramework](https://github.com/profGiovanniVolpintesta/RetroGameFramework), anch'esso Open-Source. Pertanto, il nostro gioco si appoggia sull'API di Winndows Forms, ed è necessario il .NET Framework v4.7.2 al fine di poter giocare.
######

## 2. Come si scarica InvadingAliens
Per scaricare il gioco, fare riferimento alla sezione [Releases](https://github.com/Chigo127-Edu/InvadingAliens/releases).
Sia per ambienti Windows che per ambienti Wine, occorre avere installato il .NET Framework v4.7.2.
######

## 3. Come si gioca ad InvadingAliens
Al lancio del gioco, il giocatore viene accolto da un menù introduttivo su console.
Per scegliere l'opzione desiderata, si utilizzano i tasti W, A, S e D per muoversi ed INVIO per scegliere.
######
Una volta fatto partire un nuovo gioco, la navicella può essere spostata con W, A, S e D. è possibile parare colpi con SPAZIO, ma non è possibile muoversi e sparare contemporaneamente.
Il compito del giocatore è quello di uccidere i nemici e i boss, al fine di perdere meno vite possibile. Difatti, se un nemico/boss collide con la navicella, o esce dal bordo inferiore dello schermo, il giocatore perterà tante vite quante erano quelle rimanenti del nemico/boss. Ad esempio, se un boss ha 3 vite rimanenti, il giocatore perderà 3 vite.
######
Per poter riguadagnare le vite perse, è necessario uccidere i boss. Una volta uccisi, i boss danno un numero di vite pari al livello successivo di quello in corso. Ad esempio, una volta ucciso il boss a fine livello 3, il giocatore riceverà 4 vite e salirà di livello.
######
Per poter mettere in pausa il gioco, si prema P
Per richiamare un Game Over, si prema ESC. Lo si prema un'altra volta per uscire dal gioco.
######

## 4. Come funziona InvadingAliens
Il gioco opera su una matrice di pixel, in cui interagiscono i vari elementi.
I tipi di elementi sono principalmente tre, ossia quelli singoli, quelli multipli, ed il testo.
######
La navicella è un elemento singolo, ed ha le sue proprietà e i suoi stili statici.
Gli elementi dinamici hanno tutti delle proprietà statiche specifiche per il tipo (come lo stile), e delle proprietà dinamiche (Come posizione e velocità).
######
I nemici sono degli elementi dinamici, generati con una lista di oggetti di elementi. Hanno uno stile comune per tutti i nemici. 
Se i nemici sono colpiti dai proiettili, perdono una vita. Essi scompaiono quando le loro vite si azzerano.
Se invece colpiscono il giocatore, a quest'ultimo vengono sottratte le vite del nemico con cui si è scontrato.
All'aumentare del livello, aumenta il numero di mostri generati.
######
I proiettili sono elementi con proprietà pressoché analoghe ai nemici, con uno stile diverso.
I proiettili non fanno male al giocatore che li ha sparati, ma fanno male ai nemici.
La velocità aumenta leggermente dopo aver completato più livelli.
######
I boss sono dei nemici speciali che vengono generati ogni 30 secondi, uno alla volta. La loro velocità aumenta all'aumentare del livello.
######

## 5. Conclusione
Questo progetto Open-Source è stato realizzato da Chigo127-Edu e da Ale-Cioffo, sotto la GNU General Public License, versione 3.
