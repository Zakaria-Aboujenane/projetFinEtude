using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendrierDesArchives.DAO
{
    interface NotificationIDAO
    {
        void ajouterNotification(Model.Notification notification);
        void modifierNotification(Model.Notification notification);
        void supprimerNotification(Model.Notification notification);
        List<Model.Notification> listerTousNotification();
        List<Model.Notification> listerNotificationUtilisateur(Model.Utilisateur utilisateur);
        List<Model.Notification> listerNotificationFichier(Model.Fichier fichier);
    }
}
