using CalendrierDesArchives.DAO;
using CalendrierDesArchives.Model;
using CalendrierDesArchives.Utils;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.Metiers
{
    public class ActionsFichier
    {
        HangFireUtil hangFireUtil;
        private FichierDAOSQLServer fichierDAOSQLServer;
        private TypeDAOSQLServer typeDAOSQLServer;
        public ActionsFichier()
        {
            hangFireUtil = new HangFireUtil(this);
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
        }

        public int ajouterF(Fichier f)
        {
            typeDAOSQLServer = TypeDAOSQLServer.getInstance();
            //
            if (f.type.action == "Destruction")
            {
                f.dateSuppression = f.dateAjout.AddDays(f.type.duree);
            }
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
            int id = fichierDAOSQLServer.ajouterFichier(f);
            f.idFichier = id;
            if(f.type.action == "Destruction")
            {
                hangFireUtil.DestructionSelonAjout(f);
            }
            // ca marche , il reste la creation d'une classe qui gere ca (HangFireUtil)
            //if (f.type.action == "Destruction")
            //{
            //    int idF = id;// id du fichier ajouté
            //    int timeToDelete = f.type.duree;//DUA
            //   // BackgroundJob.Schedule(() => this.NorificationAff(idF, f.nomFichier, f.type.nomType), TimeSpan.FromDays(timeToDelete - 1)
            //   //);
            //   //ajout a la table de notifications
            //    BackgroundJob.Schedule(() => this.supprimerF(idF), TimeSpan.FromDays(timeToDelete)
            //    );
            //}


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
        public List<Fichier> listerParUser(Utilisateur u)
        {
            return fichierDAOSQLServer.listerParUser(u);
        }
        public int ajouterParUser(Utilisateur u, Fichier f)
        {
            return fichierDAOSQLServer.AjouterParUser(u, f);
        }
        public Boolean appartenanceUF(Utilisateur u, Fichier f)
        {
            return fichierDAOSQLServer.appartenanceUF(u, f);
        }
        public List<Fichier> listerFichiersArchive()
        {
            return fichierDAOSQLServer.listerFichiersArchive();
        }
        public List<Fichier> rechercheGenerale(String searsh)
        {
            return fichierDAOSQLServer.rechercheGenerale(searsh);
        }
        public List<Fichier> rechercheGSelonUser(String searsh, Utilisateur u)
        {
            return fichierDAOSQLServer.rechercheGSelonUser(searsh, u);
        }

        public void commencerLesortFinal(Fichier f)
        {
            f.sortFinalComm = 1;
            fichierDAOSQLServer.modifierFichier(f);
        }
    }
}