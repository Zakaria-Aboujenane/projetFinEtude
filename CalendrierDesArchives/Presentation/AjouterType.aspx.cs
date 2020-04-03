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
        
        }

        protected void AddType_Click(object sender, EventArgs e)
        {
            String nomType = txtName.Text.ToString();
            int duree = Convert.ToInt32(txtDuree.Text.ToString());
            String action = Action.Text.ToString();

            ActionsType actionsType = new ActionsType();
            Model.Type t = new Model.Type();
            t.nomType = nomType;
            t.duree = duree;
            t.action = action;
            int idType = actionsType.ajouterType(t);
            Response.Redirect("./Types.aspx");
        }
    }
}