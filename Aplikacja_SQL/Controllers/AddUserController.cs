using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Aplikacja_SQL.Controllers
{
    public class AddUserController : MD5Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();

        //Registration User
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID_UZYTKOWNIKA,IMIE,NAZWISKO,NR_INDEKSU,EMAIL,TYP_KONTA,LOGIN,HASLO")] UZYTKOWNIK uZYTKOWNIK)
        {

            uZYTKOWNIK.TYP_KONTA = "U";
            uZYTKOWNIK.HASLO = ToMD5Hash(uZYTKOWNIK.HASLO);

            

            if (ModelState.IsValid)
            {

                if (db.UZYTKOWNIK.Where(m => m.LOGIN == uZYTKOWNIK.LOGIN).FirstOrDefault() == null)
                {
                    db.UZYTKOWNIK.Add(uZYTKOWNIK);
                    db.SaveChanges();
                ViewBag.Details = 1;
                    ModelState.Clear();
                return View();
                }
                else
                {
                    ViewBag.Details = 0;
                    ModelState.AddModelError("LOGIN", "Login jest niedostępny");
                    return View(uZYTKOWNIK);
                }

            }
            ViewBag.Details = 0;
            return View(uZYTKOWNIK);
        }




      
    }
}