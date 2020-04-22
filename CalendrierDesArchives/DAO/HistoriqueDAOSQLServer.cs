using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalendrierDesArchives.Model;
using Dapper;
using System.Data;

namespace CalendrierDesArchives.DAO
{
    public class HistoriqueDAOSQLServer : HistoriqueIDAO
    {
        private static List<Historique> historiques;
        private static HistoriqueDAOSQLServer instance = null;
        public static HistoriqueDAOSQLServer getInstance()
        {
            if (instance == null)
                instance = new HistoriqueDAOSQLServer();
            return instance;
        }
        private HistoriqueDAOSQLServer()
        {
            historiques = new List<Historique>();
        }

        public void ajouterHistorique(Historique h)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"INSERT INTO Historique(textHistorique,date,idFichier) values('{h.textHistorique}','{h.date}','{h.IdFichier}')");
            }
        }
        public List<Historique> listerTousHistoriques()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Historique>($"Select * From Historique ORDER BY IdHistorique DESC")
                    .ToList();
            }
        }

        public void supprimerHistorique(Historique h)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"DELETE FROM Historique WHERE IdHistorique='{h.IdHistorique}';");
            }
        }
    }
}