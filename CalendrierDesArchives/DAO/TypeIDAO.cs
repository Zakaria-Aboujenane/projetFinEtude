using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalendrierDesArchives.Model;
using Type = CalendrierDesArchives.Model.Type;

namespace CalendrierDesArchives.DAO
{
    interface TypeIDAO
    {
        void ajouterType(String nomType,int Duree);
        void supprimerType(int idType);
        void modifierType(int idType, String nvNom, int nvDuree);
        List<Type> listerTypes();
        Type getTypeById(int idType);
        Type getTypeByName(String name);



    }
}