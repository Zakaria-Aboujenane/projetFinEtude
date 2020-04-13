using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CalendrierDesArchives.Presentation
{
    public partial class AdminM : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUser"] == null && Session["privillege"] != "Admin")
            {
                Response.Redirect("./Authentification.aspx");
            }
            String email = Session["email"].ToString();
            String nom = Session["nom"].ToString();
            String prenom = Session["prenom"].ToString();
            String Priv = Session["privillege"].ToString();
            emailPH.InnerHtml = email;
            nomPrenomPH.InnerHtml = nom + " " + prenom;
            Privil.InnerHtml = Priv;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            Response.Redirect("./Authentification.aspx");
        }
    }
}