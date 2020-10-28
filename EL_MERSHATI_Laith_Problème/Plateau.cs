using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EL_MERSHATI_Laith_Problème
{
    class Plateau
    {
        // Attributs

        De[] des = new De[16]; // On rassemble l'ensemble de nos 16 dés dans un tableau
        Random r = new Random();
        string[,] plateau = new string[4, 4];


        // Constructeur

        public Plateau(string FileName)
        {
            ReadFile(FileName);
        }

        /// <summary>
        /// Extraire d'un fichier texte les 16 dés que l'on insère dans un tableau
        /// </summary>
        /// <param name="fileName"> Nom du document dans lequel sont répertoriés les dés </param>
        void ReadFile(string fileName)
        {
            StreamReader sReader = null;
            try
            {
                sReader = new StreamReader(fileName);
                string line;
                int i = 0;
                while ((line = sReader.ReadLine()) != null)
                {
                    string[] s = line.Split(';');
                    des[i] = new De(s);
                    i++;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sReader != null) { sReader.Close(); }
            }
        }


        // Méthodes

        /// <summary>
        /// Méthode similaire à la création et affichage d'une matrice qui nous permet ici de réaliser les deux à la fois
        /// </summary>
        /// <returns> 
        /// Une chaines de caractère avec espaces et sauts de lignes au bon endroit pour afficher au joueur le plateau de jeu
        /// </returns>
        public string toString()
        {
            string plateauJeu = null;
            int de = 0; // Dé à l'index 0 du tableau regroupant tous les dés
            for (int ligne = 0; ligne <= plateau.GetLength(0) - 1; ligne++)
            {
                for (int colonne = 0; colonne <= plateau.GetLength(1) - 1; colonne++)
                {
                    des[de].Lance(r);
                    string lettre = des[de].LettreTiree;
                    plateau[ligne, colonne] = lettre;
                    plateauJeu += lettre + " ";
                    de++;
                }
                plateauJeu += "\n";
            }
            return plateauJeu + "\n";
        }


        /// <summary>
        /// On parcout notre plateau de jeu de la même manière qu'on parcourt une matrice.
        /// Dès que l'on rencontre la première lettre de notre mot on va lancer la recherche dichotomique pour voir si on peu le former à partir des lettres du plateau en respectant la contrainte d'adjacence.
        /// Si on ne peut pas, alors on continue de parcourir notre plateau pour éventuellement le former plus loin si possible.
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public bool Test_Plateau(string mot)
        {
            int[] tabSens = new int[mot.Length];

            mot = mot.ToUpper();
            string premiereLettre = Convert.ToString(mot[0]);

            for (int ligne = 0; ligne <= plateau.GetLength(0) - 1; ligne++)
            {
                for (int colonne = 0; colonne <= plateau.GetLength(1) - 1; colonne++)
                {
                    if (plateau[ligne, colonne] == premiereLettre)
                    {
                        if (Test_Plateau_Dicho(mot, ligne, colonne, tabSens) == true)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// En parrcourant  notre plateau on trouve notre point de départ à une ligne et colonne précise. Il nous faut rechercher si la deuxieme lettre existe et se trouve autour de la première (pour la troisième elle devra être autour de la seconde etc.
        /// Ce qui fait 8 chemins possibles (en haut, en haut à droite, à droite...)
        /// Si jamais on se retrouve sur une lettre et celle qui suit n'existe pas, à proximité, alors il faut revenir en arrière d'une seule lettre et pour celà il faut faire le chemin inverse : si on a pris le trajet vers la droite (ligne = ligne et colonne = colonne + 1) alors il faut revenir par la gauche (colonne - 1 à partir de la nouvelle colonne).
        /// Au moment où l'on recule d'une lettre, il faut toujours explorer les autres chemins, i.e si on revient du chemin de droite il y a toujours le chemins du bas à explorer et autres
        /// </summary>
        /// <param name="mot"> mot que l'on recherche </param>
        /// <param name="ligne"> ligne sur laquelle se situe la dernière que l'on a trouvé (pas la dernière lettre du mot) </param>
        /// <param name="colonne"> colonne sur laquelle se situe la dernière que l'on a trouvé (pas la dernière lettre du mot) </param>
        /// <param name="tabSens"> tableau prenant des entier entre 1 et 8 afin de retenir le sens parcouru afin de pouvoir revenir en arrière correctement </param>
        /// <param name="i"> Nous permet d'obtenir la condition de fin quand le nombre de lettre trouvé équivaut au nombre de lettre de notre mot, ou bien quand celui-ci revient à 0 après tester tous les chemins possibles
        ///                  Il sert aussi en tant qu'index pour une lettre.
        /// </param>
        /// <param name="sens"> L est une lettre, et les chiffres représentent tous les sens de parcours possibles
        ///         8  1  2
        ///          \ | /
        ///        7 - L - 3
        ///          / | \
        ///         6  5  4
        /// </param>
        /// <param name="cheminInverse"> Renvoie un bool qui nous informe si le mouvement réalisé est un mouvement de retour en arrière. Ceci nous permettra de continuer la recherche sans repasser par le chemin qui n'aboutit à rien  </param>
        /// <returns></returns>
        private bool Test_Plateau_Dicho(string mot, int ligne, int colonne, int[] tabSens, int i = 0, int sens = 0, bool cheminInverse = false)
        {
            if (i == mot.Length - 1)
            {
                return true;
            }

            if (cheminInverse == true) // Un certain chemin/sens n'a pas aboutti, on revient en arrière afin d'explorer les autres
            {
                // Le cas du chemin 1 est le premier à se faire, on a pas à le remettre 

                if ((sens + 1) == 2 && ligne != 0 && colonne != 3 && plateau[ligne - 1, colonne + 1] == Convert.ToString(mot[i + 1]))
                {

                    tabSens[i] = 2;
                    return Test_Plateau_Dicho(mot, ligne - 1, colonne + 1, tabSens, i + 1, 2, false);
                }
                if ((sens + 1) == 3 && colonne != 3 && plateau[ligne, colonne + 1] == Convert.ToString(mot[i + 1]))
                {
                    tabSens[i] = 3;
                    return Test_Plateau_Dicho(mot, ligne, colonne + 1, tabSens, i + 1, 3, false);
                }
                if ((sens + 1) == 4 && ligne != 3 && colonne != 3 && plateau[ligne + 1, colonne + 1] == Convert.ToString(mot[i + 1]))
                {
                    tabSens[i] = 4;
                    return Test_Plateau_Dicho(mot, ligne + 1, colonne + 1, tabSens, i + 1, 4, false);
                }
                if ((sens + 1) == 5 && ligne != 3 && plateau[ligne + 1, colonne] == Convert.ToString(mot[i + 1]))
                {
                    tabSens[i] = 5;
                    return Test_Plateau_Dicho(mot, ligne + 1, colonne, tabSens, i + 1, 5, false);
                }
                if ((sens + 1) == 6 && ligne != 3 && colonne != 0 && plateau[ligne + 1, colonne - 1] == Convert.ToString(mot[i + 1]))
                {
                    tabSens[i] = 6;
                    return Test_Plateau_Dicho(mot, ligne + 1, colonne - 1, tabSens, i + 1, 6, false);
                }
                if ((sens + 1) == 7 && colonne != 0 && plateau[ligne, colonne - 1] == Convert.ToString(mot[i + 1]))
                {
                    tabSens[i] = 7;
                    return Test_Plateau_Dicho(mot, ligne, colonne - 1, tabSens, i + 1, 7, false);
                }
                if ((sens + 1) == 8 && ligne != 0 && colonne != 0 && plateau[ligne - 1, colonne - 1] == Convert.ToString(mot[i + 1]))
                {
                    tabSens[i] = 8;
                    return Test_Plateau_Dicho(mot, ligne - 1, colonne - 1, tabSens, i + 1, 8, false);
                }

                if (i == 0) // Si après avoir tester les 8 chemins possibles alors que l'on en est à la première lettre de notre mot et que l'on ne trouve pas de voies, alors on ne peut pas former le mot à partir de ce point de départ de notre plateau, on en ressort donc
                {
                    return false;
                }
                if (sens == 8) // Si on en est pas à la première lettre et que le chemin 8 n'aboutit pas alors on devra réaliser un double retour en arrière
                {
                    CheminInverse(mot, ligne, colonne, tabSens, i, sens);
                }
            }

            if (ligne != 0 && plateau[ligne - 1, colonne] == Convert.ToString(mot[i + 1]))
            {

                tabSens[i] = 1;
                return Test_Plateau_Dicho(mot, ligne - 1, colonne, tabSens, i + 1, 1, false);
            }
            if (ligne != 0 && colonne != 3 && plateau[ligne - 1, colonne + 1] == Convert.ToString(mot[i + 1]))
            {

                tabSens[i] = 2;
                return Test_Plateau_Dicho(mot, ligne - 1, colonne + 1, tabSens, i + 1, 2, false);
            }
            if (colonne != 3 && plateau[ligne, colonne + 1] == Convert.ToString(mot[i + 1]))
            {
                tabSens[i] = 3;
                return Test_Plateau_Dicho(mot, ligne, colonne + 1, tabSens, i + 1, 3, false);
            }
            if (ligne != 3 && colonne != 3 && plateau[ligne + 1, colonne + 1] == Convert.ToString(mot[i + 1]))
            {
                tabSens[i] = 4;
                return Test_Plateau_Dicho(mot, ligne + 1, colonne + 1, tabSens, i + 1, 4, false);
            }
            if (ligne != 3 && plateau[ligne + 1, colonne] == Convert.ToString(mot[i + 1]))
            {
                tabSens[i] = 5;
                return Test_Plateau_Dicho(mot, ligne + 1, colonne, tabSens, i + 1, 5, false);
            }
            if (ligne != 3 && colonne != 0 && plateau[ligne + 1, colonne - 1] == Convert.ToString(mot[i + 1]))
            {
                tabSens[i] = 6;
                return Test_Plateau_Dicho(mot, ligne + 1, colonne - 1, tabSens, i + 1, 6, false);
            }
            if (colonne != 0 && plateau[ligne, colonne - 1] == Convert.ToString(mot[i + 1]))
            {
                tabSens[i] = 7;
                return Test_Plateau_Dicho(mot, ligne, colonne - 1, tabSens, i + 1, 7, false);
            }
            if (ligne != 0 && colonne != 0 && plateau[ligne - 1, colonne - 1] == Convert.ToString(mot[i + 1]))
            {
                tabSens[i] = 8;
                return Test_Plateau_Dicho(mot, ligne - 1, colonne - 1, tabSens, i + 1, 8, false);
            }

            // Si on arrive à ce stade c'est que aucun des 8 chemins n'est possible

            if (i == 0)
            {
                return false;
            }

            // Chemin inverse si on ne se trouve pas sur la première lettre

            CheminInverse(mot, ligne, colonne, tabSens, i, sens);

            // Ecriture nécessaire pour lever le soucis suivant : "les chemins du code ne retournent pas tous une valeur"
            return false;
        }


        private bool CheminInverse(string mot, int ligne, int colonne, int[] tabSens, int i, int sens)
        {
            int chiffreSens = tabSens[i - 1];

            if (chiffreSens == 1)
            {
                return Test_Plateau_Dicho(mot, ligne + 1, colonne, tabSens, i - 1, chiffreSens, true);
            }
            if (chiffreSens == 2)
            {
                return Test_Plateau_Dicho(mot, ligne + 1, colonne - 1, tabSens, i - 1, chiffreSens, true);
            }
            if (chiffreSens == 3)
            {
                return Test_Plateau_Dicho(mot, ligne, colonne - 1, tabSens, i - 1, chiffreSens, true);
            }
            if (chiffreSens == 4)
            {
                return Test_Plateau_Dicho(mot, ligne - 1, colonne - 1, tabSens, i - 1, chiffreSens, true);
            }
            if (chiffreSens == 5)
            {
                return Test_Plateau_Dicho(mot, ligne - 1, colonne, tabSens, i - 1, chiffreSens, true);
            }
            if (chiffreSens == 6)
            {
                return Test_Plateau_Dicho(mot, ligne - 1, colonne + 1, tabSens, i - 1, chiffreSens, true);
            }
            if (chiffreSens == 7)
            {
                return Test_Plateau_Dicho(mot, ligne, colonne + 1, tabSens, i - 1, chiffreSens, true);
            }
            if (chiffreSens == 8)
            {
                return Test_Plateau_Dicho(mot, ligne + 1, colonne + 1, tabSens, i - 1, chiffreSens, true);
            }
            return false;
        }
    }
}
