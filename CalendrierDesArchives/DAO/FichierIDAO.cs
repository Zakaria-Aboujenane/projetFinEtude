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
        void ajouterFichier(string Nom, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP,int idType);
        void supprimerFichier(int id);
        void modifierFichier(int id, string Nom, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP,int idType);
        
        List<Fichier> listerLesfichiersParDate(DateTime date);
        List<Fichier> listerTousLesfichiers();
        //recherche par titre dyal archive
        // recherche par type
        //reherche par 

    }
}
