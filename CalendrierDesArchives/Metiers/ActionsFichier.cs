using CalendrierDesArchives.DAO;
using CalendrierDesArchives.Model;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.Metiers
{
    public class ActionsFichier
    {
        private FichierDAOSQLServer fichierDAOSQLServer;
        private TypeDAOSQLServer typeDAOSQLServer;
        public ActionsFichier()
        {
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
        }

        public void ajouterF(string Nom, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP, String nomType)
        {
            typeDAOSQLServer = TypeDAOSQLServer.getInstance();
            Model.Type type = typeDAOSQLServer.getTypeByName(nomType);
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
            fichierDAOSQLServer.ajouterFichier(Nom, DateAjout, DateModification, DateDernierAcces, DateSuppression, Chemain, extention, idP, type.idType);



            int idF = 0;// methode pour avoir id du fichier ajouté
            int timeToDelete = type.duree;//avoir le avant suppression selon le type du fichier
            BackgroundJob.Schedule(() => this.NorificationAff(idF, Nom, nomType), TimeSpan.FromDays(timeToDelete - 1)
           );
            BackgroundJob.Schedule(() => this.supprimerF(idF), TimeSpan.FromDays(timeToDelete)
            );

        }
        public void supprimerF(int idF)
        {
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
            fichierDAOSQLServer.supprimerFichier(idF);
        }
        public void NorificationAff(int idF, String nomF, String typeF)
        {

        }
        public List<Fichier> listerFichiersParDate(String date)
        {
            return fichierDAOSQLServer.listerLesfichiersParDate(date);
        }

    }
}