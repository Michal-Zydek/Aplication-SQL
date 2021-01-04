using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System.Data.Entity;

namespace Aplikacja_SQL.Controllers
{
    public class EditBazaController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        static public int ID_USER;

        [HttpGet]
        public ActionResult EditB(int ID_Baza, int ID_User)
        {
            ID_USER = ID_User;
            BAZA_DANYCH bAZA_DANYCH = db.BAZA_DANYCH.Find(ID_Baza);
            if (bAZA_DANYCH == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", bAZA_DANYCH.ID_UZYTKOWNIKA);
            return View(bAZA_DANYCH);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditB([Bind(Include = "ID_BAZA,TYP,SCIEZKA,HASLO,LOGIN,NAZWA,ID_UZYTKOWNIKA")] BAZA_DANYCH bAZA_DANYCH)
        {
            
            if (ModelState.IsValid)
            {
                string connetionString;

                // Create check conneting with postresql
                if (bAZA_DANYCH.TYP == "POSTGRESQL")
                {
                    connetionString = "Host=" + bAZA_DANYCH.SCIEZKA + ";Username=" + bAZA_DANYCH.LOGIN + ";Password=" + bAZA_DANYCH.HASLO + ";Database=" + bAZA_DANYCH.NAZWA + ";";
                    NpgsqlConnection cnn = new NpgsqlConnection(connetionString);

                    try
                    {
                        cnn.Open();
                        cnn.Close();
                        db.Entry(bAZA_DANYCH).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Details = 1;
                        return RedirectToAction("ViewB", "ViewBaza", new { ID_User = ID_USER, Detalis = 1 });
                    }
                    catch (Exception)
                    {
                        ViewBag.Details = 2;
                        return View();
                    }
                }



                // Create check conneting with oracle
                if (bAZA_DANYCH.TYP == "ORACLE")
                {
                    connetionString = "Data Source=" + bAZA_DANYCH.SCIEZKA + ";User Id=" + bAZA_DANYCH.LOGIN + ";Password=" + bAZA_DANYCH.HASLO + ";DBA Privilege=SYSDBA;";
                    OracleConnection cnn = new OracleConnection(connetionString);




                    try
                    {

                        cnn.Open();
                        cnn.Dispose();
                        ViewBag.Details = 1;
                        db.Entry(bAZA_DANYCH).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Details = 1;

                        return RedirectToAction("ViewB", "ViewBaza", new { ID_User = ID_USER, Detalis = 1 });
                    }
                    catch (Exception)
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