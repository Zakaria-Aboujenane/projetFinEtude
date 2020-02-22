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
            fichiers = this.listerTousLesfichiers();

        }
        public void ajouterFichier()
        {
            throw new NotImplementedException();
        }

        public void supprimerFichier()
        {
            throw new NotImplementedException();
        }

        public void modifierFichier()
        {
            throw new NotImplementedException();
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
    }
}