using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.Model
{
    public class Historique
    {
        public int IdHistorique { get; set; }
        public String textHistorique { get; set; }
        public DateTime date { get; set; }


        public int IdFichier { get; set; }
        public Historique()
        {

        }

        public Historique(int IdHistorique, string textHistorique, DateTime date, int IdFichier)
        {
            this.IdHistorique = IdHistorique;
            this.textHistorique = textHistorique;
            this.date = date;
            this.IdFichier = IdFichier;
        }
    }
}