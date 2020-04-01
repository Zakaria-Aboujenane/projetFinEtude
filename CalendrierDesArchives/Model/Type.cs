using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.Model
{
    public class Type
    {

        public int idType { get; set; }
        public String nomType { get; set; }
        public int duree { get; set; }
        public string action { get; set; }
        public Type(int idType, string nomType, int duree,string action)
        {
            this.idType = idType;
            this.nomType = nomType;
            this.duree = duree;
            this.action = action;
        }
        public Type()
        {
        }
    }
}