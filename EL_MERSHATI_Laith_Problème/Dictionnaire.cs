using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EL_MERSHATI_Laith_Problème
{
    class Dictionnaire
    {
        // Attributs

        DictionnaireNbLettres[] dicoParNbLettres = new DictionnaireNbLettres[14]; // On rassemble l'ensemble de nos dictionnaires regroupés auparavant par nombres de lettres dans un même tableau
        // 14 car les mots varie entre 2 et 15 lettres


        // Constructeur

        public Dictionnaire(string FileName)
        {
            ReadFile(FileName);
        }

        /// <summary>
        /// Extraire d'un fichier texte les mots du dictionnaire que l'on insère dans un tableau qui les regroupe par nombres de lettres
        /// </summary>
        /// <param name="fileName"> Nom du document dans lequel sont répertoriés les mots </param>
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
                    string[] s = line.Split(' ');
                    dicoParNbLettres[i] = new DictionnaireNbLettres(s);
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

        public string toString()
        {
            int nbMots = 0; // Nombres de mots dans tous le dictionnaire
            int nbPlusPetitMot = dicoParNbLettres[0].NbLettres;
            int nbPlusGrandtMot = dicoParNbLettres[dicoParNbLettres.Length - 1].NbLettres;

            for (int i = 0; i <= dicoParNbLettres.Length - 1; i++)
            {
                nbMots += dicoParNbLettres[i].NbElements;
            }
            return "Le dictionnaire est composé de " + nbMots + " mots, allant de " + nbPlusPetitMot + " mots à " + nbPlusGrandtMot + " lettres";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mot"> Mot que l'on recherche </param>
        /// <param name="tab"> Tableau de dictionnaire regroupant les mots de même taille que celui recherché </param>
        /// <param name="debut"> Index 0 du tableau de dictionnaire </param>
        /// <param name="fin"> Dernier index du tableau de dictionnaire </param>
        /// <returns></returns>
        public bool RechDichoRecursif(string mot, string[] tab = null, int debut = 0, int fin = 0)
        {
            if (fin == 0) // Permet de réaliser cette condition qu'une seule fois afin d'initialiser le tableau de dictionnaire dans lequel nous allons faire notre recherche
            {
                mot = mot.ToUpper();
                int nbLettre = mot.Length;
                for (int i = 0; i <= dicoParNbLettres.Length - 1; i++)
                {
                    if (dicoParNbLettres[i].NbLettres == nbLettre)
                    {
                        tab = dicoParNbLettres[i].Dico;
                        fin = tab.Length - 1;
                    }
                }
                return RechDichoRecursif(mot, tab, debut, fin);
            }
            // A partir de ce stade on effectue une recherche dichotomique classique
            int c = (debut + fin) / 2;

            if (fin - debut == 1 && mot != tab[c])
            {
                return false;
            }

            if (tab[0].CompareTo(mot) <= 0 && tab[c].CompareTo(mot) > 0)
            {
                fin = c;
                return RechDichoRecursif(mot, tab, debut, fin);
            }
            if (tab[c].CompareTo(mot) < 0 && tab[tab.Length - 1].CompareTo(mot) >= 0)
            {
                debut = c;
                return RechDichoRecursif(mot, tab, debut, fin);
            }
            else
            {
                if (mot == tab[c])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
