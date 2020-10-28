using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_MERSHATI_Laith_Problème
{
    class Program
    {
        /// <summary>
        /// La partie d'une minute est lancé dans laquelle le joueur devra entrer un mot présent dans le plateau qui lui sera affiché.
        /// Le décompte des points se fera en fonction de différends paramètres, à savoir si le mot possède au minimum trois lettres, existe dans le dictionnaire et sur le plateau.
        /// </summary>
        /// <param name="Joueur"> Joueur qui joue cette manche </param>
        /// <param name="plateauJeu"> Plateau de jeu dans lequelle le joueur devra rechercher des mots </param>
        /// <param name="dico"> Un dictionnaire afin de vérifier si le mot entré existe </param>
        static void Jouer(Joueur Joueur, int joueur, Plateau plateauJeu, Dictionnaire dico)
        {
            if (joueur == 1)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (joueur == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine("- C’est au tour de " + Joueur.Nom + "\n");

            Console.ForegroundColor = ConsoleColor.Black;

            if (joueur == 1)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
            }
            else if (joueur == 2)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }

            Console.WriteLine(plateauJeu.toString() + "\n");
            Console.ResetColor();

            DateTime debut = DateTime.Now; // Début du temps de jeu pour ce joueur
            TimeSpan diffTemps;
            double temps = 0;

            while (temps < 1) // tant que l'intervalle de temps d'une minutes n'est pas atteint, le joueur continue du jouer
            {
                string mot = null;
                do
                {
                    Console.Write("Saisissez un mot : ");
                    mot = Convert.ToString(Console.ReadLine());
                    diffTemps = DateTime.Now - debut;
                    temps = diffTemps.TotalMinutes;
                }
                while ((mot.Length < 3 || dico.RechDichoRecursif(mot) == false || Joueur.Contain(mot) == true || plateauJeu.Test_Plateau(mot) == false) && (temps < 1));   // AJOUTER TEST PLATEAU // Pour que le mot sit valide le joueur doit respecter la contrainte de taille, d'existance du mot et ne l'entrer qu'une seul fois
                                                                                                                                                                           // Si on atteint la limite de temps, on sort de la boucle peut importe ce que le joueur a entré
                if (temps < 1) // Permet d'éviter de prendre en compte le mot que le joueur à entré dans la boucle do/while s'il l'a entré après la limite de temps
                {
                    Joueur.Add_Mot(mot);
                    int nbLettre = mot.Length;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    switch (nbLettre)
                    {
                        case 3:
                            Joueur.Score += 2;
                            Console.WriteLine("Vous avez marqué " + 2 + " points");
                            break;

                        case 4:
                            Joueur.Score += 3;
                            Console.WriteLine("Vous avez marqué " + 3 + " points");
                            break;

                        case 5:
                            Joueur.Score += 4;
                            Console.WriteLine("Vous avez marqué " + 4 + " points");
                            break;

                        case 6:
                            Joueur.Score += 5;
                            Console.WriteLine("Vous avez marqué " + 5 + " points");
                            break;

                        default: // Le cas des mots de taille de 0 à 2 est traité dans la boucle do/while
                            Joueur.Score += 11;
                            Console.WriteLine("Vous avez marqué " + 11 + " points");
                            break;
                    }
                    Console.ResetColor();
                }
                diffTemps = DateTime.Now - debut;
                temps = diffTemps.TotalMinutes;
            }
            Console.WriteLine("\n" + Joueur.toString());
        }

        static void FinPartie(Joueur joueur1, Joueur joueur2)
        {
            Joueur joueurGagnant = joueur1;
            bool vainqueur = false;

            if (joueur1.Score > joueur2.Score)
            {
                vainqueur = true;
            }
            if (joueur1.Score < joueur2.Score)
            {
                joueurGagnant = joueur2;
                vainqueur = true;
            }

            if (vainqueur)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(joueurGagnant.Nom + " a remporté la partie avec un score de " + joueurGagnant.Score + " points");
            }
            else
            {
                Console.WriteLine("C'est un match nul !");
            }
        }

        static void Main(string[] args)
        {
            // Les classes étant inaccessible depuis le projet Test Unitaire, les différends test ont donc été réalisés ci-dessous en fin de programme.


            // Affichage nom du jeu

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            string jeu = "  ZigZag Mot  ";
            int nbEspaces = (Console.WindowWidth - jeu.Length) / 2;
            Console.SetCursorPosition(nbEspaces, Console.CursorTop);
            Console.WriteLine(jeu + "\n\n");
            Console.ResetColor();


            // Initialisation des joueurs

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Joueur 1,");
            Console.ResetColor();
            Console.Write(" inscrivez votre nom: ");
            Joueur joueur1 = new Joueur(Convert.ToString(Console.ReadLine()));

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Joueur 2,");
            Console.ResetColor();
            Console.Write(" inscrivez votre nom: ");
            Joueur joueur2 = new Joueur(Convert.ToString(Console.ReadLine()));
            Console.WriteLine("\n");


            // Initialisation du dictionnaire et des dés

            Dictionnaire dico = new Dictionnaire("MotsPossibles.txt");
            Plateau plateauJeu = new Plateau("Des.txt");


            // Début de jeu

            int joueur = 0; // numéro du joueur qui sera soit 1 soit 2 par une opération mathématique

            DateTime debut = DateTime.Now; // Initialiser le début du temps de jeu
            TimeSpan diffTemps;
            double temps = 0;
            while (temps < 6)
            {
                joueur = (joueur % 2) + 1; // A chaque tour de boucle "joueur" passera de 1 à 2 et inversement

                if (joueur == 1)
                {
                    Jouer(joueur1, 1, plateauJeu, dico);
                }

                else if (joueur == 2)   // Changement de joueur
                {
                    Jouer(joueur2, 2, plateauJeu, dico);
                }
                Console.WriteLine("\n\n\n");

                diffTemps = DateTime.Now - debut;
                temps = diffTemps.TotalMinutes;
            }

            Console.WriteLine("Fin du temps imparti.\n");
            FinPartie(joueur1, joueur2);



            // TEST

            /*
            Joueur joueur1 = new Joueur("Kakashi");
            joueur1.Add_Mot("test");
            joueur1.Add_Mot("reTest");
            Console.WriteLine(joueur1.Contain("retest"));
            Console.WriteLine(joueur1.toString());
            */

            /*
            Random r = new Random();
            string[] de = { "L", "Y", "R", "A", "I"};
            De de1 = new De(de);
            string[] de = { "L", "Y", "R", "A", "I", "S" };
            De de1 = new De(de);
            Console.WriteLine(de1.toString());
            de1.Lance(r);
            Console.WriteLine(de1.LettreTiree);
            */

            /*
            Dictionnaire dico = new Dictionnaire("MotsPossibles.txt");
            Console.WriteLine(dico.toString());
            Console.WriteLine(dico.RechDichoRecursif("NAN"));
            Console.WriteLine(dico.RechDichoRecursif("OUI"));
            */

            /*
            Plateau plateauJeu = new Plateau("Des.txt");
            Console.WriteLine(plateauJeu.toString());
            */

            /*
            DateTime debut = DateTime.Now;
            double temps;
            do
            {
                TimeSpan diffTemps = DateTime.Now - debut;
                temps = diffTemps.TotalMinutes;
                Console.WriteLine("Ecris qqc");
                string a = Console.ReadLine();
                Console.WriteLine(a);
            }
            while (temps < 1);
            */

            /*
            DateTime debut = DateTime.Now;
            double temps = 0;
            while(temps < 1)
            {
                TimeSpan diffTemps = DateTime.Now - debut;
                temps = diffTemps.TotalMinutes;
                Console.WriteLine("Ecris qqc");
                string a = Console.ReadLine();
                Console.WriteLine(a);
            }
            */

            Console.ReadKey();
        }
    }
}
