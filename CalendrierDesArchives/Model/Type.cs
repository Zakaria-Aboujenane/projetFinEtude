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
        public Type(int idType, string nomType, int duree)
        {
            this.idType = idType;
            this.nomType = nomType;
            duree = duree;
        }
        public Type()
        {
        }
    }
}