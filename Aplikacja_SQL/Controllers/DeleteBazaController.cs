using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplikacja_SQL.Controllers
{
    public class DeleteBazaController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        [HttpPost]
        public ActionResult Delete(int ID_Baza)
        {
            var bAZA = db.BAZA_DANYCH.Find(ID_Baza);
            int ID_USER = bAZA.ID_UZYTKOWNIKA.Value;


            List<TEST> tEST = db.TEST.Where(m => m.ID_BAZA == ID_Baza).ToList();
            

            foreach (var TEST in tEST)
            {
            List<PYTANIE> pYTANIA = db.PYTANIE.Where(m => m.ID_TEST == TEST.ID_TEST).ToList();
                foreach(var PYTANIE in pYTANIA)
                {
                    List<ODPOWIEDZ> oDPOWIEDZ = db.ODPOWIEDZ.Where(m => m.ID_TEST == TEST.ID_TEST).ToList();
                    foreach(var ODPOWIEDZ in oDPOWIEDZ)
                    {
                        db.ODPOWIEDZ.Remove(ODPOWIEDZ);
                    }
                    db.PYTANIE.Remove(PYTANIE);
                }
               db.TEST.Remove(TEST);
            }
            db.BAZA_DANYCH.Remove(bAZA);
            db.SaveChanges();

            var redirectURL = new UrlHelper(Request.RequestContext).Action("ViewB", "ViewBaza", new { ID_User = ID_USER, DETALIS = 4 });
            return Json(new { Url = redirectURL });
        }

    }
}