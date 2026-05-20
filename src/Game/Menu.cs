
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public class SimpleMenu
    {
        private string[] option;
        private int selected = 0;
        public string TestoInizio = "Ciao Giocatore1 Benvenuto in SpaceInvaders !!";
        private bool controllo = false;

        public SimpleMenu(string[] entries)
        {
            option = entries;
        }

        public void StringChar(string frase)
        {
            for (int i = 0; i < frase.Length; i++)
            {
                Console.Write(frase[i]);
                Thread.Sleep(40); 
            }
        }
        public int MostraMenu()
        {
            Console.CursorVisible = false;

            // posizioni fisse 
            int inizioX = (Console.WindowWidth - TestoInizio.Length) / 2;
            int inizioY = (Console.WindowHeight / 2) - (option.Length / 2) - 2;
            int startYOptions = inizioY + 3;

            while (true)
            {
                Console.Clear();

                //testo di benvenuto
                Console.SetCursorPosition(inizioX, inizioY);
                if (!controllo)
                {
                    StringChar(TestoInizio);
                    controllo = true;
                }
                else
                {
                    Console.Write(TestoInizio); 
                }

                // Mostra e centra le opzioni del menu
                for (int i = 0; i < option.Length; i++)
                {
                    string rigaOpzione;
                    if (i == selected)
                    {
                        rigaOpzione = "> " + option[i];
                    }
                    else
                    {
                        rigaOpzione = "  " + option[i];
                    }

                    // Ricalcola X per ogni opzione per centrarla 
                    int opzioneX = (Console.WindowWidth - rigaOpzione.Length) / 2;

                    // Incrementa la Y (+ i) per stampare una riga sotto l'altra
                    Console.SetCursorPosition(opzioneX, startYOptions + i);

                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(rigaOpzione);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(rigaOpzione);
                    }
                }
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.W)
                {
                    selected = (selected == 0) ? option.Length - 1 : selected - 1;
                }
                else if (key.Key == ConsoleKey.S)
                {
                    selected = (selected == option.Length - 1) ? 0 : selected + 1;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    return selected; // Ritorna l'indice dell'opzione scelta
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    return -1; // Ritorna -1 per uscire dal gioco
                }
            }
        }
    }
}
