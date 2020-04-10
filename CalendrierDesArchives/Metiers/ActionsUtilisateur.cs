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

        public void ajouterUtilisateur(Utilisateur u)
        {
            //utilisateurDAOSQLServer = UtilisateurDAOSQLServer.getInstance();
            utilisateurDAOSQLServer.ajouterUtilisateur(u.nom, u.prenom, u.email, u.motDePasse, u.privillege);
        }

        public void supprimerUtilisateur(Utilisateur u)
        {
            //utilisateurDAOSQLServer = UtilisateurDAOSQLServer.getInstance();
            utilisateurDAOSQLServer.supprimerUtilisateur(u.idUtilisateur);
        }

        public void modifierUtilisateur(Utilisateur u)
        {
            //utilisateurDAOSQLServer = UtilisateurDAOSQLServer.getInstance();
            utilisateurDAOSQLServer.modifierUtilisateur(u.idUtilisateur, u.nom, u.prenom, u.email, u.motDePasse, u.privillege);
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
        public Utilisateur Authentifier(string email, string motDePasse)
        {
            return utilisateurDAOSQLServer.Authentifier(email, motDePasse);
        }
    }
}