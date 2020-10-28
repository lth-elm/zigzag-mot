using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_MERSHATI_Laith_Problème
{
    class De
    {
        // Attributs

        string[] de = new string[6];
        string lettreTiree;     // On utilise un string afin d'éviter d'avoir des problème de conversion des "string" aux "char" après lecture de notre fichier


        // Constructeur

        /// <summary>
        /// Créer un dé
        /// </summary>
        /// <param name="De"> On lui entre un tableau de caractère d'une longueur de 6 pour les 6 faces d'un dé </param>
        public De(string[] De)
        {
            if (De.Length == 6)
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (De[i] == null || De[i].Length < 1 || De[i].Length > 1)
                    {
                        Console.WriteLine("Une case de votre dé ne possède pas de lettre, ou bien en possède trop");
                    }
                    else { this.de = De; }
                }
            }
            else { Console.WriteLine("Le dé entrée en paramètre ne contient pas suffisament de lettres"); }
        }


        // Accesseur

        public string LettreTiree
        {
            get { return lettreTiree; }
        }


        // Méthodes

        public void Lance(Random r)
        {
            lettreTiree = de[r.Next(6)]; // De l'index 0 à 5, donc 6 exclue
        }

        public string toString()
        {
            string descriptionDe = "Ce dé est composée des lettres suivants : ";
            for (int i = 0; i <= 5; i++)
            {
                if (i != 5)
                {
                    descriptionDe += Convert.ToString(de[i]) + ", ";
                }
                if (i == 5) // On ne met pas de virgule après le dernier élément du dé
                {
                    descriptionDe += Convert.ToString(de[i]);
                }
            }
            return descriptionDe;
        }
    }
}
