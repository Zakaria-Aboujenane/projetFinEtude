using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.Model
{
    public class Utilisateur
    {
        public int idUtilisateur { get; set; }
        public String nom { get; set; }
        public String prenom { get; set; }
        public String email { get; set; }
        public String motDePasse { get; set; }
        public String privillege { get; set; }

        public Utilisateur(int IdUtilisateur,string Nom, string Prenom, string Email, string MotDePasse,string Privillege)
        {
            this.idUtilisateur = idUtilisateur;
            this.nom = Nom;
            this.prenom = Prenom;
            this.email = Email;
            this.motDePasse = MotDePasse;
            this.privillege = Privillege;

        }
        public Utilisateur()
        {

        }
        public override string ToString()
        {
            return "";
        }
    }
}