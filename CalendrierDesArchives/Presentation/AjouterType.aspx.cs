using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CalendrierDesArchives.Metiers;
using CalendrierDesArchives.Model;

namespace CalendrierDesArchives.Presentation
{
    public partial class AjouterType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUser"] == null && Session["privillege"] != "Admin")
            {
                Response.Redirect("./Authentification.aspx");
            }
        }

        protected void AddType_Click(object sender, EventArgs e)
        {

        }

        protected void BTNADDArch_Click(object sender, EventArgs e)
        {
            String nomType = typeN.Value;
            int duree = Convert.ToInt32(DUA.Value);
            String action = selectAction.SelectedValue;
            String critereSort = selectCritere.SelectedValue;
            String description = textArea.InnerHtml;
            if (nomType != "" || duree != null || action != "" || critereSort != "")
            {
                ActionsType actionsType = new ActionsType();
                Model.Type t = new Model.Type();
                t.nomType = nomType;
                t.duree = duree;
                t.action = action;
                t.DUAselon = critereSort;
                t.Description = description;
                int idType = actionsType.ajouterType(t);
                Response.Redirect("./Types.aspx");
            }

        }
    }
}