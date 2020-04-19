using CalendrierDesArchives.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using CalendrierDesArchives.Metiers;

namespace CalendrierDesArchives.Utils
{
    public class HangFireUtil
    {
        const int NotifComm = 5;
        ActionsFichier actionsFichier;
        ActionsNotification actionsNotification;
        public HangFireUtil(ActionsFichier actionsFichier)
        {
            this.actionsFichier = actionsFichier;
            this.actionsNotification = ActionsNotification.getInstance();
        }
        // cas ou le sort final est la suppression , selon la date d'ajout
        public void DestructionSelonAjout(Fichier f)
        {//calcul de temps restant pour notification et pour la suppression:
            int joursRest = f.dateSuppression.Subtract(f.dateAjout).Days;
            int jousRestPourNorification = joursRest - NotifComm;

            BackgroundJob.Schedule(//programmer l'affichage de notifications
                       () => actionsNotification.RefaireNotifChaqueJour(f),
                       TimeSpan.FromDays(jousRestPourNorification));

            var JobId = BackgroundJob.Schedule(
                        () => actionsFichier.supprimerF(f.idFichier),
                        TimeSpan.FromDays(joursRest));

            f.HangFireID = JobId;
            actionsFichier.modifier(f);
        }
        // cas ou lesort final est la conservation , selon la date d'ajout 
        public void ConservationSelonAjout(Fichier f)
        {
            int joursRest = f.dateSuppression.Subtract(f.dateAjout).Days;
            int jousRestPourNorification = joursRest - NotifComm;
            BackgroundJob.Schedule(
                      () => actionsNotification.RefaireNotifChaqueJour(f),
                      TimeSpan.FromDays(jousRestPourNorification));
            var JobId = BackgroundJob.Schedule(
                       () => actionsFichier.commencerLesortFinal(f),
                       TimeSpan.FromDays(joursRest));
            f.HangFireID = JobId;
            actionsFichier.modifier(f);
        }



        //cas ou le sort final est la suppression selon la date de derniere modification
        public void DestructionSelonModification(Fichier f)
        {
            f.dateSuppression = DateTime.Now.AddDays(f.type.duree);
            f.dateModification = DateTime.Now;
            int joursRest = f.type.duree;
            int jousRestPourNorification = joursRest - NotifComm;
            //on doit d'abord supprimer l'action de la modification avant celle ci si elle existe:

            if (f.HangFireID != "" && f.HangFireNotificationID != "" &&
                f.HangFireRecJobNotID != "" && f.HangFireRecJobNotID != null &&
                f.HangFireID != null && f.HangFireNotificationID != null)
            {
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);
                RecurringJob.RemoveIfExists(f.HangFireRecJobNotID);
            }
            //suppression des actions si l'action des notifications n'est pas encore commence:
            if (f.HangFireID != "" && f.HangFireNotificationID != "" &&
                f.HangFireID != null && f.HangFireNotificationID != null)
            {
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);
            }
            f.HangFireRecJobNotID = "";

            //schedulement:
            var JobNotId = BackgroundJob.Schedule(
                      () => actionsNotification.RefaireNotifChaqueJour(f),
                      TimeSpan.FromDays(jousRestPourNorification));

            var jobID = BackgroundJob.Schedule(
                           () => actionsFichier.supprimerF(f.idFichier),
                           TimeSpan.FromDays(joursRest));

            //sauvegarde des actions:
            f.HangFireID = jobID;
            f.HangFireNotificationID = JobNotId;
            actionsFichier.modifier(f);
        }
        // cas ou le sort final est la destruction selon la date de derniere modification
        public void ConservationSelonModification(Fichier f)
        {
            f.dateSuppression = DateTime.Now.AddDays(f.type.duree);
            f.dateModification = DateTime.Now;
            int joursRest = f.type.duree;
            int jousRestPourNorification = joursRest - NotifComm;
            //on doit d'abord supprimer l'action de la modification avant celle ci si elle existe:
            if (f.HangFireID != "" && f.HangFireNotificationID != "" &&
                f.HangFireRecJobNotID != "" && f.HangFireRecJobNotID != null && f.HangFireID != null && f.HangFireNotificationID != null)
            {
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);
                RecurringJob.RemoveIfExists(f.HangFireRecJobNotID);
            }
            if (f.HangFireID != "" && f.HangFireNotificationID != "" && f.HangFireID != null && f.HangFireNotificationID != null)
            {
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);
            }
            f.HangFireRecJobNotID = "";
            var JobNotId = BackgroundJob.Schedule(
                      () => actionsNotification.RefaireNotifChaqueJour(f),
                      TimeSpan.FromDays(jousRestPourNorification));

            var jobID = BackgroundJob.Schedule(
                           () => actionsFichier.commencerLesortFinal(f),
                           TimeSpan.FromDays(joursRest));
            f.HangFireID = jobID;
            f.HangFireNotificationID = JobNotId;
            actionsFichier.modifier(f);
        }

        // cas ou le sort final est la conservation selon la date du dernier acces a l archive
        public void DestructionSelonDernerAcces(Fichier f)
        {
            f.dateSuppression = DateTime.Now.AddDays(f.type.duree);
            f.dateDernierAcces = DateTime.Now;
            int joursRest = f.type.duree;
            int jousRestPourNorification = joursRest - NotifComm;
            //on doit d'abord supprimer l'action de la modification avant celle ci si elle existe:
            if (f.HangFireRecJobNotID != null && f.HangFireID != null
                && f.HangFireNotificationID != null && !f.HangFireRecJobNotID.Equals("") && !f.HangFireID.Equals("")
                && !f.HangFireNotificationID.Equals(""))
            {
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);
                RecurringJob.RemoveIfExists(f.HangFireRecJobNotID);
                new ActionsNotification().supprimerNotDuFichier(f);
            }
            if (f.HangFireID != null && f.HangFireNotificationID != null && f.HangFireID != "" && f.HangFireNotificationID != "")
            {
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);
                new ActionsNotification().supprimerNotDuFichier(f);
            }
            f.HangFireRecJobNotID = "";
            var JobNotId = BackgroundJob.Schedule(
                      () => actionsNotification.RefaireNotifChaqueJour(f),
                      TimeSpan.FromDays(jousRestPourNorification));

            var jobID = BackgroundJob.Schedule(
                           () => actionsFichier.supprimerF(f.idFichier),
                           TimeSpan.FromDays(joursRest));
            f.HangFireID = jobID;
            f.HangFireNotificationID = JobNotId;
            actionsFichier.modifier(f);
        }
        public void ConservationSelonDernerAcces(Fichier f)
        {
            f.dateSuppression = DateTime.Now.AddDays(f.type.duree);
            f.dateDernierAcces = DateTime.Now;

            int joursRest = f.type.duree;
            int jousRestPourNorification = joursRest - NotifComm;

            //on doit d'abord supprimer l'action de la modification avant celle ci si elle existe:
            if (f.HangFireID != "" && f.HangFireNotificationID != "" &&
                f.HangFireRecJobNotID != "" && f.HangFireRecJobNotID != null && f.HangFireID != null && f.HangFireNotificationID != null)
            {
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);
                RecurringJob.RemoveIfExists(f.HangFireRecJobNotID);
                new ActionsNotification().supprimerNotDuFichier(f);
            }
            if (f.HangFireID != "" && f.HangFireNotificationID != "" && f.HangFireID != null && f.HangFireNotificationID != null)
            {
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);
                new ActionsNotification().supprimerNotDuFichier(f);
            }
            f.HangFireRecJobNotID = "";
            var JobNotId = BackgroundJob.Schedule(
                      () => actionsNotification.RefaireNotifChaqueJour(f),
                      TimeSpan.FromDays(jousRestPourNorification));

            var jobID = BackgroundJob.Schedule(
                           () => actionsFichier.commencerLesortFinal(f),
                           TimeSpan.FromDays(joursRest));
            f.HangFireID = jobID;
            f.HangFireNotificationID = JobNotId;
            actionsFichier.modifier(f);
        }


    }
}