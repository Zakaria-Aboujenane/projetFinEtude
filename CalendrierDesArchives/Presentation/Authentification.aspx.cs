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
    public partial class Authentification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                String email = cne.Value;
                String mdp = password.Value;
                Utilisateur u = new Utilisateur();
                ActionsUtilisateur actionsUtilisateur = new ActionsUtilisateur();
                Utilisateur user = actionsUtilisateur.Authentifier(email, mdp);
                if (user != null)
                {
                    Session["email"] = user.email;
                    Session["nom"] = user.nom;
                    Session["prenom"] = user.prenom;
                    Session["privillege"] = user.privillege;
                    Session["idUser"] = user.idUtilisateur;
                    Session.Timeout = 12000;
                    if(user.privillege == "Admin")
                    {
                        Response.Redirect("./CalendrierAdmin.aspx");
                    }else if(user.privillege == "user")
                    {
                        Response.Redirect("./Calendrier.aspx");
                    }
                   
                 }
                else
                {
                    erreur.InnerHtml = "Email ou mot de passe incorecte";
                }
              
            }
        }
    }
}