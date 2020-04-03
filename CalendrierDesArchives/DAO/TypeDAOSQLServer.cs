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
        public int ajouterType(string nomType, int Duree, string action)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                String query = $"INSERT INTO Type(nomType,duree,action) values ('{ nomType}','{ Duree}','{action}');" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int id = connection.Query<int>(query).Single();
                return id;
            }
        }

        public List<Model.Type> listerTypes()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {

                return connection.Query<Model.Type>($"Select * From Type ").ToList();
            }
        }

        public void modifierType(Model.Type type)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"UPDATE Type SET nomType='{type.nomType}',  duree='{type.duree}',action= '{type.action}' WHERE idType ='{type.idType}'");
            }
        }

        public void supprimerType(Model.Type type)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"DELETE FROM Type WHERE idType ='{type.idType}';");
            }
        }
        /*  public void supprimerType(int idType)
          {
              using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
              {
                  connection.Execute($"DELETE FROM GestionType WHERE idType ='{idType}';");
              }
          }*/

        public Model.Type getTypeById(int idType)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                Model.Type type = connection.QuerySingle<Model.Type>($"Select * From Type Where idType='{idType}'");
                return type;
            }
        }

        public Model.Type getTypeByName(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                Model.Type type = (Model.Type)connection.Query<Model.Type>($"Select * From Type Where nomType='{name}'");
                return type;
            }
        }

        public List<Model.Type> rechercheTypeParNom(string type)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                String nomGenere = "%" + type + "%";
                return connection.Query<Model.Type>($"Select * From Type Where nomType like {nomGenere};")
                    .ToList();
            }
        }
    }
}