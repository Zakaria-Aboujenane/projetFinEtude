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
    public partial class Site1 : System.Web.UI.MasterPage
    {
        private int idUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUser"] == null)
            {
                Response.Redirect("./Authentification.aspx");
            }
            String email = Session["email"].ToString();
            String nom = Session["nom"].ToString();
            String prenom = Session["prenom"].ToString();
            String Priv = Session["privillege"].ToString();
            idUser = Int32.Parse(Session["idUser"].ToString());
            emailPH.InnerHtml = email;
            nomPrenomPH.InnerHtml = nom + " " + prenom;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("./Authentification.aspx");
        }
    }
}