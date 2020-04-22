using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalendrierDesArchives.DAO;


namespace CalendrierDesArchives.Metiers
{
    public class ActionsHistorique
    {
        private HistoriqueDAOSQLServer historiqueDAOSQLServer;

        public ActionsHistorique()
        {
            historiqueDAOSQLServer = HistoriqueDAOSQLServer.getInstance();
        }

        public void ajouterHistorique(Model.Historique h)
        {
            historiqueDAOSQLServer = HistoriqueDAOSQLServer.getInstance();
            historiqueDAOSQLServer.ajouterHistorique(h);
        }

        public List<Model.Historique> listerTousHistoriques()
        {
            return historiqueDAOSQLServer.listerTousHistoriques();
        }
        //hahi supp
        public void supprimerHistorique(Model.Historique h)
        {
            historiqueDAOSQLServer = HistoriqueDAOSQLServer.getInstance();
            historiqueDAOSQLServer.supprimerHistorique(h);
        }
    }
}