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
        public String action { get; set; }
        public String DUAselon { get; set; }
        public String Description { get; set; }
        public Type(int idType, String nomType, int duree,String action, String DUAselon, String Description)
        {
            this.idType = idType;
            this.nomType = nomType;
            this.duree = duree;
            this.action = action;
            this.DUAselon = DUAselon;
            this.Description = Description;
        }
        public Type()
        {

        }
    }
}