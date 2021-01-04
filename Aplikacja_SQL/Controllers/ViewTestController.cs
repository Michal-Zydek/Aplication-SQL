using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Aplikacja_SQL.Controllers
{
    public class ViewTestController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        static public int ID_USER;

     

        //View after Login & Main view user
        public ActionResult ViewT(int ID_User, int Detalis)
        {
            var User = db.UZYTKOWNIK.Find(ID_User);

            ID_USER = User.ID_UZYTKOWNIKA;
            ViewBag.Imie = User.IMIE;

            var tEST = db.TEST.Where(m=>m.ID_UZYTKOWNIKA==ID_USER);

            ViewBag.Details = Detalis;
            return View(tEST.ToList());
            
        }
        public ActionResult AddTest()
        {
            return RedirectToAction("AddT", "AddTest", new { ID_User = ID_USER });
        }

        public ActionResult EditTest(int ID_TEST)
        {


            return RedirectToAction("EditT", "EditTest", new { ID_TEST = ID_TEST, ID_User = ID_USER });
        }

        public ActionResult ViewO(int ID_TEST)
        {


            return RedirectToAction("ViewO", "ViewOdpowiedz", new { ID_TEST = ID_TEST, ID_User = ID_USER });
        }







    }
}