using Aplikacja_SQL.Models;
using LinqToDB;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


namespace Aplikacja_SQL.Controllers
{
    public class AddBazaController : Controller
    {

        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        static public int ID_USER;

        public ActionResult AddBazaHandle(int ID_User)
        {
            ID_USER = ID_User;
            return RedirectToAction("AddB");
        }


        //Create DataBase for User
        [HttpGet]
        public ActionResult AddB()
        {
           
            
            return View();
        }

        //HTTPPOST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddB([Bind(Include = "ID_BAZA,TYP,SCIEZKA,HASLO,LOGIN,NAZWA,ID_UZYTKOWNIKA")] BAZA_DANYCH bAZA_DANYCH)
        {
            bAZA_DANYCH.ID_UZYTKOWNIKA = ID_USER;
            if (ModelState.IsValid)
            {
                string connetionString;

                // Create check conneting with postresql
                if (bAZA_DANYCH.TYP== "POSTGRESQL")
                {
                connetionString = "Host="+bAZA_DANYCH.SCIEZKA+";Username="+bAZA_DANYCH.LOGIN+";Password="+bAZA_DANYCH.HASLO+";Database="+bAZA_DANYCH.NAZWA+";";
                    NpgsqlConnection cnn = new NpgsqlConnection(connetionString);
                    
                try
                {
                    cnn.Open();
                    cnn.Close();
                        db.BAZA_DANYCH.Add(bAZA_DANYCH);
                        db.SaveChanges();
                        ViewBag.Details = 1;
                        return RedirectToAction("ViewB", "ViewBaza", new { ID_User = ID_USER , Detalis = 2});
                    }
                catch (Exception)
                {
                        ViewBag.Details= 2;
                        return View();
                }
                }



                // Create check conneting with oracle
                if (bAZA_DANYCH.TYP == "ORACLE")
                {
                    connetionString = "Data Source="+bAZA_DANYCH.SCIEZKA+";User Id="+bAZA_DANYCH.LOGIN+";Password="+bAZA_DANYCH.HASLO+";DBA Privilege=SYSDBA;";
                    OracleConnection cnn = new OracleConnection(connetionString);




                    try
                    {
                        
                        cnn.Open();
                        cnn.Dispose();
                        ViewBag.Details = 1;
                        db.BAZA_DANYCH.Add(bAZA_DANYCH);
                        db.SaveChanges();
                        ViewBag.Details = 1;

                        return RedirectToAction("ViewB", "ViewBaza", new { ID_User = ID_USER, Detalis = 2 });
                    }
                    catch (Exception )
                    {
                        
                       ViewBag.Details = 2;
                       return View();
                    }


                }
            }

            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", bAZA_DANYCH.ID_UZYTKOWNIKA);
            return View(bAZA_DANYCH);
        }
    }
}