using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalendrierDesArchives.DAO;

namespace CalendrierDesArchives.Metiers
{
    public class ActionsNotification
    {
        private NotificationDAOSQLServer notificationDAOSQLServer;
        public ActionsNotification()
        {
            notificationDAOSQLServer = NotificationDAOSQLServer.getInstance();

        }

        public void ajouterNotification(Model.Notification notification)
        {
            notificationDAOSQLServer = NotificationDAOSQLServer.getInstance();
            notificationDAOSQLServer.ajouterNotification(notification);
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
    }
}