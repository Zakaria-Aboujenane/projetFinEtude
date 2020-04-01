using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalendrierDesArchives.DAO;
using CalendrierDesArchives.Model;

namespace CalendrierDesArchives.Metiers
{
    public class ActionsUtilisateur
    {
        private UtilisateurDAOSQLServer utilisateurDAOSQLServer;
        public ActionsUtilisateur()
        {
            utilisateurDAOSQLServer = UtilisateurDAOSQLServer.getInstance();
        }

        public void ajouterUtilisateur(String Nom, String Prenom, String Email, String MotDePasse, String Privillege)
        {
            //utilisateurDAOSQLServer = UtilisateurDAOSQLServer.getInstance();
            utilisateurDAOSQLServer.ajouterUtilisateur(Nom, Prenom, Email, MotDePasse, Privillege);
        }

        public void supprimerUtilisateur(int idUtilisateur)
        {
            //utilisateurDAOSQLServer = UtilisateurDAOSQLServer.getInstance();
            utilisateurDAOSQLServer.supprimerUtilisateur(idUtilisateur);
        }

        public void modifierUtilisateur(int idUtilisateur, String nomUtilisateur, String prenomUtilisateur, String email, String motDePasse, String privillege)
        {
            //utilisateurDAOSQLServer = UtilisateurDAOSQLServer.getInstance();
            utilisateurDAOSQLServer.modifierUtilisateur(idUtilisateur, nomUtilisateur, prenomUtilisateur, email, motDePasse, privillege);
        }


        public List<Model.Utilisateur> listerTousUtilisateur()
        {
            return utilisateurDAOSQLServer.listerTousUtilisateur();
        }
        public Utilisateur rechercheUtilisateurParId(int idUtilisateur)
        {
            return utilisateurDAOSQLServer.rechercheUtilisateurParId(idUtilisateur);
        }
        public List<Model.Utilisateur> rechercheUtilisateurParNom(string nom)
        {
            return utilisateurDAOSQLServer.rechercheUtilisateurParNom(nom);
        }
    }
}