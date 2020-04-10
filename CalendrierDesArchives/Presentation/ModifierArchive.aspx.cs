using CalendrierDesArchives.Metiers;
using CalendrierDesArchives.Model;
using CalendrierDesArchives.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CalendrierDesArchives.Presentation
{
    public partial class ModifierArchive : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUser"] == null && Session["privillege"] != "User")
            {
                Response.Redirect("./Authentification.aspx");
            }

            if (!IsPostBack)
            {
                ActionsFichier actionsFichier = new ActionsFichier();
                int id = 0;
                Int32.TryParse(Request.QueryString["idFichier"], out id);
                Fichier f = actionsFichier.getFichierById(id);
                TitreArch.Value = f.Nom;
                EmpPc.Value = f.emplacementPC;
                index.Value = f.index;
                ArchiveUpload.Enabled = false;
                textArea.InnerHtml = f.Description;
                selectTypeAroo.DataSource = new ActionsType().ListerTypes();
                selectTypeAroo.DataTextField = "nomType";
                selectTypeAroo.DataValueField = "idType";
                selectTypeAroo.DataBind();
                selectTypeAroo.SelectedValue = f.idType + "";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        }


        protected void BTNADDArch_Click(object sender, EventArgs e)
        {

            if (TitreArch.Value != "" || textArea.Value != "" || EmpPc.Value != "" || index.Value != "" || selectTypeAroo.SelectedValue != "")
            {
                String stringidF = Request.QueryString["idFichier"];
                int idF;
                idF = Int32.Parse(stringidF);

                String titreAr = TitreArch.Value;
                String description = textArea.InnerHtml;
                DateTime day = DateTime.Now;
                int idType = 0;
                Int32.TryParse(selectTypeAroo.SelectedValue, out idType);
                Fichier f = new ActionsFichier().getFichierById(idF);

                f.Nom = titreAr;
                f.Description = description;
                f.dateModification = day;
                f.index = index.Value;
                f.emplacementPC = EmpPc.Value;
                f.idType = Int32.Parse(selectTypeAroo.SelectedValue);
                f.type = new ActionsType().getTypeById(f.idType);
                new ActionsFichier().modifier(f);
                if (f.type.DUAselon == "DateDerniereMod")
                {
                    new ActionsFichier().modifierSelonHangFire(f);
                }
                String indexmsg = "le fichier :" + f.Nom + " a ete bien modifier, index :" + f.index + "";
                Response.Redirect("./Calendrier.aspx?indexmsg=" + indexmsg);
            }
            else
            {
                erreur.InnerHtml = "veuillez remplir tous les champs";
            }


        }
    }
}
