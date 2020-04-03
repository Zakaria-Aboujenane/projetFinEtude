using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalendrierDesArchives.DAO;
using CalendrierDesArchives.Model;

namespace CalendrierDesArchives.Metiers
{
    public class ActionsType
    {
        private TypeDAOSQLServer typeDAOSQLServer;
        public ActionsType()
        {
            typeDAOSQLServer = TypeDAOSQLServer.getInstance();

        }

        public int ajouterType(Model.Type type)
        {

            typeDAOSQLServer = TypeDAOSQLServer.getInstance();
            int id = typeDAOSQLServer.ajouterType(type.nomType, type.duree, type.action);
            return id;
        }

        /*        public void supprimerType(int idType)
                {
                    typeDAOSQLServer = TypeDAOSQLServer.getInstance();
                    typeDAOSQLServer.supprimerType(idType);
                }*/

        public void supprimerType(Model.Type type)
        {
            typeDAOSQLServer = TypeDAOSQLServer.getInstance();
            typeDAOSQLServer.supprimerType(type);
        }

        public void modifierType(Model.Type type)
        {
            typeDAOSQLServer = TypeDAOSQLServer.getInstance();
            typeDAOSQLServer.modifierType(type);
        }

        public List<Model.Type> ListerTypes()
        {
            return typeDAOSQLServer.listerTypes();
        }
        public Model.Type getTypeById(int idType)
        {
            return typeDAOSQLServer.getTypeById(idType);
        }
        public Model.Type getTypeByName(String nom)
        {
            return typeDAOSQLServer.getTypeByName(nom);
        }
        public List<Model.Type> rechercheTypeParNom(String nom)
        {
            return typeDAOSQLServer.rechercheTypeParNom(nom);
        }
    }
}