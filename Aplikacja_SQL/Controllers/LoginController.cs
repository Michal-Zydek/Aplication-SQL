using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplikacja_SQL.Controllers
{
    public class LoginController: MD5Controller
    {

        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        


       //Login User
        public ActionResult Logowanie()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logowanie([Bind(Include = "LOGIN,HASLO")] UZYTKOWNIK uZYTKOWNIK)
        {
            
           uZYTKOWNIK.HASLO=ToMD5Hash(uZYTKOWNIK.HASLO);
            var User = db.UZYTKOWNIK.Where(m => m.LOGIN == uZYTKOWNIK.LOGIN && m.HASLO == uZYTKOWNIK.HASLO).FirstOrDefault();

            

                if (User != null)
                {
                    ViewBag.Details = 0;
                return RedirectToAction("UserMain", "User",new {ID_User=User.ID_UZYTKOWNIKA, DETALIS=0 });
            }
                else
                {

                    ViewBag.Details = 1;
                    return View();

                }

        }


    }
}