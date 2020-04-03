using CalendrierDesArchives.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using Hangfire;

namespace CalendrierDesArchives.DAO
{
    public class FichierDAOSQLServer : FichierIDAO
    {
        //singletion:

        private static List<Fichier> fichiers;
        private static FichierDAOSQLServer instance = null;
        public static FichierDAOSQLServer getInstance()
        {
            if (instance == null)
                instance = new FichierDAOSQLServer();
            return instance;
        }
        private FichierDAOSQLServer()
        {

            fichiers = new List<Fichier>();

        }
        public int ajouterFichier(string Nom, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP, int idType, string Description)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                idP = 1;
                Chemain = "";
                extention = ".pdf";
                String dateAjout = DateAjout.ToString();
                String dateMod = "2020-01-01 00:00:00";
                String dateDA = "2020 - 01 - 01 00:00:00";
                String dateSup = DateSuppression.ToString();
                String query = $"INSERT INTO Fichier(Nom,DateAjout, DateModification, DateDernierAcces,  DateSuppression,  Chemain,  extention,idP,idType,Description) " +
                   $"values ('{Nom}','{dateAjout}', '{dateMod}', '{dateDA}', '{dateSup}','{Chemain}', '{extention}','{idP}','{idType}','{Description}' );"+
                   "SELECT CAST(SCOPE_IDENTITY() as int)";
               //recuperation de l'archive ajoute:
                int id = connection.Query<int>(query).Single();
                return id;
            }

        }
        public void supprimerFichier(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"DELETE FROM Fichier WHERE IdFichier='{id}';");
            }
        }
        public void modifierFichier(Fichier f)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"UPDATE Fichier SET Nom='{f.Nom}',Chemain='{f.chemain}',idType={f.type.idType}  WHERE IdFichier = {f.idFichier}");
            }
        }


        public void modifierFichier(int id, string Nom, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP, int idType, string Description)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"UPDATE Fichier SET Nom='{Nom}',  DateAjout='{ DateAjout}', DateModification='{ DateModification}'," +
                    $" DateSuppression='{ DateSuppression}', WHERE Id = @id");
            }
        }
        public List<Fichier> listerLesfichiersParDate(String date)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {

                return connection.Query<Fichier>($"Select * From Fichier Where datediff(day, DateAjout, '{date}')=0 OR datediff(day, DateModification,'{date}')=0 OR datediff(day, DateSuppression, '{date}')=0 ").ToList();
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
        public List<Fichier> listerLesfichiersParDate(DateTime date)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Fichier>($"Select * From Fichier Where ")
                    .ToList();
            }
        }
        public List<Fichier> rechercheFichierParNom(string Nom)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                String nomGenere = "%" + Nom + "%";
                return connection.Query<Fichier>($"Select Nom * From Fichier Where Nom like {nomGenere};")
                    .ToList();
            }
        }

        public List<Fichier> rechercheFichierParType(int idType)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Fichier>($"Select Nom * From Fichier Where idType='{idType}';")
                    .ToList();
            }
        }

        public Fichier getFichierById(int idF)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.QuerySingle<Fichier>($"Select * From Fichier Where idFichier='{idF}';");
            }
        }
    }
}