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
            if (Session["idUser"] == null && Session["privillege"] != "Admin")
            {
                Response.Redirect("./Authentification.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    selectTypeAroo.DataSource = new ActionsType().ListerTypes();
                    selectTypeAroo.DataTextField = "nomType";
                    selectTypeAroo.DataValueField = "idType";
                    selectTypeAroo.DataBind();
                }

            }
        }
        protected void BTNADDArch_Click(object sender, EventArgs e)
        {
            if (TitreArch.Value != "" || textArea.InnerHtml != "" || EmpPc.Value != "" || index.Value != "" || selectTypeAroo.SelectedValue != "")
            {
                Fichier f = new Fichier();




                String titreAr = TitreArch.Value;
                String description = textArea.InnerHtml;
                DateTime day = DateTime.Now;
                int idType = 0;
                Int32.TryParse(selectTypeAroo.SelectedValue, out idType);

                f.Nom = titreAr;
                f.Description = description;
                f.dateAjout = day;
                f.dateModification = day;
                f.dateDernierAcces = day;
                f.index = index.Value;
                f.emplacementPC = EmpPc.Value;
                f.dateSuppression = day;
                if (CKRC.Checked)
                    f.commArch = 0;
                else
                    f.commArch = 1;
                ActionsType actionsType = new ActionsType();
                f.type = actionsType.getTypeById(idType);
                int id = new ActionsFichier().ajouterF(f);
                f.idFichier = id;
                if (!CKCE.Checked)
                {

                    String ext = System.IO.Path.GetExtension(ArchiveUpload.FileName);
                    f.extention = ext;
                    if (ext == ".pdf" || ext == ".doc" || ext == ".png")
                    {
                        f.chemain = Server.MapPath("~/Archives/" + "archive-" + id + ".pdf");
                        ArchiveUpload.SaveAs(Server.MapPath("~/Archives/" + "archive-" + id + ".pdf"));
                       
                    }
                    else
                    {
                        erreur.InnerHtml = "veuillez remplir tous les champs";
                        Response.Redirect("./AjouterArchiveAdmin.aspx");
                    }
                    
                }
                new ActionsFichier().modifier(f);


                String indexmsg = "index du archive ajoute est : " + f.index + "";

                Response.Redirect("./CalendrierAdmin.aspx?indexmsg=" + indexmsg);


            }
            else
            {
                erreur.InnerHtml = "veuillez remplir tous les champs";
            }
        }

        protected void CKCE_CheckedChanged(object sender, EventArgs e)
        {
            if (ArchiveUpload.Visible)
            {
                ArchiveUpload.Enabled = false;
                ArchiveUpload.Visible = false;
            }else if (!ArchiveUpload.Visible)
            {
                ArchiveUpload.Enabled = true;
                ArchiveUpload.Visible = true;
            }
        }
    }
}