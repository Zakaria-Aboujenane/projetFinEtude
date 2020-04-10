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
    public partial class Calendrier : System.Web.UI.Page
    {
        public static int idUser = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUser"] == null && Session["privillege"] != "User")
            {
                Response.Redirect("./Authentification.aspx");
            }
            idUser = Int32.Parse(Session["idUser"].ToString());
        }
        [WebMethod]
        public static String getArchives(String date,String typeD,String typeS)
        {
            String s = "";
            Utilisateur u;
            u = new ActionsUtilisateur().rechercheUtilisateurParId(idUser);
            
            if (typeS == "1")//tous
            {
                s = listerTous(u, typeD, date);
                return s;
            }
            else if(typeS == "2")
            {
                s= listerParuserDate(typeD, u, date);
                return s;

            }
            if (s == "")
            {
                s = "rien trouve";
            }
             return s;
        }

    [WebMethod]
    public static string DeleteArchive(String idArch,String date,String typeD,String typeS)
        {
            int idF = 0;
            Int32.TryParse(idArch,out idF);
            new ActionsFichier().supprimerF(idF);
            Utilisateur u;
            u = new ActionsUtilisateur().rechercheUtilisateurParId(idUser);
            //
            String s = "";
            if(typeS == "1")
            {
                return listerTous(u, typeD, date);
            }else if(typeS == "2")
            {
                return listerParuserDate(typeD, u, date);
            }
            return s;
        }

        //recherche dans fichiers user:
        [WebMethod]
        public static String searchArchives(String search,String typeS)
        {
            String s = "";
            Utilisateur u;
            u = new ActionsUtilisateur().rechercheUtilisateurParId(idUser);
            if (typeS == "2")
            {
                List<Fichier> fichiers = new ActionsFichier().rechercheGSelonUser(search, u);
                
                foreach (var f in fichiers)
                {
                    s += GenerateArchive(f, u);
                }
            }else if (typeS == "1")
            {
                List<Fichier> fichiers = new ActionsFichier().rechercheGenerale(search);
                
                foreach (var f in fichiers)
                {
                    s += GenerateArchive(f, u);
                }
            }
            if(s == "")
            {
                s = "<h1 style=\"color:white\" > aucun fichier trouve</h1>";
            }
            return s;
        }


        //Lister Tous archives:
        public static String listerTous(Utilisateur u,String typeD, String date)
        {
            ActionsFichier actionsFichier = new ActionsFichier();
            String s = "";
            List<Fichier> listF;
            listF = actionsFichier.listerFichiersParDate(date);
            if (typeD == "1")
            {
                foreach (var f in listF)
                {
                    String dato = f.dateAjout.ToString("yyyy/MM/dd");

                    if (dato == date)
                    {
                        s += Calendrier.GenerateArchive(f, u);
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
                        s += Calendrier.GenerateArchive(f, u);
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
                        s += Calendrier.GenerateArchive(f, u);
                    }
                }
            }
            else if (typeD == "4")
            {
                foreach (var f in listF)
                {
                    if (f.dateSuppression.ToString() == date)
                    {
                        s += Calendrier.GenerateArchive(f, u);
                    }
                }
            }
            return s;
        }


        //lister les archive de l utilisateur u
        public static String listerParuserDate(String typeD,Utilisateur u,String date)
        {
            String s = "";
            ActionsFichier actionsFichier = new ActionsFichier();
            List<Fichier> listF = actionsFichier.listerParUser(u);
            if (typeD == "1")
            {
                foreach (var f in listF)
                {
                    String dato = f.dateAjout.ToString("yyyy/MM/dd");

                    if (dato == date)
                    {
                        s += Calendrier.GenerateArchive(f, u);
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
                        s += Calendrier.GenerateArchive(f, u);
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
                        s += Calendrier.GenerateArchive(f, u);
                    }
                }
            }
            else if (typeD == "4")
            {
                foreach (var f in listF)
                {
                    if (f.dateSuppression.ToString() == date)
                    {
                        s += Calendrier.GenerateArchive(f, u);
                    }
                }
            }
            return s;
        }


        //generateur des archives pour user:
        public static String GenerateArchive(Fichier f,Utilisateur u)
        {
            Model.Type t = new ActionsType().getTypeById(f.idType);
            String s=  "  <div class=\"archive\">\r\n" +
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
                "                                <td><a href=\"DownloadFile.aspx?idFile="+f.idFichier+"#\"><i class=\"fa fa-file-download\"><span class=\"separator\"> </span></i>telecharger</a> </td>\r\n" +
                "                            </tr>\r\n" +
                "                             \r\n" +
                "                        \r\n" +
                "                            \r\n" +
                "                        </table>\r\n" +
                "                       \r\n" +
                "                    </div>\r\n" +
                "                    <div class=\"btnsAr\">\r\n" +
                "                       \r\n";
                 if(new ActionsFichier().appartenanceUF(u, f))
                    {
                  s += "                        <a href=\"./ModifierArchive.aspx?idFichier=" + f.idFichier + "\"><i class=\"fas fa-edit\"></i></a>\r\n" +
                "                        <a onclick='deleteArchive(" + f.idFichier + ",\"" + f.dateAjout + "\")' href=\"#\"><i class=\"fas fa-trash-alt\"></i></a>\r\n";
                    }

                    s += "                    </div>\r\n" +
                "                        \r\n" +
                "                </div>";
            return s;
               
        }



        //  notification pour user u
         [WebMethod]
        public static String getNotifications()
        {
            ActionsNotification actionsNotification = new ActionsNotification();
            List<Notification> nS = actionsNotification.listerNotificationUtilisateur(new ActionsUtilisateur().rechercheUtilisateurParId(idUser));
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
            "                                <span class=\"textNot\">"+n.textNotification+"<a onclick=\"ouvrirFichier("+n.idFichier+")\"> Cliquez ici</a> pour ouvrir le fichier</span>\r\n" +
            "                            </li>";
            return s;
        }

        //calcul de nombre des notifications pour utilisateur u
        [WebMethod]
        public static String getNumNots()
        {
            int nv = 0;
            if (new Calendrier().Session["idUser"] != null)
            {
                idUser = Int32.Parse(new Calendrier().Session["idUser"].ToString());
                ActionsNotification actionsNotification = new ActionsNotification();
                Utilisateur u = new ActionsUtilisateur().rechercheUtilisateurParId(idUser);
                List<Notification> nS = actionsNotification.listerNotificationUtilisateur(u);
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
            
            return nv+"";
        }
        [WebMethod]
        public static String afficherArchive(String idArch)
        {
            Utilisateur u = new ActionsUtilisateur().rechercheUtilisateurParId(idUser);
            Fichier f = new ActionsFichier().getFichierById(Int32.Parse(idArch));
            String s = GenerateArchive(f,u);
            return s;
        }
    }
}