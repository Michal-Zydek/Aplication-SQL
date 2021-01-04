using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Aplikacja_SQL.Controllers
{
    public class ViewBazaController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        static public int ID_USER;


        //View after Login & Main view user
        public ActionResult ViewB(int ID_User,int Detalis)
        {
            var User = db.UZYTKOWNIK.Find(ID_User);

            ID_USER = User.ID_UZYTKOWNIKA;
            ViewBag.Imie = User.IMIE;
            ViewBag.Details = Detalis;



            var bAZA_DANYCH = db.BAZA_DANYCH.Where(m=>m.ID_UZYTKOWNIKA==ID_USER);
            return View(bAZA_DANYCH.ToList());

        }

        public ActionResult Addbaza()
        {


            return RedirectToAction("AddBazaHandle", "AddBaza", new { ID_User = ID_USER });
        }

        public ActionResult EditBaza(int ID_BAZA)
        {


            return RedirectToAction("EditB", "EditBaza", new {ID_Baza= ID_BAZA, ID_User=ID_USER });
        }

       

    }
}