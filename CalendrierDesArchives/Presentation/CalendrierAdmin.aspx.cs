using CalendrierDesArchives.Metiers;
using CalendrierDesArchives.Model;
using CalendrierDesArchives.Utils;
using Hangfire;
using Hangfire.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CalendrierDesArchives.Presentation
{
    public partial class CalendrierAdmin : System.Web.UI.Page
    {
        int idUser;
        private static int nmbreNotifications = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            nmbreNotifications = 0;
            if (Session["idUser"] == null && Session["privillege"] != "Admin")
            {
                Response.Redirect("./Authentification.aspx");
            }
            idUser = Int32.Parse(Session["idUser"].ToString());
        }
        [WebMethod]
        public static String getArchives(String date, String typeD, String typeS)
        {
            String s = "";
            ActionsFichier actionsFichier = new ActionsFichier();
            List<Fichier> listF;
            if (typeS == "1")//tous
            {
                listF = actionsFichier.listerFichiersParDate(date);
                if (typeD == "1")// selon date ajout
                {
                    foreach (var f in listF)
                    {
                        String dato = f.dateAjout.ToString("yyyy/MM/dd");

                        if (dato == date)
                        {
                            if (f.sortFinalComm == 0)
                                s += CalendrierAdmin.GenerateArchive(f);
                        }
                    }
                }
                else if (typeD == "2")
                {
                    foreach (var f in listF)
                    {
                        String dato = f.dateDernierAcces.ToString("yyyy/MM/dd");
                        if (dato == date)
                        {
                            if (f.sortFinalComm == 0)
                                s += CalendrierAdmin.GenerateArchive(f);
                        }
                    }

                }
                else if (typeD == "3")
                {
                    foreach (var f in listF)
                    {
                        String dato = f.dateModification.ToString("yyyy/MM/dd");
                        if (dato == date)
                        {
                            if (f.sortFinalComm == 0)
                                s += CalendrierAdmin.GenerateArchive(f);
                        }
                    }
                }
                else if (typeD == "4")
                {
                    foreach (var f in listF)
                    {
                        if (f.dateSuppression.ToString() == date)
                        {
                            if (f.sortFinalComm == 0)
                                s += CalendrierAdmin.GenerateArchive(f);
                        }
                    }
                }
                return s;
            }

            if (s == "")
            {
                s = "<h1>rien trouve</h1>";
            }
            return s;
        }


        [WebMethod]
        public static string DeleteArchive(String idArch, String date)
        {
            int idF = 0;
            Int32.TryParse(idArch, out idF);
            new ActionsFichier().supprimerF(idF);

            //
            String s = "";
            List<Fichier> listF = new ActionsFichier().listerFichiersParDate(date);
            foreach (var f in listF)
            {
                if (f.sortFinalComm == 0)
                    s += GenerateArchive(f);
            }

            return s;
        }
        //generateur des archives:
        public static String GenerateArchive(Fichier f)
        {
            String text = f.chemain;
            String url = text.Replace("\\", @"/");
            Model.Type t = new ActionsType().getTypeById(f.idType);
            String s = "  <div class=\"archive\">\r\n" +
                "                    \r\n" +
                "                    <div class=\"photoAr\">\r\n" +
                "                        <img class=\"photoArImg\" src=\"./images/pdf.png\" alt=\"Alternate Text\" />\r\n" +
                "                    </div>\r\n" +
                "                    <div class=\"descripAr\">\r\n" +
                "                        <table class=\"tableArchive\">\r\n" +
                "                            <tr>\r\n" +
                "                                <td>Archive:</td>\r\n" +
                "                                <td> " + f.Nom + " </td>\r\n" +
                "                            </tr>\r\n" +
                "                             <tr>\r\n" +
                "                                <td>Type :</td>\r\n" +
                "                                <td>" + t.nomType + "</td>\r\n" +
                "                            </tr>\r\n" +
                "                            <tr>\r\n" +
                "                                <td>\r\n" +
                "                                    <a onclick='openArchiveModal(" + f.idFichier + ",\"" + url + "\")' href=\"#\"><i class=\"far fa-eye\"></i> voir le fichier</a><br />\r\n" +
                "                                </td>\r\n";
                    if (f.chemain != "")
                    {
                        s += "                                <td><a href=\"DownloadFile.aspx?idFile=" + f.idFichier + "#\"><i class=\"fa fa-file-download\"><span class=\"separator\"> </span></i>telecharger</a> </td>\r\n";
                    }
            s += "                            </tr>\r\n" +
             "                             \r\n" +
             "                        \r\n" +
             "                            \r\n" +
             "                        </table>\r\n" +
             "                       \r\n" +
             "                    </div>\r\n" +
             "                    <div class=\"btnsAr\">\r\n" +
             "                       \r\n";
            if (1 == 1)
            {
                s += "                        <a class=\"aform\" href=\"./ModifierArchiveAdmin.aspx?idFichier=" + f.idFichier + "\"><i class=\"fas fa-edit\"></i></a>\r\n" +
              "                        <a class=\"aform\" onclick='deleteArchive(" + f.idFichier + ",\"" + f.dateAjout + "\")' href=\"#\"><i class=\"fas fa-trash-alt\"></i></a>\r\n";
            }

            s += "                    </div>\r\n" +
        "                        \r\n" +
        "                </div>";
            return s;

        }



        /*Notifications: (de tous les archives)*/

        //  notification pour tous
        [WebMethod]
        public static String getNotifications()
        {
            ActionsNotification actionsNotification = new ActionsNotification();
            List<Notification> nS = actionsNotification.listerTousNotification();
            String s = "";

            foreach (var n in nS)
            {

                s += generateNotif(n);
            }

            return s;
        }

        //generateur de notifications:
        public static String generateNotif(Notification n)
        {
            if (n.Vu == 1)
            {
                String s = "<li>\r\n" +
           "                                <span class=\"iconeu\"><i style=\"transform:scale(1,1);\" class=\"fas fa-file-archive\"></i></span>\r\n" +
           "                                <span class=\"textNot\">" + n.textNotification + "<a onclick=\"ouvrirFichier(" + n.idFichier + ")\"> Cliquez ici</a> pour ouvrir le fichier</span>\r\n" +
           "                            </li>";
                return s;
            }
            else
            {
                String s = "<li>\r\n" +
           "                                <span class=\"iconenonVu\"><i style=\"transform:scale(1,1);\" class=\"fas fa-file-archive\"></i></span>\r\n" +
           "                                <span class=\"textNot\">" + n.textNotification + "<a onclick=\"ouvrirFichier(" + n.idFichier + ");marquerVu(" + n.idNotification + ")\"> Cliquez ici</a> pour ouvrir le fichier</span>\r\n" +
           "                            </li>";
                return s;
            }

        }
        [WebMethod]
        public static String marquerVuNotif(String idNotif)
        {
            int idNot = Int32.Parse(idNotif);
            Notification n = new ActionsNotification().getNotificationByID(idNot);
            if (n.Vu == 0)
            {
                n.Vu = 1;
                new ActionsNotification().modifierNotification(n);
            }

            return getNumNots();
        }
        //calcul de nombre de toutes les notifications Non vues:
        [WebMethod]
        public static String getNumNots()
        {
            int nv = 0;
            if (new Calendrier().Session["idUser"] != null)
            {
                ActionsNotification actionsNotification = new ActionsNotification();
                List<Notification> nS = actionsNotification.listerTousNotification();
                nv = 0;
                foreach (var n in nS)
                {
                    if (n.Vu == 0)
                    {
                        nv++;
                    }
                }
            }
            else
            {
                return "veuillez verifiez votre connection !";
            }
            return nv + "";
        }

        [WebMethod]
        public static string searchArchives(String search)
        {
            List<Fichier> fichiers = new ActionsFichier().rechercheGenerale(search);
            String s = "";

            foreach (var f in fichiers)
            {
                if (f.sortFinalComm == 0)
                    s += GenerateArchive(f);
            }
            if (s == "")
                s = "Aucun Fichier trouve";
            return s;

        }
        [WebMethod]
        public static String afficherArchive(String idArch)
        {
            ActionsFichier ActsFich = new ActionsFichier();
            Fichier f = ActsFich.getFichierById(Int32.Parse(idArch));
            String s = GenerateArchive(f);
            return s;
        }
        [WebMethod]
        public static String commArchivage(String idF)
        {
            ActionsFichier act = new ActionsFichier();
            HangFireUtil hang = new HangFireUtil(act);
            Fichier f = act.getFichierById(Int32.Parse(idF));
            f.commArch = 1;
            act.modifier(f);
            f.type = new ActionsType().getTypeById(f.idType);
            if (f.type.DUAselon == "DateAjout")
            {
                f.dateSuppression = f.dateAjout.AddDays(f.type.duree);
                act.modifier(f);
                if (f.type.action == "Destruction")
                {
                    hang.DestructionSelonAjout(f);
                }
                else if (f.type.action == "Conservation")
                {
                    hang.ConservationSelonAjout(f);
                }
            }
            else if(f.type.DUAselon == "DateDernierAcces")
            {
                if (f.type.action == "Destruction")
                    hang.DestructionSelonDernerAcces(f);
                else if (f.type.action == "Conservation")
                    hang.ConservationSelonDernerAcces(f);
            }

            Historique h = new Historique();
            h.textHistorique = "les regles de conservation sont applique pour l archive "+f.Nom;
            h.IdFichier = f.idFichier;
            h.date = DateTime.Now;
            new ActionsHistorique().ajouterHistorique(h);

            if (f.sortFinalComm == 0)
                return RetentionArchives.ArchiveInfoGenerateur(f);
            else if (f.sortFinalComm == 1)
                return RetentionArchives.ArchiveInfoGenerateur(f);
            else
                return "verifiez votre connection";
        }
    }
}