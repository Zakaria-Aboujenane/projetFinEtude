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
        int ajouterFichier(Fichier f);
        void supprimerFichier(int id);
        void modifierFichier(Fichier f);
        List<Fichier> listerFichiersArchive();
        List<Fichier> listerLesfichiersParDate(String date);
        List<Fichier> listerTousLesfichiers();
        List<Fichier> rechercheFichierParNom(String Nom);
        List<Fichier> rechercheFichierParType(int idType);
        List<Fichier> listerParUser(Utilisateur user);
        Fichier getFichierById(int idF);
        int AjouterParUser(Utilisateur u, Fichier f);
        Boolean appartenanceUF(Utilisateur u, Fichier f);
        List<Fichier> rechercheGenerale(String searsh);
        List<Fichier> rechercheGSelonUser(String searsh,Utilisateur u);
    }
}