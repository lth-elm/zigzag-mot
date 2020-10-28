using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_MERSHATI_Laith_Problème
{
    class Joueur
    {
        // Attributs

        string nom;
        int score = 0;
        string[] motTrouve = new string[300]; // Tableau suffisament large pour regrouper l'ensemble des mots trouvés par le joueur

        int libre = 0; // Retient l'index de la première case null du tableau de mot trouvé afin de ne pas avoir à parcourir le tableau à la recherche d'une case libre à chaque fois que le joueur entre un mot


        // Constructeur

        public Joueur(string Nom)
        {
            this.nom = Nom;
        }


        // Accesseur

        public string Nom
        {
            get { return nom; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }


        // Méthodes

        /// <summary>
        /// Recherche si un mot a déjà été trouvé par le joueur ou non
        /// </summary>
        /// <param name="mot"> Mot entrée par le joueur </param>
        /// <returns> vrai ou faux selon si le mot a déjà été trouvé ou non </returns>
        public bool Contain(string mot)
        {
            mot = mot.ToUpper();
            bool appartient = false;
            for (int i = 0; motTrouve[i] != null; i++)
            {
                if (motTrouve[i] == mot)
                {
                    appartient = true;
                }
            }
            return appartient;
        }

        public void Add_Mot(string mot)
        {
            motTrouve[libre] = mot.ToUpper();
            libre++; // La case initialement null venant d'être rempli, on prendra la prochaine fois celle qui suit
        }

        public string toString()
        {
            return nom + " a un score de " + score + " point(s), et a trouvé les mots suivants : " + toStringMots();
        }

        /// <summary>
        /// Parcours le tableau de mots trouvées pour en récupérer les mots
        /// </summary>
        /// <returns> Une chaine de caractère décrivant ces mots à la suite et séparé d'un espace </returns>
        public string toStringMots()
        {
            string mots = null;
            for (int i = 0; motTrouve[i] != null; i++)
            {
                mots += motTrouve[i] + " ";
            }
            return mots;
        }
    }
}
