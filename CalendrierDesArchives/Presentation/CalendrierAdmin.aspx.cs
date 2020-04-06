using CalendrierDesArchives.Metiers;
using CalendrierDesArchives.Model;
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
        protected void Page_Load(object sender, EventArgs e)
        {
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
                if (typeD == "1")
                {
                    foreach (var f in listF)
                    {
                        String dato = f.dateAjout.ToString("yyyy/MM/dd");

                        if (dato == date)
                        {
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
                s += GenerateArchive(f);
            }

            return s;
        }
        //generateur des archives:
        public static String GenerateArchive(Fichier f)
        {
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
                "                                    <a href=\"#\"><i class=\"far fa-eye\"></i> voir le fichier</a><br />\r\n" +
                "                                </td>\r\n" +
                "                                <td><a href=\"#\"><i class=\"fa fa-file-download\"><span class=\"separator\"> </span></i>telecharger</a> </td>\r\n" +
                "                            </tr>\r\n" +
                "                             \r\n" +
                "                        \r\n" +
                "                            \r\n" +
                "                        </table>\r\n" +
                "                       \r\n" +
                "                    </div>\r\n" +
                "                    <div class=\"btnsAr\">\r\n" +
                "                       \r\n";
            if (1==1)
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
            String s = "<li>\r\n" +
            "                                <span class=\"iconeu\"><i style=\"transform:scale(1,1);\" class=\"fas fa-file-archive\"></i></span>\r\n" +
            "                                <span class=\"textNot\">" + n.textNotification + "<a onclick=\"ouvrirFichier(" + n.idFichier + ")\"> Cliquez ici</a> pour ouvrir le fichier</span>\r\n" +
            "                            </li>";
            return s;
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
                s+=GenerateArchive(f);
            }
            if (s == "")
                s = "Aucun Fichier trouve";
            return s;

        }
    }
}