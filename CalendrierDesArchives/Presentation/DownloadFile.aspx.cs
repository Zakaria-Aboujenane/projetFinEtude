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
    public partial class DownloadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String idFile = Request.QueryString["idFile"];
            Fichier f = new ActionsFichier().getFichierById(Int32.Parse(idFile));
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=archive-"+f.idFichier+f.extention);
            Response.TransmitFile(Server.MapPath("~/Archives/archive-" + f.idFichier + f.extention));
            Response.End();
            //Response.End();
            //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
    }
}