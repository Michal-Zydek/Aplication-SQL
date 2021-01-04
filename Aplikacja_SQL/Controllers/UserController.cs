using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplikacja_SQL.Controllers
{

    
    public class UserController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        static public int ID_USER;


        //View after Login & Main view user
        public ActionResult UserMain(int ID_User,int DETALIS)
        {
            ID_USER = ID_User;
            return RedirectToAction("ViewT", "ViewTest", new { ID_User = ID_USER , Detalis = DETALIS });
        }


        public ActionResult User_ViewBaza()
        {
            return RedirectToAction("ViewB", "ViewBaza",new {ID_User = ID_USER , Detalis = 0});
        }

        public ActionResult User_ViewTest()
        {
            return RedirectToAction("ViewT", "ViewTest", new { ID_User = ID_USER, Detalis = 0});
        }

        public ActionResult User_SQL(string Klucz)
        {
        var tEST = db.TEST.Where(m => m.KLUCZ == Klucz).FirstOrDefault();
            if(Klucz !="" && tEST !=null )
            {
                DateTime DaneNOW = DateTime.Now;
                DateTime DaneDataStart = Convert.ToDateTime(tEST.CZAS_START);
                DateTime DaneDataStop = Convert.ToDateTime(tEST.CZAS_STOP);
                int DaneCheck;
                int DaneCheck2;

                if (tEST.CZAS_START != null && tEST.CZAS_STOP != null)
                {
                    DaneCheck = DaneDataStart.CompareTo(DaneNOW);
                    DaneCheck2 = DaneDataStop.CompareTo(DaneNOW);
                    if(DaneCheck== -1 && DaneCheck2==1)
                    {
                        if (db.ODPOWIEDZ.Where(m => m.ID_UZYTKOWNIKA==ID_USER).FirstOrDefault() == null)
                        {
                            return RedirectToAction("UserSQL_U", "UserSQL", new { ID_User = ID_USER, ID_Test = tEST.ID_TEST });
                        }
                        else
                        {
                            return RedirectToAction("ViewT", "ViewTest", new { ID_User = ID_USER, Detalis = 7 });
                        }

                    }

                   return RedirectToAction("ViewT", "ViewTest", new { ID_User = ID_USER, Detalis = 5 });
                }
                else
                {
                    return RedirectToAction("UserSQL_U", "UserSQL", new { ID_User = ID_USER, ID_Test = tEST.ID_TEST });
                }
            }
            return RedirectToAction("ViewT", "ViewTest", new { ID_User = ID_USER , Detalis = 3 });
        }

        public ActionResult User_EditUser()
        {
            return RedirectToAction("Edit", "EditUser", new { EditUserID = ID_USER });
        }


        public ActionResult Logout()
        {
            return RedirectToAction("Logowanie", "Login");
        }
    }
}