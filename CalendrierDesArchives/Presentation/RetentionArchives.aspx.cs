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
    public partial class RetentionArchives : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUser"] == null)
            {
                Response.Redirect("./Authentification.aspx");
            }
        }
        [WebMethod]
        public static String getTypes()
        {
            String s = "";
            List<Model.Type> types = new ActionsType().ListerTypes();
            foreach (var t in types)
            {
                s += typeGenerator(t);
            }
            return s;
        }
        [WebMethod]
        public static String getArchives()
        {
            String s = "";
            List<Fichier> fichiers = new ActionsFichier().listerFichiersArchive();
            s += generatebtnRetour();
            foreach (var f in fichiers)
            {
                s += CalendrierAdmin.GenerateArchive(f);
            }
            return s;
        }

        public static String generatebtnRetour()
        {
            return " <div onlick=\"getTypes\" class=\"containerbtnRetour\">\r\n" +
            "               <button class=\"Routeur\">Routeur<i class=\"fas fa-chevron-circle-left\"></i></button>\r\n" +
            "            </div>";
        }
        public static String typeGenerator(Model.Type t)
        {
            String s = " <div onclick=\"voirArchives("+t.idType+")\" class=\"contentTousType\">\r\n" +
            "            <div class=\"nomTypes\">\r\n" +
            "                "+t.nomType+"\r\n" +
            "            </div>\r\n" +
            "            <div class=\"dateAjouterTypes\">\r\n" +
            "               <label class=\"dateAjout\">DUA</label>\r\n" +
            "                "+t.duree+"\r\n" +
            "            </div>\r\n" +
            "           \r\n" +
            "            <div class=\"mindescriptionTypes\">\r\n" +
            "                <p>"+t.Description+"</p>\r\n" +
            "                <p>cliquez pour voir les Archives conservé après qu'ils ont passé leurs DUA de ce type</p>\r\n" +
            "            </div>  \r\n" +
            "        </div>   ";
            return s;
        }
        
    }
}