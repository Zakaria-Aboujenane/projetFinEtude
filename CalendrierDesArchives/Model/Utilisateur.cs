using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.Model
{
    public class Utilisateur
    {
        public String nom { get; set; }
        public String prenom { get; set; }
        public String email { get; set; }
        public String motDePasse { get; set; }
        public String privillege { get; set; }

        public Utilisateur(string nom, string prenom, string email, string motDePasse)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            this.motDePasse = motDePasse;

        }

        public override string ToString()
        {
            return "";
        }
    }
}