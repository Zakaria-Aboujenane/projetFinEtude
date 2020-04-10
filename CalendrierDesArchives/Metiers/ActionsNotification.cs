using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalendrierDesArchives.DAO;
using CalendrierDesArchives.Model;
using Hangfire;

namespace CalendrierDesArchives.Metiers
{
    public class ActionsNotification
    {
        private NotificationDAOSQLServer notificationDAOSQLServer;
        private static ActionsNotification instance;
        public static ActionsNotification getInstance()
        {
            if (instance == null)
                instance = new ActionsNotification();
            return instance;
        }
        public ActionsNotification()
        {
            
            notificationDAOSQLServer = NotificationDAOSQLServer.getInstance();
            instance = this;
        }

        public int ajouterNotification(Model.Notification notification)
        {
            notificationDAOSQLServer = NotificationDAOSQLServer.getInstance();
           return notificationDAOSQLServer.ajouterNotification(notification);
        }

        public void supprimerNotification(Model.Notification notification)
        {
           // notificationDAOSQLServer = NotificationDAOSQLServer.getInstance();
            notificationDAOSQLServer.supprimerNotification(notification);
        }

        public void modifierNotification(Model.Notification notification)
        {
            //notificationDAOSQLServer = NotificationDAOSQLServer.getInstance();
            notificationDAOSQLServer.modifierNotification(notification);
        }

        public List<Model.Notification> listerTousNotification()
        {
            return notificationDAOSQLServer.listerTousNotification();
        }

        public List<Model.Notification> listerNotificationUtilisateur(Model.Utilisateur utilisateur)
        {
            return notificationDAOSQLServer.listerNotificationUtilisateur(utilisateur);
        }

        public List<Model.Notification> listerNotificationFichier(Model.Fichier fichier)
        {
            return notificationDAOSQLServer.listerNotificationFichier(fichier);
        }
        public void  ajouterNotificationPour(Fichier f)
        {
        
            int joursRest = f.dateSuppression.Subtract(f.dateAjout).Days;
            Notification notif = new Notification();
            f.type = new ActionsType().getTypeById(f.idType);
            notif.dateNotification = f.dateAjout;
            notif.idFichier = f.idFichier;
            notif.textNotification = "La " + f.type.action + " de ce fichier est après " + joursRest;
            notif.Vu = 0;
            int idNotif = ajouterNotification(notif);
            notif.idNotification = idNotif;
            List<Utilisateur> users = new ActionsUtilisateur().listerTousUtilisateur();
            foreach (var u in users)
            {
                if (new ActionsFichier().appartenanceUF(u, f))
                {
                    new ActionsNotification().ajouterNotifAvecUser(u, notif);
                }
            }
        }

        public void RefaireChaquemin(Fichier f)
        {
            String rec = "id"+f.idFichier;
            f = new ActionsFichier().getFichierById(f.idFichier);
            f.HangFireRecJobNotID = rec;
            new ActionsFichier().modifier(f);
            RecurringJob.AddOrUpdate(rec,() => ajouterNotificationPour(f), Cron.Minutely);
          
        }
        public void supprimerNotDuFichier(Fichier f)
        {
            notificationDAOSQLServer.supprimerNotDuFichier(f);
        }
        public void ajouterNotifAvecUser(Utilisateur u, Notification n)
        {
            notificationDAOSQLServer.ajouterNotifAvecUser(u, n);
        }
    }
}