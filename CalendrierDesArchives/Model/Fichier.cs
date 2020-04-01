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
        //structure des dates : 2020-12-12 00:00:00 (en base de donnees SQL SERVER  )
        public DateTime dateAjout { get; set; }
        public DateTime dateModification { get; set; }
        public DateTime dateDernierAcces { get; set; }
        public DateTime dateSuppression { get; set; }
        public String chemain { get; set; }
        public String extention { get; set; }
        public int idParent { get; set; }
        public Type type { get; set; }
        //Constructeur pour la base de donnees :
        public Fichier(int IdFichier, string Nom, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention, int idP,int idType)
        {
            type = new Type();
            this.idFichier = IdFichier;
            this.nomFichier = Nom;
            this.dateAjout = DateAjout;
            this.dateModification = DateModification;
            this.dateDernierAcces = DateDernierAcces;
            this.dateSuppression = DateSuppression;
            this.chemain = Chemain;
            this.extention = extention;
            this.idParent = idP;
            this.type.idType = idType;
        }

        public Fichier(int idFichier, int idParent, string nomFichier,string chemain, DateTime dateAjout)
        {
            this.idFichier = idFichier;
            this.idParent = idParent;
            this.nomFichier = nomFichier;
            this.chemain = chemain;
            this.dateAjout = dateAjout;
        }
    }
}