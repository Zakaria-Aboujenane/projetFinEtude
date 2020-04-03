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
        int ajouterFichier(string Nom, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP, int idType, string Description);
        void supprimerFichier(int id);
        void modifierFichier(int id, string Nom, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP, int idType, string Description);
        List<Fichier> listerLesfichiersParDate(DateTime date);
        List<Fichier> listerTousLesfichiers();
        List<Fichier> rechercheFichierParNom(String Nom);
        List<Fichier> rechercheFichierParType(int idType);
        Fichier getFichierById(int idF);


    }
}