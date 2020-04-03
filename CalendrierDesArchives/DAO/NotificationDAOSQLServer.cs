﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using CalendrierDesArchives.Model;

namespace CalendrierDesArchives.DAO
{
    public class NotificationDAOSQLServer : NotificationIDAO
    {
       
        private static List<Notification> notifications;
        private static NotificationDAOSQLServer instance = null;
        public static NotificationDAOSQLServer getInstance()
        {
            if (instance == null)
                instance = new NotificationDAOSQLServer();
            return instance;
        }

        private NotificationDAOSQLServer()
        {
            notifications = new List<Notification>();
        }
        public void ajouterNotification(Model.Notification notification)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                String query = $"INSERT INTO Notification(textNotification,DateNotification,IdFichier) values('{notification.textNotification}','{notification.dateNotification}','{notification.idFichier}')";
            }
        }

        public void modifierNotification(Model.Notification notification)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"UPDATE Notification SET textNotification='{notification.textNotification}',  DateNotification='{notification.dateNotification}', IdFichier='{notification.idFichier}' WHERE IdNotification='{notification.idNotification}'");
            }
        }

        public void supprimerNotification(Model.Notification notification)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"DELETE FROM Notification WHERE IdNotification='{notification.idNotification}';");
            }
        }

        public List<Model.Notification> listerNotificationFichier(Model.Fichier fichier)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Model.Notification>($"Select * From Notification Where IdFichier='{fichier.idFichier}';").ToList();
            }
        }

        public List<Model.Notification> listerNotificationUtilisateur(Model.Utilisateur utilisateur)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Model.Notification>($"Select textNotification,u.Nom,u.Prenom " +
                    $"From Notification n, Utilisateur u, GestionNotification g " +
                    $"Where n.IdNotification=g.IdNotification and u.IdUtilisateur=g.IdUtilisateur and g.IdUtilisateur='{utilisateur.idUtilisateur}';").ToList();
            }
        }

        public List<Model.Notification> listerTousNotification()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {

                return connection.Query<Model.Notification>($"Select * From  Notification ").ToList();
            }
        }

       

       
    }
}