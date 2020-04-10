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
        {
            int joursRest = f.dateSuppression.Subtract(f.dateAjout).Days;
            int jousRestPourNorification = joursRest - NotifComm;

           BackgroundJob.Schedule(
                      () => actionsNotification.RefaireChaquemin(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));
           var JobId =  BackgroundJob.Schedule(
                       () => actionsFichier.supprimerF(f.idFichier),
                       TimeSpan.FromMinutes(joursRest));
            f.HangFireID = JobId;
           // f.HangFireNotificationID = JobNotId;
            actionsFichier.modifier(f);

        }
        // cas ou lesort final est la conservation , selon la date d'ajout 
        public void ConservationSelonAjout(Fichier f)
        {
            int joursRest = f.dateSuppression.Subtract(f.dateAjout).Days;
            int jousRestPourNorification = joursRest - NotifComm;

            BackgroundJob.Schedule(
                      () => actionsNotification.RefaireChaquemin(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));

            var JobId = BackgroundJob.Schedule(
                       () => actionsFichier.commencerLesortFinal(f),
                       TimeSpan.FromMinutes(joursRest));
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
                f.HangFireRecJobNotID != "" && f.HangFireRecJobNotID != null && f.HangFireID != null && f.HangFireNotificationID != null)
            {
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);
                RecurringJob.RemoveIfExists(f.HangFireRecJobNotID);
            }
            //suppression des actions si l'action des notifications n'est pas encore commence:
            if (f.HangFireID != "" && f.HangFireNotificationID != "" && f.HangFireID != null && f.HangFireNotificationID != null)
            {
                BackgroundJob.Delete(f.HangFireNotificationID);
                BackgroundJob.Delete(f.HangFireID);
            }
                f.HangFireRecJobNotID = "";

            //schedulement:
            var JobNotId = BackgroundJob.Schedule(
                      () => actionsNotification.RefaireChaquemin(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));

            var jobID = BackgroundJob.Schedule(
                           () => actionsFichier.supprimerF(f.idFichier),
                           TimeSpan.FromMinutes(joursRest));

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
                      () => actionsNotification.RefaireChaquemin(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));

            var jobID = BackgroundJob.Schedule(
                           () => actionsFichier.commencerLesortFinal(f),
                           TimeSpan.FromMinutes(joursRest));
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
                      () => actionsNotification.RefaireChaquemin(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));

            var jobID = BackgroundJob.Schedule(
                           () => actionsFichier.supprimerF(f.idFichier),
                           TimeSpan.FromMinutes(joursRest));
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
                      () => actionsNotification.RefaireChaquemin(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));

            var jobID = BackgroundJob.Schedule(
                           () => actionsFichier.commencerLesortFinal(f),
                           TimeSpan.FromMinutes(joursRest));
            f.HangFireID = jobID;
            f.HangFireNotificationID = JobNotId;
            actionsFichier.modifier(f);
        }

       
    }
}