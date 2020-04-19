using CalendrierDesArchives.Metiers;
using CalendrierDesArchives.Model;
using CalendrierDesArchives.Utils;
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
        public static String getArchives(String idtype)
        {
            int type = 0;
            Int32.TryParse(idtype,out type);
            String s = "";
            List<Fichier> fichiers = new ActionsFichier().listerFichiersArchive();
            s += generatebtnRetour();
            foreach (var f in fichiers)
            {
                if(f.idType == type)
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
            "                <div></div>\r\n" +
            "                <p>cliquez pour voir les Archives conservé après qu'ils ont passé leurs DUA de ce type</p>\r\n" +
            "            </div>  \r\n" +
            "        </div>   ";
            return s;
        }
        [WebMethod]
        public static string searchArchives(String search)
        {
            if(search != "")
            {
                List<Fichier> fichiers = new ActionsFichier().rechercheGenerale(search);
                String s = "";
                s += generatebtnRetour();
                foreach (var f in fichiers)
                {
                    if (f.sortFinalComm == 1)
                        s += CalendrierAdmin.GenerateArchive(f);
                }
                if (s == "")
                    s = "Aucun Archive trouve";
                return s;
            }
            else
            {
               String  s = "Veuillez entrer un type a rechercher,un nom ou l'index que vous avez creer";
                return s;
            }
            
        }
        [WebMethod]
        public static string getArchiveInfo(String idArch)
        {
            ActionsFichier actsF = new ActionsFichier();
            int idArchive = 0;
            Int32.TryParse(idArch, out idArchive);
            Fichier f = actsF.getFichierById(idArchive);
            f.dateDernierAcces = DateTime.Now;
            if(f.sortFinalComm == 0)
            {
                f.type = new ActionsType().getTypeById(f.idType);
                if (f.type.DUAselon == "DateDernierAcces")
                {
                    if (f.type.action == "Destruction")
                        new HangFireUtil(actsF).DestructionSelonDernerAcces(f);
                    else if (f.type.action == "Conservation")
                        new HangFireUtil(actsF).ConservationSelonDernerAcces(f);
                }
            }
         
            actsF.modifier(f);
           
            if (f.sortFinalComm == 0)
                return ArchiveInfoGenerateur(f);
            else if (f.sortFinalComm == 1)
                return ArchiveInfoGenerateur(f);
            else
                return "probleme non gere veuillez verifiez votre connection";

        }
        public static String ArchiveInfoGenerateur(Fichier f)
        {
            String desc = HttpUtility.HtmlDecode(f.Description);
            int joursRestants = f.dateSuppression.Subtract(f.dateAjout).Days;
            String s = " <span onclick=\"closeArchiveModal()\" class=\"closeBtnAjout\">&times;</span>\r\n" +
        "                <div class=\"titreArchive\">\r\n" +
        "                    <h1><label>Archive:</label ><label class=\"arch\">"+f.Nom+"</label></h1>\r\n" +
        "                </div>\r\n" +
        "                <div class=\"infosArchives\">\r\n" +
        "                    <label class=\"infos\">Ajoute le:</label><br>\r\n" +
        "                    <label class=\"infos\">"+f.dateAjout.ToString("dd/MM/yyyy")+"</label><br>\r\n" +
        "                    <label class=\"infos\">Date du sort final:</label><br>\r\n" +
        "                    <label class=\"infos\">"+f.dateSuppression.ToString("dd / MM / yyyy")+"</label><br>\r\n" +
        "                    <label class=\"infos\">Date de derniere modification</label><br>\r\n" +
        "                    <label class=\"infos\">"+f.dateModification.ToString("dd/MM/yyyy") + "</label><br>\r\n" +
        "                    <label class=\"infos\">Date de dernier acces a ce fichier:</label><br>\r\n" +
        "                    <label class=\"infos\">"+ f.dateDernierAcces.ToString("dd/MM/yyyy") + "</label><br>\r\n" +
        "                    <label class=\"infos\">Il reste"+joursRestants+" jours pour la "+f.type.action+" ce fichier définitivement</label><br>\r\n" +
                 "                    <label class=\"infos\"><h1 style='Color:green'>Description de l'archive :</h1></label><br>\r\n" +
        "                    <label class=\"infos\">" + desc + "</label><br>\r\n" +
        "                </div>";
            return s;
        }
        public static String ArchiveRetentioneGenerateur(Fichier f)
        {
            String desc = HttpUtility.HtmlDecode(f.Description);
            String s = " <span class=\"closeBtnAjout\">&times;</span>\r\n" +
        "                <div class=\"titreArchive\">\r\n" +
        "                    <h1><label>Archive:</label ><label class=\"arch\">" + f.Nom + "</label></h1>\r\n" +
        "                </div>\r\n" +
        "                <div class=\"pdfViewC\" id=\"pdfViewContaint\">\r\n" +
        "                    hna diri duv haywa hhhhhhhhhhhh\r\n" +
        "                </div>\r\n" +
        "                <div class=\"infosArchives\">\r\n" +
        "                    <label class=\"infos\">Ajoute le:</label><br>\r\n" +
        "                    <label class=\"infos\">" + f.dateAjout.ToString("dd/MM/yyyy") + "</label><br>\r\n" +
        "                    <label class=\"infos\">Date du sort final:</label><br>\r\n" +
        "                    <label class=\"infos\">" + f.dateSuppression.ToString("dd / MM / yyyy") + "</label><br>\r\n" +
        "                    <label class=\"infos\">Date de derniere modification</label><br>\r\n" +
        "                    <label class=\"infos\">" + f.dateModification.ToString("dd/MM/yyyy") + "</label><br>\r\n" +
        "                    <label class=\"infos\">Date de dernier acces a ce fichier:</label><br>\r\n" +
        "                    <label class=\"infos\">" + f.dateDernierAcces.ToString("dd/MM/yyyy") + "</label><br>\r\n" +
                  "                    <label class=\"infos\">cet Archive est conserve depuis le" + f.dateSuppression.ToString("dd / MM / yyyy") + " </label><br>\r\n" +
                  "                    <label class=\"infos\"><h1 style='Color:green'><br/>Description de l'archive :</h1></label><br>\r\n" +
                  "                    <label class=\"infos\">" + desc + "</label><br>\r\n" +
                  "                    \r\n" +
                  "                </div>";
            return s;
        }
    }
}