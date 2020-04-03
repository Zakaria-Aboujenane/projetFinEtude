using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.Model
{
    public class Fichier
    {
        
        public int idFichier { get; set; }
        public string Nom { get; set; }
        //structure des dates : 2020-12-12 00:00:00 (en base de donnees SQL SERVER  )
        public DateTime dateAjout { get; set; }
        public DateTime dateModification { get; set; }
        public DateTime dateDernierAcces { get; set; }
        public DateTime dateSuppression { get; set; }
        public string chemain { get; set; }
        public string extention { get; set; }
        public int sortFinalComm { get; set; }
        public int commArch { get; set; }
        public int idParent { get; set; }
        public Type type { get; set; }
        public int idType { get; set; }
        public string Description { get; set; }
        //Constructeur pour la base de donnees :
        public Fichier()
        {

        }
        public Fichier(int IdFichier, string Nom, DateTime DateAjout, DateTime DateModification, DateTime DateDernierAcces, DateTime DateSuppression, string Chemain, string extention,int sortFinalComm,int commArch, int idP,int idType,string Description)
        {
            this.idFichier = IdFichier;
            this.Nom = Nom;
            this.dateAjout = DateAjout;
            this.dateModification = DateModification;
            this.dateDernierAcces = DateDernierAcces;
            this.dateSuppression = DateSuppression;
            this.chemain = Chemain;
            this.extention = extention;
            this.idParent = idP;
            this.idType = idType;
            this.Description = Description;
            this.commArch = commArch;
            this.sortFinalComm = sortFinalComm;
        }

   
    }
}