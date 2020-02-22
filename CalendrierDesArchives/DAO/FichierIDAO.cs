using CalendrierDesArchives.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CalendrierDesArchives.DAO
{
    interface FichierIDAO
    {
        void ajouterFichier();
        void supprimerFichier();
        void modifierFichier();
        
        List<Fichier> listerLesfichiersParDate(DateTime date);
        List<Fichier> listerTousLesfichiers();

    }
}
