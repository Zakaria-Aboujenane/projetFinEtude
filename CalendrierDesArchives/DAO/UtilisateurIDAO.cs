using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendrierDesArchives.Model;

namespace CalendrierDesArchives.DAO
{
    interface UtilisateurIDAO
    {
        void ajouterUtilisateur(string Nom, String Prenom, String Email, String MotDePasse, String Privillege);
        void supprimerUtilisateur(int IdUtilisateur);
        void modifierUtilisateur(int IdUtilisateur, String Nom, String Prenom, String Email, String MotDePasse, String Privillege);
        List<Utilisateur> listerTousUtilisateur();
        Utilisateur rechercheUtilisateurParId(int IdUtilisateur);
        List<Utilisateur> rechercheUtilisateurParNom(String Nom);

    }
}