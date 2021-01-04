using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplikacja_SQL.Controllers
{
    public class DeleteTestController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();

        [HttpPost]
        public ActionResult Delete(int ID_Test)
        {
            var tEST = db.TEST.Find(ID_Test);
            int ID_USER = tEST.ID_UZYTKOWNIKA.Value;
            List<PYTANIE> pYTANIA = db.PYTANIE.Where(m => m.ID_TEST == tEST.ID_TEST).ToList();
            List<ODPOWIEDZ> oDPOWIEDZs = db.ODPOWIEDZ.Where(m => m.ID_TEST == tEST.ID_TEST).ToList();


            foreach (var ODPOWIEDZ in oDPOWIEDZs)
            {
                db.ODPOWIEDZ.Remove(ODPOWIEDZ);
            }
            foreach (var PYTANIE in pYTANIA)
            {
                db.PYTANIE.Remove(PYTANIE);
            }
            db.TEST.Remove(tEST);
            db.SaveChanges();

            var redirectURL = new UrlHelper(Request.RequestContext).Action("ViewT","ViewTest", new { ID_User = ID_USER, DETALIS = 4 });
            return Json(new { Url= redirectURL });
        }





    }
}