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
                      () => RefaireNotificationChaqueJour(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));
            BackgroundJob.Schedule(
                       () => actionsFichier.supprimerF(f.idFichier),
                       TimeSpan.FromMinutes(joursRest));

        }
        // cas ou lesort final est la conservation , selon la date d'ajout 
        public void ConservationSelonAjout(Fichier f)
        {
            int joursRest = f.dateSuppression.Subtract(f.dateAjout).Days;
            int jousRestPourNorification = joursRest - NotifComm;

            BackgroundJob.Schedule(
                      () => RefaireNotificationChaqueJour(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));

            BackgroundJob.Schedule(
                       () => actionsFichier.commencerLesortFinal(f),
                       TimeSpan.FromMinutes(joursRest));
        }
        

        //cas ou le sort final est la suppression selon la date de derniere modification
        public void DestructionSelonModification(Fichier f)
        {
            f.dateSuppression = DateTime.Now.AddDays(f.type.duree);
            f.dateModification = DateTime.Now;

            int joursRest = f.type.duree;
            int jousRestPourNorification = joursRest - NotifComm;

            BackgroundJob.Schedule(
                      () => RefaireNotificationChaqueJour(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));

            var jobID = BackgroundJob.Schedule(
                           () => actionsFichier.supprimerF(f.idFichier),
                           TimeSpan.FromMinutes(joursRest));
            f.HangFireID = jobID;
            actionsFichier.modifier(f);
        }
        // cas ou le sort final est la destruction selon la date de derniere modification
        public void ConservationSelonModification(Fichier f)
        {
            f.dateSuppression = DateTime.Now.AddDays(f.type.duree);
            f.dateModification = DateTime.Now;

            int joursRest = f.type.duree;
            int jousRestPourNorification = joursRest - NotifComm;

             BackgroundJob.Schedule(
                      () => RefaireNotificationChaqueJour(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));
            //si le job est deja commence(on a deja modifie ce fichier et on veut le modifier autre fois)
            if(f.HangFireID != null)
            {
                BackgroundJob.Delete(f.HangFireID);
            }
            // en tous les cas on doit creer un nouveau:
            var jobID = BackgroundJob.Schedule(
                       () => actionsFichier.commencerLesortFinal(f),
                       TimeSpan.FromMinutes(joursRest));
            f.HangFireID = jobID;
            actionsFichier.modifier(f);
        }

        // cas ou le sort final est la conservation selon la date du dernier acces a l archive
        public void DestructionSelonDernerAcces(Fichier f)
        {
            f.dateSuppression = DateTime.Now.AddDays(f.type.duree);
            f.dateDernierAcces = DateTime.Now;

            int joursRest = f.type.duree;
            int jousRestPourNorification = joursRest - NotifComm;

            BackgroundJob.Schedule(
                      () => RefaireNotificationChaqueJour(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));

            if (f.HangFireID != null)
            {
                BackgroundJob.Delete(f.HangFireID);
            }

            var jobId = BackgroundJob.Schedule(
                       () => actionsFichier.supprimerF(f.idFichier),
                       TimeSpan.FromMinutes(joursRest));
            f.HangFireID = jobId;
            actionsFichier.modifier(f);
        }
        public void ConservationSelonDernerAcces(Fichier f)
        {
            f.dateSuppression = DateTime.Now.AddDays(f.type.duree);
            f.dateDernierAcces = DateTime.Now;

            int joursRest = f.type.duree;
            int jousRestPourNorification = joursRest - NotifComm;

            BackgroundJob.Schedule(
                      () => RefaireNotificationChaqueJour(f),
                      TimeSpan.FromMinutes(jousRestPourNorification));

            if (f.HangFireID != null)
            {
                BackgroundJob.Delete(f.HangFireID);
            }

            var jobId = BackgroundJob.Schedule(
                       () => actionsFichier.commencerLesortFinal(f),
                       TimeSpan.FromMinutes(joursRest));
            f.HangFireID = jobId;
            actionsFichier.modifier(f);
        }

        // 5 jours avant le sort final :
        public void RefaireNotificationChaqueJour(Fichier f)// re envoyer une notification chaque jour
        {
            // recuring job: --  every day:
            RecurringJob.AddOrUpdate(() => actionsNotification.ajouterNotificationPour(f), Cron.Daily);
        }


        
    }
}