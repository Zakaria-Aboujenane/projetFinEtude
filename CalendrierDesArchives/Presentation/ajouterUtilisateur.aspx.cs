using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CalendrierDesArchives.Metiers;

namespace CalendrierDesArchives.Presentation
{
    public partial class ajouterUtilisateur : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUser"] == null && Session["privillege"] != "Admin")
            {
                Response.Redirect("./Authentification.aspx");
            }
        }

        protected void btnAjouterUtilisateur_Click(object sender, EventArgs e)
        {
          
        }

        protected void btnAjouterUtilisateur_Click1(object sender, EventArgs e)
        {
            String nom_Utilisateur = nomUtilisateur.Value;
            String prenom_Utilisateur = prenomUtilisateur.Value;
            String email_Utilisateur = emailUtilisateur.Value;
            String mdp_Utilisateur = mdpUtilisateur.Value;
            String role = selectRole.SelectedValue;
            if (nom_Utilisateur != "" || prenom_Utilisateur != "" || email_Utilisateur != "" || mdp_Utilisateur != "" || role != "")
            {
                ActionsUtilisateur actionsUtilisateur = new ActionsUtilisateur();
                Model.Utilisateur utilisateur = new Model.Utilisateur();
                utilisateur.nom = nom_Utilisateur;
                utilisateur.prenom = prenom_Utilisateur;
                utilisateur.email = email_Utilisateur;
                utilisateur.motDePasse = mdp_Utilisateur;
                utilisateur.privillege = role;
                actionsUtilisateur.ajouterUtilisateur(utilisateur);
                Response.Redirect("./ajouterUtilisateur.aspx");
            }
        }
    }
}