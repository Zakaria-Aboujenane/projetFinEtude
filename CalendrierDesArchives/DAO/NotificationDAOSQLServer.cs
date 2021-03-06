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
        public int ajouterNotification(Model.Notification notification)
        {
            if (FichierDAOSQLServer.getInstance().getFichierById(notification.idFichier) != null)
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
                {
                    String query = $"INSERT INTO Notification(textNotification,DateNotification,IdFichier) values('{notification.textNotification}','{notification.dateNotification}','{notification.idFichier}');" +
                        $"SELECT CAST(SCOPE_IDENTITY() as int)";
                    int id = connection.Query<int>(query).Single();
                    return id;
                }
            }
            else return -1;
        }

        public void modifierNotification(Model.Notification notification)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"UPDATE Notification SET textNotification='{notification.textNotification}',  DateNotification='{notification.dateNotification}',Vu = '{notification.Vu}', IdFichier='{notification.idFichier}' WHERE IdNotification='{notification.idNotification}'");
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
                return connection.Query<Model.Notification>($"Select * From Notification Where IdFichier='{fichier.idFichier}' ORDER BY IdNotification DESC").ToList();
            }
        }

        public List<Model.Notification> listerNotificationUtilisateur(Model.Utilisateur utilisateur)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Model.Notification>($"Select n.* " +
                    $"From Notification n, Utilisateur u, GestionNotification g " +
                    $"Where n.IdNotification=g.IdNotification and u.IdUtilisateur=g.IdUtilisateur and g.IdUtilisateur='{utilisateur.idUtilisateur}' ORDER BY IdNotification DESC;").ToList();
            }
        }

        public List<Model.Notification> listerTousNotification()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {

                return connection.Query<Model.Notification>($"Select * From  Notification ORDER BY IdNotification DESC ").ToList();
            }
        }

        public void supprimerNotDuFichier(Fichier f)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                try
                {
                    connection.Execute($"DELETE FROM Notification WHERE IdFichier='{f.idFichier}';");
                }
                catch (Exception)
                {

                    throw;
                }
               
            }
        }

        public void ajouterNotifAvecUser(Utilisateur u, Notification n)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                String query = $"INSERT INTO GestionNotification(IdNotification,IdUtilisateur) values('{n.idNotification}','{u.idUtilisateur}')";
                connection.Execute(query);
            }
        }

        public Notification getNotificationByID(int idNotif)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {

                return connection.QuerySingle<Model.Notification>($"Select * From  Notification WHERE IdNotification = '{idNotif}'");
            }
        }
    }
}