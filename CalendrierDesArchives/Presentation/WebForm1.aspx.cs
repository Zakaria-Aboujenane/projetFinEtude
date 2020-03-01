using CalendrierDesArchives.DAO;
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
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static FichierDAOSQLServer dao;
        protected void Page_Load(object sender, EventArgs e)
        {
            dao = FichierDAOSQLServer.getInstance();
            
          
            
        }
        [WebMethod]
        public static String getDate(String date)
        {
            List<Fichier> listF = dao.listerLesfichiersParDate(date);
            String text = "";
            foreach (var item in listF)
            {
                text += item.nomFichier;
            }
            return "files in this date :"+text;
        }
        
    }
}