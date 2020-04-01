using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.DAO
{
    public class TypeDAOSQLServer : TypeIDAO
    {
        List<System.Type> types;
        private static TypeDAOSQLServer instance = null;
        public static TypeDAOSQLServer getInstance()
        {
            if (instance == null)
                instance = new TypeDAOSQLServer();
            return instance;
        }
        private TypeDAOSQLServer()
        {
            types = new List<System.Type>();
        }
        public void ajouterType(string nomType, int Duree)
        {
            throw new NotImplementedException();
        }

        public List<Model.Type> listerTypes()
        {
            throw new NotImplementedException();
        }

        public void modifierType(int idType, string nvNom, int nvDuree)
        {
            throw new NotImplementedException();
        }

        public void supprimerType(int idType)
        {
            throw new NotImplementedException();
        }

        public Model.Type getTypeById(int idType)
        {
            throw new NotImplementedException();
            
        }

        public Model.Type getTypeByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}