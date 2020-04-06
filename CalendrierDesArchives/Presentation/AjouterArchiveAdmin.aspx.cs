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
    public partial class AjouterArchiveAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUser"] == null && Session["privillege"]!="Admin")
            {
                Response.Redirect("./Authentification.aspx");
            }
            else
            {
                selectTypeAroo.DataSource = new ActionsType().ListerTypes();
                selectTypeAroo.DataTextField = "nomType";
                selectTypeAroo.DataValueField = "idType";
                selectTypeAroo.DataBind();
            }
        }
        protected void BTNADDArch_Click(object sender, EventArgs e)
        {
            if (TitreArch.Value != "" || textArea.InnerHtml != "" || EmpPc.Value != "" || index.Value != "" || selectTypeAroo.SelectedValue != ""
           || ArchiveUpload.HasFile)
            {
                String ext = System.IO.Path.GetExtension(ArchiveUpload.FileName);
                if (ext == ".pdf" || ext == ".docx" || ext == ".png")
                {
                   
                    String titreAr = TitreArch.Value;
                    String description = textArea.InnerHtml;
                    DateTime day = DateTime.Now;
                    int idType = 0;
                    Int32.TryParse(selectTypeAroo.SelectedValue, out idType);
                    Fichier f = new Fichier();
                    f.Nom = titreAr;
                    f.Description = description;
                    f.dateAjout = day;
                    f.extention = ext;
                    f.dateModification = day;
                    f.dateDernierAcces = day;
                    f.index = index.Value;
                    f.emplacementPC = EmpPc.Value;
                    f.dateSuppression = day;
                    ActionsType actionsType = new ActionsType();
                    f.type = actionsType.getTypeById(idType);
                    int id = new ActionsFichier().ajouterF(f);
                    f.idFichier = id;
                    f.chemain = Server.MapPath("~/Archives/" + "archive-" + id + ".pdf");
                    new ActionsFichier().modifier(f);
                    ArchiveUpload.SaveAs(Server.MapPath("~/Archives/" + "archive-" + id + ".pdf"));

                    String indexmsg = "index du archive ajoute est : " + f.index + "";

                    Response.Redirect("./CalendrierAdmin.aspx?indexmsg=" + indexmsg);
                }
                else
                {
                    erreur.InnerHtml = "Le fichier doit etre format pdf,img,docx seulement";
                }

            }
            else
            {
                erreur.InnerHtml = "veuillez remplir tous les champs";
            }
        }
    }
}