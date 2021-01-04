using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Aplikacja_SQL.Controllers
{
    public class EditUserController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        static public int ID_USER;


        [HttpGet]
        public ActionResult Edit(int EditUserID)
        {         
            UZYTKOWNIK uZYTKOWNIK = db.UZYTKOWNIK.Find(EditUserID);
            if (uZYTKOWNIK == null)
            {
                return HttpNotFound();
            }
            ID_USER = EditUserID;
            return View(uZYTKOWNIK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_UZYTKOWNIKA,IMIE,NAZWISKO,NR_INDEKSU,EMAIL,TYP_KONTA,LOGIN,HASLO")] UZYTKOWNIK uZYTKOWNIK)
        {

            if (ModelState.IsValid)
            {

                db.Entry(uZYTKOWNIK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserMain","User", new { ID_User = ID_USER, Detalis=1 });
            }
            return View(uZYTKOWNIK);
        }


    }
}