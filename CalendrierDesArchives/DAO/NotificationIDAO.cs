using CalendrierDesArchives.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendrierDesArchives.DAO
{
    interface NotificationIDAO
    {
        int ajouterNotification(Model.Notification notification);
        void ajouterNotifAvecUser(Utilisateur u,Notification n);
        void modifierNotification(Model.Notification notification);
        void supprimerNotification(Model.Notification notification);
        void supprimerNotDuFichier(Fichier f);
        Notification getNotificationByID(int idNotif);
        List<Model.Notification> listerTousNotification();
        List<Model.Notification> listerNotificationUtilisateur(Model.Utilisateur utilisateur);
        List<Model.Notification> listerNotificationFichier(Model.Fichier fichier);
    }
}
