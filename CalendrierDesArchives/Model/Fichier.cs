using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.Model
{
    public class Fichier
    {
        public int idFichier { get; set; }
        public String nomFichier { get; set; }
        public String type { get; set; }
        //structure des dates : 2020-12-12 00:00:00 (en base de donnees SQL SERVER  )
        public DateTime dateAjout { get; set; }
        public DateTime dateModification { get; set; }
        public DateTime dateDernierAcces { get; set; }
        public DateTime dateSuppression { get; set; }
        public String chemain { get; set; }
        public String extention { get; set; }
        public int idParent { get; set; }
        //Constructeur pour la base de donnees :
        public Fichier(int IdFichier, string Nom, string Type, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP)
        {
            this.idFichier = IdFichier;
            this.nomFichier = Nom;
            this.type = Type;
            this.dateAjout = DateAjout;
            this.dateModification = DateModification;
            this.dateDernierAcces = DateDernierAcces;
            this.dateSuppression = DateSuppression;
            this.chemain = Chemain;
            this.extention = extention;
            this.idParent = idP;
        }

        public Fichier(int idFichier, int idParent, string nomFichier, string type, string chemain, DateTime dateAjout)
        {
            this.idFichier = idFichier;
            this.idParent = idParent;
            this.nomFichier = nomFichier;
            this.type = type;
            this.chemain = chemain;
            this.dateAjout = dateAjout;
        }

        public String afficherMoi()
        {
            return "i am " + this.nomFichier;
        }
    }
}