﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalendrierDesArchives.DAO;
using CalendrierDesArchives.Model;

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
        public void  ajouterNotificationPour(Fichier f)
        {
            int joursRest = f.dateSuppression.Subtract(f.dateAjout).Days;
            Notification notif = new Notification();
            notif.dateNotification = f.dateAjout;
            notif.idFichier = f.idFichier;
            notif.textNotification = "La "+f.type.action+" de ce fichier est apres "+joursRest+
                " cliquez ici pour voir le fichier";
            notif.Vu = 0;
            notif.idNotification = 3;
            ajouterNotification(notif);
        }
    }
}