using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CalendrierDesArchives.DAO
{
    public static class ConnectionHelper
    {
        public static string conVal(String name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }



    }
}