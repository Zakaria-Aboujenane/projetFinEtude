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

        public int ajouterF(Fichier f)
        {
            typeDAOSQLServer = TypeDAOSQLServer.getInstance();
            //
            if(f.type.action == "Destruction")
            {
                f.dateSuppression = f.dateAjout.AddDays(f.type.duree);
            }
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
            int id = fichierDAOSQLServer.ajouterFichier(f.Nom, f.dateAjout, f.dateModification, f.dateDernierAcces, f.dateSuppression, f.chemain, f.extention, f.idParent, f.type.idType,f.Description);


            if (f.type.action == "Destruction")
            {
                int idF = id;// id du fichier ajouté
                int timeToDelete = f.type.duree;//DUA
               // BackgroundJob.Schedule(() => this.NorificationAff(idF, f.nomFichier, f.type.nomType), TimeSpan.FromDays(timeToDelete - 1)
               //);
               //ajout a la table de notifications
                BackgroundJob.Schedule(() => this.supprimerF(idF), TimeSpan.FromDays(timeToDelete)
                );
            }
          

            return id;
        }
        public void supprimerF(int idF)
        {
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
            fichierDAOSQLServer.supprimerFichier(idF);
        }
        public void modifier(Fichier f)
        {
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
            fichierDAOSQLServer.modifierFichier(f);
        }
        public void NorificationAff(int idF, String nomF, String typeF)
        {

        }
        public List<Fichier> listerFichiersParDate(String date)
        {
            return fichierDAOSQLServer.listerLesfichiersParDate(date);
        }
        public Fichier getFichierById(int idF)
        {
            return fichierDAOSQLServer.getFichierById(idF);
        }

    }
}