# Roadmap del progetto 
Questo progetto seguirà una roadmap, tramite cui verrà determinato il criterio di aggiunta delle funzionalità del nostro Retro-Game.
- La divisione dei ruoli può subire variazioni nel corso del tempo, a seconda delle esigenze.
######

## Indice
- [ ] Prima settimana: Creazione modelli, movimento
- [ ] Seconda settimana: Logica del gioco e gestione collisioni
- [ ] Terza settimana: Perfezionamento e rifinitura
######

## Prima settimana (da Lun 04/05 a Dom 10/05)
- [ ] Creazione della navicella + Pixel art (Una matrice di valori) [--> Ale-Cioffo]
- [ ] Creazione degli ostacoli + pixel art [--> Ale-Cioffo]
- [ ] Movimento della navicella [--> Chigo127-Edu]
    - [ ] Aggiornamento coordinate della matrice che viene ridisegnata [--> Chigo127-Edu]
    - [ ] Il Movimento è unidirezionale (Solo orizzontale oppure verticale) [--> Chigo127-Edu]
- [ ] Movimento ostacoli su console (no generazione, inizialmente ostacoli fissi) [--> Chigo127-Edu]
    - [ ] Aggiornamento coordinate della matrice che viene ridisegnata [--> Chigo127-Edu]
    - [ ] Il movimento è unidirezionale (inizialmente orientamento opposto alla navicella) [--> Chigo127-Edu]
- [ ] Movimento dei proiettili + pixel art [--> Ale-Cioffo]
    - [ ] Aggiornamento coordinate della matrice che viene ridisegnata [--> Ale-Cioffo]
    - [ ] Il movimento è unidirezionale, (inizialmente orientamento opposto alla ai nemici) [--> Ale-Cioffo]
######

## Seconda settimana (da Lun 11/05 a Dom 17/05)
- [ ] Gestione collisioni [--> Chigo127-Edu, Ale-Cioffo]
    - [ ] Proiettili che vanno a segno [--> Chigo127-Edu]
    - [ ] Ostacoli che finiscono sulla navicella [--> Ale-Cioffo]
    - [ ] Ostacoli tralasciati [--> Ale-Cioffo]
- [ ] Progresso [--> Chigo127-Edu, Ale-Cioffo]
    - [ ] Incremento spawn degli ostacoli [--> Chigo127-Edu]
    - [ ] Incremento velocità (opzionale) [--> Chigo127-Edu]
    - [ ] Spawn laterale di ostacoli (opzionale) [--> Ale-Cioffo]
- [ ] Vite e clausola per fine gioco [--> Chigo127-Edu, Ale-Cioffo]
    - [ ] Collegamento con le collisioni, determinazione vite [--> Chigo127-Edu]
    - [ ] Al termine delle vite il gioco finisce [--> Chigo127-Edu]
    - [ ] Il gioco è a permanenza (Come il chrome dino) e c’è un aumento graduale della difficoltà, ossia quanto descritto nel punto 7  (progresso) [--> Ale-Cioffo]
        - [ ] Il criterio di aumento della difficoltà è definito grossolanamente. [--> Ale-Cioffo]
######

## Terza settimana (da Lun 18/05 a Gio 21/05)
- [ ] Gestione punteggio (in caso di n collisioni game over) [--> Chigo127-Edu]
- [ ] Fix bug e abbellimento [--> Chigo127-Edu, Ale-Cioffo]
- [ ] Cura finale del repository (es. Perfezionare i rilasci) [--> Chigo127-Edu, Ale-Cioffo]
