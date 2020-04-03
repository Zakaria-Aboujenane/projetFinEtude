using CalendrierDesArchives.Metiers;
using CalendrierDesArchives.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CalendrierDesArchives.Presentation.FormulairesArchive
{
    public partial class AjouterArchive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            selectTypeAr.DataSource = new ActionsType().ListerTypes();
            selectTypeAr.DataTextField = "nomType";
            selectTypeAr.DataValueField = "idType";
            selectTypeAr.DataBind();
        }

        protected void AddArchBtn_Click(object sender, EventArgs e)
        {
            if (ArchiveUpload.HasFile)
            {
                String ext = System.IO.Path.GetExtension(ArchiveUpload.FileName);
                if(ext != ".pdf")
                {
                    Label1.Text += "Le fichier doit etre format pdf";
                }
                else
                {
                    String titreAr = archiveTitre.Value;
                    String description = DescriptionArchive.Value;
                    DateTime day = DateTime.Now;
                    int idType = 0;
                    Int32.TryParse(selectTypeAr.Value, out idType);
                    Fichier f = new Fichier();
                    f.Nom = titreAr;
                    f.Description = description;
                    f.dateAjout = day;
                    ActionsType actionsType = new ActionsType();
                    f.type = actionsType.getTypeById(idType);
                    int id = new ActionsFichier().ajouterF(f);
                    f.idFichier = id;
                    f.chemain = Server.MapPath("~/Archives/" + "archive-" + id + ".pdf");
                    new ActionsFichier().modifier(f);
                    ArchiveUpload.SaveAs(Server.MapPath("~/Archives/" +"archive-"+id+".pdf"));
                    Response.Redirect("./Calendrier.aspx");
                }
            }
        }
    }
}