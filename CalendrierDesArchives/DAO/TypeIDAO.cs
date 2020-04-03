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
        int ajouterType(String nomType, int Duree, string action);
        void supprimerType(Model.Type type);
        void modifierType(Model.Type type);
        List<Type> listerTypes();
        Type getTypeById(int idType);
        Type getTypeByName(String name);
        List<Type> rechercheTypeParNom(String type);

    }
}