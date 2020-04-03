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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static String getArchives(String date)
        {
            ActionsFichier actionsFichier = new ActionsFichier();
            List<Fichier> listF = actionsFichier.listerFichiersParDate(date);
            String s = "";
            foreach (var item in listF)
            {
                s += Calendrier.GenerateArchive(item);
            }

            return s;
        }

    [WebMethod]
    public static string DeleteArchive(String idArch,String date)
        {
            int idF = 0;
            Int32.TryParse(idArch,out idF);
            new ActionsFichier().supprimerF(idF);

            //

            ActionsFichier actionsFichier = new ActionsFichier();
            List<Fichier> listF = actionsFichier.listerFichiersParDate(date);
            String s = "";
            foreach (var item in listF)
            {
                s += Calendrier.GenerateArchive(item);
            }

            return s;
        }

        public static String GenerateArchive(Fichier f)
        {
            Model.Type t = new ActionsType().getTypeById(1); 
            return "  <div class=\"archive\">\r\n" +
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
                "                       \r\n" +
                "                        <a href=\"./ModifierArchive.aspx?idFichier="+f.idFichier+"\"><i class=\"fas fa-edit\"></i></a>\r\n" +
                "                        <a onclick='deleteArchive("+f.idFichier+",\""+f.dateAjout+"\")' href=\"#\"><i class=\"fas fa-trash-alt\"></i></a>\r\n" +
                "                    </div>\r\n" +
                "                        \r\n" +
                "                </div>";
        }
    }
}