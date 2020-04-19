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
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
            int id = fichierDAOSQLServer.ajouterFichier(f);

            f.idFichier = id;
            if(f.type.DUAselon == "DateAjout")
            {
               
                f.dateSuppression = f.dateAjout.AddDays(f.type.duree);
                    modifier(f);
                if (f.type.action == "Destruction")
                {
                    hangFireUtil.DestructionSelonAjout(f);
                }
                else if (f.type.action == "Conservation")
                {
                    hangFireUtil.ConservationSelonAjout(f);
                }
            }
            return id;
        }
        public void supprimerF(int idF)
        {
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
            Fichier f = getFichierById(idF);

            if (f.HangFireRecJobNotID != "" && f.HangFireRecJobNotID != null)
                RecurringJob.RemoveIfExists(f.HangFireRecJobNotID);
            if(f.HangFireNotificationID != "" && f.HangFireNotificationID != null)
                BackgroundJob.Delete(f.HangFireNotificationID);

            fichierDAOSQLServer.supprimerFichier(idF);
            Notification n = new Notification();
        }
        public void modifier(Fichier f)
        {
            fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
            fichierDAOSQLServer.modifierFichier(f);
            
        }
        //hangfire:
        public void modifierSelonHangFire(Fichier f)
        {
            if (f.type.DUAselon == "DateDerniereMod")
            {
                if (f.type.action == "Destruction")
                {
                    hangFireUtil.DestructionSelonModification(f);
                }
                else if (f.type.action == "Conservation")
                {
                    hangFireUtil.ConservationSelonModification(f);
                }
            }
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
            Fichier f2 = getFichierById(f.idFichier);
            f = f2;

            if (f.HangFireNotificationID != null)
            {
                RecurringJob.RemoveIfExists(f.HangFireRecJobNotID);
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);

                f.HangFireNotificationID = 0+"";
                f.HangFireRecJobNotID = 0 + "";
            }
            f.sortFinalComm = 1;
            fichierDAOSQLServer.modifierFichier(f);
            new ActionsNotification().supprimerNotDuFichier(f);
        }
        public void AccesAuFichier(Fichier f)
        {
            if (f.type.action == "Destruction")
            {
                hangFireUtil.DestructionSelonDernerAcces(f);
            }
            else if (f.type.action == "Conservation")
            {
                hangFireUtil.ConservationSelonDernerAcces(f);
            }
        }
    }
}