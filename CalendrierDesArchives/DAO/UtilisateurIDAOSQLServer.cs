using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalendrierDesArchives.Model;
using Dapper;
using System.Data;

namespace CalendrierDesArchives.DAO
{
    public class UtilisateurDAOSQLServer : UtilisateurIDAO
    {
        private static List<Model.Utilisateur> utilisateurs;
        private static UtilisateurDAOSQLServer instance = null;
        public static UtilisateurDAOSQLServer getInstance()
        {
            if (instance == null)
                instance = new UtilisateurDAOSQLServer();
            return instance;
        }

        private UtilisateurDAOSQLServer()
        {
            utilisateurs = new List<Utilisateur>();
        }
        public void ajouterUtilisateur(string Nom, string Prenom, string Email, string MotDePasse, string Privillege)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"INSERT INTO Utilisateur(Nom,Prenom,Email,MotDePasse,Privillege) " +
                    $"values ('{ Nom}','{ Prenom}', '{Email}', '{MotDePasse}', '{ Privillege}')");
            }
        }

        public List<Utilisateur> listerTousUtilisateur()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {

                return connection.Query<Model.Utilisateur>($"Select * From Utilisateur ").ToList();
            }
        }

        public void modifierUtilisateur(int IdUtilisateur, string Nom, String Prenom, string Email, string MotDePasse, string Privillege)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"UPDATE Utilisateur SET Nom='{Nom}',  Prenom='{ Prenom}', Email='{Email}'," +
                    $" MotDePasse='{ MotDePasse}', Privillege='{Privillege}' WHERE IdUtilisateur = '{IdUtilisateur}';");
            }
        }
        public void supprimerUtilisateur(int IdUtilisateur)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"DELETE FROM Utilisateur WHERE  IdUtilisateur='{IdUtilisateur}';");
            }
        }

        public Utilisateur rechercheUtilisateurParId(int IdUtilisateur)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                
                    Model.Utilisateur utilisateur =connection.QuerySingle<Model.Utilisateur>($"Select * From Utilisateur Where IdUtilisateur='{IdUtilisateur}'");
                return utilisateur;
            }
        }
        public List<Utilisateur> rechercheUtilisateurParNom(string Nom)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                String nomGenere = "%" + Nom + "%";
                return connection.Query<Model.Utilisateur>($"Select Nom * From Utilisateur Where Nom like {nomGenere};")
                    .ToList();
            }
        }

        public void renitialiserTout()
        {
            utilisateurs.Clear();
            utilisateurs = this.listerTousUtilisateur();
        }

        public Utilisateur Authentifier(string email, string motDePasse)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                try
                {
                    Model.Utilisateur utilisateur = connection.QuerySingle<Model.Utilisateur>($"Select * From Utilisateur Where Email='{email}' AND MotDePasse='{motDePasse}'");
                    return utilisateur;
                }
                catch (Exception e)
                {

                    return null;
                }
               
            }
        }
    }
}