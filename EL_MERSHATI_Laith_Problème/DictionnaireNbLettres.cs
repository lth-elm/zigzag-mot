using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_MERSHATI_Laith_Problème
{
    class DictionnaireNbLettres // Cette classe (dictionnaire par nombres de lettres) permet d'éviter de créer un tableau de tableau de string dans la classe Dictionaire 
    {
        // Attributs

        string[] dico; // Un dictionnaire avec des mots possédant tous le même nombres de lettres
        int nbLettres;
        int nbElements;


        // Constructeur

        public DictionnaireNbLettres(string[] Dico)
        {
            this.dico = Dico;
            this.nbLettres = Dico[0].Length;
            this.nbElements = Dico.Length;
        }


        // Accesseur

        public int NbLettres
        {
            get { return nbLettres; }
        }

        public int NbElements
        {
            get { return nbElements; }
        }

        public string[] Dico
        {
            get { return dico; }
        }
    }
}
