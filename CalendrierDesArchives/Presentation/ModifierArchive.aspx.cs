using CalendrierDesArchives.Metiers;
using CalendrierDesArchives.Model;
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
            if (!IsPostBack)
            {
                ActionsFichier actionsFichier = new ActionsFichier();
                int id = 0;
                Int32.TryParse(Request.QueryString["idFichier"], out id);
                Fichier f = actionsFichier.getFichierById(id);
                archiveTitre.Value = f.Nom;
                DescriptionArchive.Value = f.Description;
                int val = new ActionsType().getTypeById(f.idType).idType;
                selectTypeAr.DataSource = new ActionsType().ListerTypes();
                selectTypeAr.DataTextField = "nomType";
                selectTypeAr.DataValueField = "idType";
                selectTypeAr.DataBind();
                selectTypeAr.Items.FindByValue(val + "").Selected = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int id = 0;
            Int32.TryParse(Request.QueryString["idFichier"], out id);
            String titreAr = archiveTitre.Value;
            String description = DescriptionArchive.Value;
            DateTime day = DateTime.Now;
            int idType = 0;
            Int32.TryParse(selectTypeAr.Value, out idType);
            Fichier f = new ActionsFichier().getFichierById(id);
            f.Nom = titreAr;
            f.Description = description;
            f.dateModification = day;
            ActionsType actionsType = new ActionsType();
            f.type = actionsType.getTypeById(idType);
            new ActionsFichier().modifier(f);
            Response.Redirect("./Calendrier.aspx");
        }
    }
}