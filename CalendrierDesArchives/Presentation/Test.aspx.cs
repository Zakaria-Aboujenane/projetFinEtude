using CalendrierDesArchives.DAO;
using CalendrierDesArchives.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CalendrierDesArchives.Presentation
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            string[] row = { "ko", "ka", "ki" };
            FichierDAOSQLServer fichierDAOSQLServer = FichierDAOSQLServer.getInstance();
            List<Fichier> fichiers = fichierDAOSQLServer.listerTousLesfichiers();
            foreach (var item in fichiers)
            {
                TextBox2.Text += item.nomFichier;
            }
     
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}