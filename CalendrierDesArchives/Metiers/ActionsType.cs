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

        public void ajouterType(String nomType, int duree)
        {
            //typeDAOSQLServer = TypeDAOSQLServer.getInstance();
            typeDAOSQLServer.ajouterType(nomType, duree);
        }

        public void supprimerType(int idType)
        {
            //typeDAOSQLServer = TypeDAOSQLServer.getInstance();
            typeDAOSQLServer.supprimerType(idType);
        }

        public void modifierType(int idType, String nomType, int duree)
        {
            //typeDAOSQLServer = TypeDAOSQLServer.getInstance();
            typeDAOSQLServer.modifierType(idType, nomType, duree);
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