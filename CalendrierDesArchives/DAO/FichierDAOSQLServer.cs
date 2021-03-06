﻿using CalendrierDesArchives.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using Hangfire;

namespace CalendrierDesArchives.DAO
{
    public class FichierDAOSQLServer : FichierIDAO
    {
        //singleton:
        private static FichierDAOSQLServer instance = null;
        public static FichierDAOSQLServer getInstance()
        {
            if (instance == null)
                instance = new FichierDAOSQLServer();
            return instance;
        }

        private FichierDAOSQLServer()
        {

        }
        public int ajouterFichier(Fichier f)
        {
            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
               
                String query = $"INSERT INTO Fichier(Nom,DateAjout, DateModification, DateDernierAcces,  DateSuppression,  Chemain," +
                    $"extention,[index],emplacementPC,sortFinalComm,commArch,idType,Description,HangFireID,HangFireNotificationID," +
                    $"HangFireRecJobNotID) " +
                   $"values ('{f.Nom}','{f.dateAjout}', '{f.dateModification}', '{f.dateDernierAcces}', '{f.dateSuppression}','{f.chemain}'," +
                   $" '{f.extention}','{f.index}','{f.emplacementPC}','{f.sortFinalComm}',{f.commArch},'{f.type.idType}','{f.Description}'," +
                   $"'{f.HangFireID}','{f.HangFireNotificationID}','{f.HangFireRecJobNotID}' );"+
                   "SELECT CAST(SCOPE_IDENTITY() as int)";
               //recuperation de l'archive ajoute:
                 int id = connection.Query<int>(query).Single();
                return id;
            }

        }
        public void supprimerFichier(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"DELETE FROM Fichier WHERE IdFichier='{id}';");
            }
        }
        public void modifierFichier(Fichier f)
        {
            
                if (f.type.idType ==0)
                    f.type.idType = f.idType;
           
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                
                connection.Execute($"UPDATE Fichier SET Nom='{f.Nom}',idType={f.type.idType},DateModification='{f.dateModification}'," +
                    $"DateDernierAcces='{f.dateDernierAcces}',DateSuppression='{f.dateSuppression}',[index]='{f.index}'," +
                    $"emplacementPC='{f.emplacementPC}',sortFinalComm='{f.sortFinalComm}',commArch='{f.commArch}',Chemain='{f.chemain}',Description='{f.Description}'," +
                    $"HangFireID='{f.HangFireID}',HangFireNotificationID='{f.HangFireNotificationID}',HangFireRecJobNotID='{f.HangFireRecJobNotID}'" +
                    $"  WHERE IdFichier = {f.idFichier}");
            }
        }

        public List<Fichier> listerLesfichiersParDate(String date)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {

                return connection.Query<Fichier>($"Select * From Fichier Where datediff(day, DateAjout, '{date}')=0 OR datediff(day, DateModification,'{date}')=0 OR datediff(day, DateSuppression, '{date}')=0 ORDER BY IdFichier DESC").ToList();
            }

        }
        public List<Fichier> listerTousLesfichiers()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Fichier>($"Select * From Fichier ORDER BY IdFichier DESC")
                    .ToList();
            }
        }
        public List<Fichier> rechercheFichierParNom(string Nom)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                String nomGenere = "%" + Nom + "%";
                return connection.Query<Fichier>($"Select Nom * From Fichier Where Nom like {nomGenere};")
                    .ToList();
            }
        }

        public List<Fichier> rechercheFichierParType(int idType)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Fichier>($"Select Nom * From Fichier Where idType='{idType}';")
                    .ToList();
            }
        }

        public Fichier getFichierById(int idF)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                try
                {
                    return connection.QuerySingle<Fichier>($"Select * From Fichier Where idFichier='{idF}';");
                }
                catch (Exception e)
                {
                    return null;
                }     
            }
        }

        public List<Fichier> listerParUser(Utilisateur user)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Fichier>($"Select f.* " +
                    $"From Fichier f,GestionFichier gf,Utilisateur u" +
                    $" Where f.IdFichier=gf.IdFichier AND u.IdUtilisateur=gf.IdUtilisateur AND u.IdUtilisateur='{user.idUtilisateur}';")
                    .ToList();
            }
        }

        public int AjouterParUser(Utilisateur u, Fichier f)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                
                String query = $"INSERT INTO Fichier(Nom,DateAjout, DateModification, DateDernierAcces,  DateSuppression,  Chemain,  extention,idType,Description) " +
                   $"values ('{f.Nom}','{f.dateAjout.ToString()}', '{f.dateModification.ToString()}', '{f.dateDernierAcces.ToString()}', '{f.dateSuppression.ToString()}','{f.chemain}', '{f.extention}','{f.type.idType}','{f.Description}' );" +
                   "SELECT CAST(SCOPE_IDENTITY() as int)";
                //recuperation de l'archive ajoute:
                int id = connection.Query<int>(query).Single();
                ajouterUserFichier(u, id);
                return id;
            }
        }
        public void ajouterUserFichier(Utilisateur u,int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                connection.Execute($"INSERT INTO GestionFichier(IdFichier,IdUtilisateur) " +
                    $"values('{id}','{u.idUtilisateur}');");
            }
        }

   

        public bool appartenanceUF(Utilisateur u, Fichier f)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                try
                {
                    int app = connection.QuerySingle<Int32>($"Select f.IdFichier " +
                    $"From Fichier f,GestionFichier gf,Utilisateur u" +
                    $" Where f.IdFichier=gf.IdFichier AND u.IdUtilisateur=gf.IdUtilisateur AND gf.IdUtilisateur='{u.idUtilisateur}' AND gf.idFichier='{f.idFichier}';");
                    if (app > 0)
                        return true;
                    else return false;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
               
            }
        }

        public List<Fichier> listerFichiersArchive()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                return connection.Query<Fichier>($"Select * From Fichier Where sortFinalComm='1' ORDER BY IdFichier DESC")
                    .ToList();
            }
        }

        public List<Fichier> rechercheGenerale(string searsh)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                String str = "%" + searsh + "%";
                searsh = str;
                return connection.Query<Fichier>($"Select f.* From Fichier f,Type t " +
                    $"Where f.idType = t.idType AND (f.Nom LIKE '{searsh}' OR f.[index] LIKE '{searsh}' OR t.nomType LIKE '{searsh}') " +
                    $"ORDER BY IdFichier DESC")
                    .ToList();
            }
        }

        public List<Fichier> rechercheGSelonUser(string searsh, Utilisateur u)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.conVal("CalendrierDatabase")))
            {
                String str = "%" + searsh + "%";
                searsh = str;
                return connection.Query<Fichier>($"Select f.* From Fichier f,Type t,GestionFichier g,Utilisateur u " +
                    $"Where " +
                    $"f.idType = t.idType AND g.IdFichier = f.IdFichier AND g.IdUtilisateur = u.IdUtilisateur AND u.idUtilisateur = '{u.idUtilisateur}' " +
                    $"AND(f.Nom LIKE '{searsh}' OR f.[index] LIKE '{searsh}' OR t.nomType LIKE '{searsh}') " +
                    $"ORDER BY IdFichier DESC")
                    .ToList();
            }
        }
    }
}