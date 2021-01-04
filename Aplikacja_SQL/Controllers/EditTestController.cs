using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplikacja_SQL.Controllers
{
    public class EditTestController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        static public int ID_USER;
        static public int NRTEST;
        [HttpGet]
        public ActionResult EditT(int ID_Test)
        {
            
            NRTEST = ID_Test;


            TEST tEST = db.TEST.Find(ID_Test);
            ID_USER = int.Parse(""+tEST.ID_UZYTKOWNIKA);
            ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH.Where(m => m.ID_UZYTKOWNIKA == ID_USER), "ID_BAZA", "NAZWA");

            List<PYTANIE> pytanie = db.PYTANIE.Where(m => m.ID_TEST == ID_Test).ToList();
            ViewBag.Pytania = pytanie;
            return View(tEST);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditT([Bind(Include = "ID_TEST,NAZWA,CZAS_START,CZAS_STOP,KLUCZ,ID_BAZA,ID_UZYTKOWNIKA")] TEST tEST)
        {
            List<PYTANIE> pytanie;
            if (ModelState.IsValid)
            {
                DateTime DaneDataStart = Convert.ToDateTime(tEST.CZAS_START);
                DateTime DaneDataStop = Convert.ToDateTime(tEST.CZAS_STOP);
                int DaneCheck;
                 DaneCheck = DaneDataStart.CompareTo(DaneDataStop);
                        if(DaneCheck == 1)
                        {

                            ModelState.AddModelError("CZAS_STOP", "Błędna data!!");
                    pytanie = db.PYTANIE.Where(m => m.ID_TEST == NRTEST).ToList();
                    ViewBag.Pytania = pytanie;
                    ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH.Where(m => m.ID_UZYTKOWNIKA == ID_USER), "ID_BAZA", "NAZWA");

                    return View(tEST);
                        }



                db.Entry(tEST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewT", "ViewTest", new { ID_User = ID_USER , Detalis = 1 });
            }

             pytanie = db.PYTANIE.Where(m => m.ID_TEST == NRTEST).ToList();
            ViewBag.Pytania = pytanie;
            ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH.Where(m => m.ID_UZYTKOWNIKA == ID_USER), "ID_BAZA", "NAZWA");
            return View(tEST);

        }
        

        
        public ActionResult EditP()
        {
            
            var data = from item in db.PYTANIE
                       where item.ID_TEST == NRTEST
                       orderby item.ID_TEST
                       select item;

            return View(data.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditP(List<PYTANIE> listP)
        {
            if (ModelState.IsValid)
            {
                

                    var elemDEL = db.PYTANIE.Where(m => m.ID_TEST == NRTEST).ToList();
                   foreach(var i in elemDEL)
                    {
                        db.PYTANIE.Remove(i);
                    }
                   db.SaveChanges();

                    
                    foreach (PYTANIE item in listP)
                    {

                        item.ID_TEST = NRTEST;
                        db.PYTANIE.Add(item);

                    }


                    db.SaveChanges();
                    return RedirectToAction("ViewT", "ViewTest", new { ID_User = ID_USER , Detalis = 1 });
                
            }
           
            ViewBag.Details = 1;
            return View(listP);

        }


        public ActionResult Check_Odp(int Id_Test)
        {
            
            int wynik;
            if (db.ODPOWIEDZ.Where(m=>m.ID_TEST==Id_Test).FirstOrDefault() != null)
            {
                wynik = 0;
                return Json(wynik);
            }
            else
            {
                var redirectURL = new UrlHelper(Request.RequestContext).Action("EditT", "EditTest", new { ID_Test = Id_Test });
                return Json(new { Url = redirectURL }); 
            }


        }

    }
}