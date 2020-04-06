using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.Model
{
    public class Notification
    {
        public int idNotification { get; set; }
        public int idFichier { get; set; }
        public DateTime dateNotification { get; set; }
        public String textNotification { get; set; }
        public int Vu { get; set; }
        public Notification()
        {

        }
        public Notification(int idNotification,String textNotification , DateTime dateNotification,int Vu, int idFichier)
        {
            this.idNotification = idNotification;
            this.textNotification = textNotification;
            this.dateNotification = dateNotification;
            this.idFichier = idFichier;    
        }
    }
}