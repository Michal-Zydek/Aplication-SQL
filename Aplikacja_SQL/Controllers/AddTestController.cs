using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplikacja_SQL.Controllers
{
    public class AddTestController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        
        private static int NRTest;
        private static int ID_USER;

        //Create Question
        [HttpGet]
        public ActionResult AddP()
        {
            // This is only for show by default one row for insert data to the database
            List<PYTANIE> listP = new List<PYTANIE> { new PYTANIE { ID_PYTANIE = 0, PYTANIE1 = "", ID_TEST = 0 } };
            return View(listP);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddP(List<PYTANIE> listP)
        {

            if (ModelState.IsValid)
            {
                
                    if(listP==null)
                    {
                        
                        return RedirectToAction("AddP");
                    }
                    

                    foreach (var i in listP)
                    {
                        
                        i.ID_TEST = NRTest;
                        db.PYTANIE.Add(i);
                    }
                    db.SaveChanges();
                    ModelState.Clear();

                    return RedirectToAction("ViewT", "ViewTest", new { ID_User = ID_USER , Detalis = 2 });
                
            }
            ViewBag.Details = 1;
            return View(listP);
        }




    //Create TEST
    [HttpGet]
        public ActionResult AddT(int ID_User)
        {
            ID_USER = ID_User;
            ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH.Where(m => m.ID_UZYTKOWNIKA == ID_USER), "ID_BAZA", "NAZWA");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddT([Bind(Include = "ID_TEST,NAZWA,CZAS_START,CZAS_STOP,KLUCZ,ID_BAZA,ID_UZYTKOWNIKA")] TEST tEST)
        {

            tEST.ID_UZYTKOWNIKA = ID_USER;
            DateTime DaneNOW = DateTime.Now;
            DateTime DaneDataStart = Convert.ToDateTime(tEST.CZAS_START);
            DateTime DaneDataStop = Convert.ToDateTime(tEST.CZAS_STOP);
            int DaneCheck;



            if (ModelState.IsValid)
            {

                if(tEST.CZAS_START != null)
                {
                    DaneCheck = DaneDataStart.CompareTo(DaneNOW);
                    if(DaneCheck== -1)
                    {
                        
                        
                        ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH.Where(m => m.ID_UZYTKOWNIKA == ID_USER), "ID_BAZA", "NAZWA");
                        ModelState.AddModelError("CZAS_START","Data mineła!!");
                        return View(tEST);
                    }

                    if (tEST.CZAS_STOP != null)
                    {
                        DaneCheck = DaneDataStart.CompareTo(DaneDataStop);
                        if(DaneCheck == 1)
                        {

                            ModelState.AddModelError("CZAS_STOP", "Błędna data!!");
                            ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH.Where(m => m.ID_UZYTKOWNIKA == ID_USER), "ID_BAZA", "NAZWA");
                            return View(tEST);
                        }
                    }
                }
                else
                {
                    tEST.CZAS_START = DaneNOW.ToString();
                }
                

           


                tEST.KLUCZ = KeyGenerate();
                db.TEST.Add(tEST);
                db.SaveChanges();
                NRTest = tEST.ID_TEST;
                return RedirectToAction("AddP");
            }
            
            ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH, "ID_BAZA", "NAZWA", tEST.ID_BAZA);
            return View(tEST);
        }




        //Method generate Key to Test
        public string KeyGenerate()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var key = new char[6];
            var random = new Random();

            for (int i = 0; i < key.Length; i++)
            {
                key[i] = chars[random.Next(chars.Length)];
            }

            string finalkey = new String(key);
            return finalkey;
        }

       

    }
}