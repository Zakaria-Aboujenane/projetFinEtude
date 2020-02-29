using CalendrierDesArchives.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;

namespace CalendrierDesArchives.DAO
{
    public class FichierDAOSQLServer : FichierIDAO
    {
        
        private static List<Fichier> fichiers;
        private static FichierDAOSQLServer instance = null;
        public static FichierDAOSQLServer getInstance()
        {
            if (instance== null)
                instance = new FichierDAOSQLServer();
            return instance;
        }
        private FichierDAOSQLServer()
        {
            
            fichiers = new List<Fichier>();

        }
        public void ajouterFichier(string Nom, string Type, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"INSERT INTO Fichier(Nom,Type,DateAjout, DateModification, DateDernierAcces,  DateSuppression,  Chemain,  extention,idP) " +
                    $"values ('{ Nom}','{ Type}','{ DateAjout}', '{DateModification}', '{ DateDernierAcces}', '{ DateSuppression}','{Chemain}', '{extention}','{idP}')");
            }
        }
        void FichierIDAO.supprimerFichier(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"");
            }
        }
        void FichierIDAO.modifierFichier(int id, string Nom, string Type, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"");
            }
        }
        public List<Fichier> listerLesfichiersParDate(DateTime date)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Fichier>($"Select * From Fichier Where DateAjout = '{date}' OR DateModification = '{date}' OR DateSuppression = '{date}' ").ToList();
            }
            
        }
        public List<Fichier> listerTousLesfichiers()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Fichier>($"Select * From Fichier")
                    .ToList();
            }
        }
        public void renitialiserTout()
        {
            fichiers.Clear();
            fichiers = this.listerTousLesfichiers();
        }
        public void renitialiserDate(DateTime date)
        {
            fichiers.Clear();
            fichiers = this.listerLesfichiersParDate(date);
        }
    }
}