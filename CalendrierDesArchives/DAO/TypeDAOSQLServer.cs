using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using Hangfire;
using CalendrierDesArchives.Model;

namespace CalendrierDesArchives.DAO
{
    public class TypeDAOSQLServer : TypeIDAO
    {
        List<Model.Type> types;
        private static TypeDAOSQLServer instance = null;
        public static TypeDAOSQLServer getInstance()
        {
            if (instance == null)
                instance = new TypeDAOSQLServer();
            return instance;
        }
        private TypeDAOSQLServer()
        {
            types = new List<Model.Type>();
        }
        public void ajouterType(string nomType, int Duree,string action)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"INSERT INTO GestionType(Type,duree) values ('{ nomType}','{ Duree}','{action}');");
            }
        }

        public List<Model.Type> listerTypes()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {

                return connection.Query<Model.Type>($"Select * From GestionType ").ToList();
            }
        }

        public void modifierType(int idType, string nvNom, int nvDuree,string nvAction)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"UPDATE GestionType SET Type='{nvNom}',  duree='{nvDuree}',action= '{nvAction}' WHERE idType ='{idType}'");
            }
        }

        public void supprimerType(int idType)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"DELETE FROM GestionType WHERE idType ='{idType}';");
            }
        }

        public Model.Type getTypeById(int idType)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                Model.Type type = (Model.Type)connection.Query<Model.Type>($"Select * From GestionType Where idType='{idType}'");
                return type;
            }
        }

        public Model.Type getTypeByName(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                Model.Type type = (Model.Type)connection.Query<Model.Type>($"Select * From GestionType Where Type='{name}'");
                return type;
            }
        }

        public List<Model.Type> rechercheTypeParNom(string type)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                String nomGenere = "%" + type + "%";
                return connection.Query<Model.Type>($"Select type * From GestionType Where type like {nomGenere};")
                    .ToList();
            }
        }
    }
}