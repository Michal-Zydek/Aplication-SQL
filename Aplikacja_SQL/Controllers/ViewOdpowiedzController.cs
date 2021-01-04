using Aplikacja_SQL.Models.MyModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aplikacja_SQL.Models;

namespace Aplikacja_SQL.Controllers
{
    public class ViewOdpowiedzController : Controller
    {
        // GET: ViewOdpowiedz
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        static public int ID_USER;


        
        public ActionResult ViewO(int ID_Test, int ID_User)
        {
            var User = db.UZYTKOWNIK.Find(ID_User);

            ID_USER = User.ID_UZYTKOWNIKA;
            ViewBag.Imie = User.IMIE;

         var oDPOWIEDZ = db.ODPOWIEDZ.Where(m => m.ID_TEST == ID_Test).GroupBy(m=> m.ID_UZYTKOWNIKA).Select(g=>g.FirstOrDefault()).ToList();

            ViewBag.list = oDPOWIEDZ;
            return View(oDPOWIEDZ);
        }


        // GET: ViewPytanie
        public ActionResult ViewO_User(int ID_Test, int ID_User)
        {
            var User = db.UZYTKOWNIK.Find(ID_User);
            ID_USER = User.ID_UZYTKOWNIKA;
            ViewBag.Imie = User.IMIE;
            ViewBag.Nazwisko = User.NAZWISKO;
            ViewBag.Index = User.NR_INDEKSU;



            List<PYTANIE> pytanie = db.PYTANIE.Where(m => m.ID_TEST == ID_Test).ToList();
            List<ODPOWIEDZ> odpowiedz = db.ODPOWIEDZ.Where(n => n.ID_UZYTKOWNIKA == ID_User && n.ID_TEST == ID_Test).ToList();


            Pytanie_Odpowiedz model = new Pytanie_Odpowiedz
            {

                pYTANIE =pytanie ,
                oDPOWIEDZ = odpowiedz
        };

            ViewBag.Rows = model.pYTANIE.Count();
           
            
            return View(model);
        }





    }
}