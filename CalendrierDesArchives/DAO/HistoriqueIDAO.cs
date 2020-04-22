using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendrierDesArchives.Model;
namespace CalendrierDesArchives.DAO
{
    interface HistoriqueIDAO
    {
        void ajouterHistorique(Historique h);
        List<Historique> listerTousHistoriques();
        void supprimerHistorique(Historique h);
    }
}
