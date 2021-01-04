using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aplikacja_SQL.Models;

namespace Aplikacja_SQL.Controllers
{
    public class ODPOWIEDZController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();

        // GET: ODPOWIEDZ
        public ActionResult Index()
        {
            var oDPOWIEDZ = db.ODPOWIEDZ.Include(o => o.PYTANIE).Include(o => o.TEST).Include(o => o.UZYTKOWNIK);
            return View(oDPOWIEDZ.ToList());
        }

        // GET: ODPOWIEDZ/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ODPOWIEDZ oDPOWIEDZ = db.ODPOWIEDZ.Find(id);
            if (oDPOWIEDZ == null)
            {
                return HttpNotFound();
            }
            return View(oDPOWIEDZ);
        }

        // GET: ODPOWIEDZ/Create
        public ActionResult Create()
        {
            ViewBag.ID_PYTANIE = new SelectList(db.PYTANIE, "ID_PYTANIE", "PYTANIE1");
            ViewBag.ID_TEST = new SelectList(db.TEST, "ID_TEST", "NAZWA");
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE");
            return View();
        }

        // POST: ODPOWIEDZ/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ODPOWIEDZI,ODPOWIEDZ1,ID_PYTANIE,ID_TEST,ID_UZYTKOWNIKA")] ODPOWIEDZ oDPOWIEDZ)
        {
            if (ModelState.IsValid)
            {
                db.ODPOWIEDZ.Add(oDPOWIEDZ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PYTANIE = new SelectList(db.PYTANIE, "ID_PYTANIE", "PYTANIE1", oDPOWIEDZ.ID_PYTANIE);
            ViewBag.ID_TEST = new SelectList(db.TEST, "ID_TEST", "NAZWA", oDPOWIEDZ.ID_TEST);
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", oDPOWIEDZ.ID_UZYTKOWNIKA);
            return View(oDPOWIEDZ);
        }

        // GET: ODPOWIEDZ/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ODPOWIEDZ oDPOWIEDZ = db.ODPOWIEDZ.Find(id);
            if (oDPOWIEDZ == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PYTANIE = new SelectList(db.PYTANIE, "ID_PYTANIE", "PYTANIE1", oDPOWIEDZ.ID_PYTANIE);
            ViewBag.ID_TEST = new SelectList(db.TEST, "ID_TEST", "NAZWA", oDPOWIEDZ.ID_TEST);
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", oDPOWIEDZ.ID_UZYTKOWNIKA);
            return View(oDPOWIEDZ);
        }

        // POST: ODPOWIEDZ/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ODPOWIEDZI,ODPOWIEDZ1,ID_PYTANIE,ID_TEST,ID_UZYTKOWNIKA")] ODPOWIEDZ oDPOWIEDZ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oDPOWIEDZ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PYTANIE = new SelectList(db.PYTANIE, "ID_PYTANIE", "PYTANIE1", oDPOWIEDZ.ID_PYTANIE);
            ViewBag.ID_TEST = new SelectList(db.TEST, "ID_TEST", "NAZWA", oDPOWIEDZ.ID_TEST);
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", oDPOWIEDZ.ID_UZYTKOWNIKA);
            return View(oDPOWIEDZ);
        }

        // GET: ODPOWIEDZ/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ODPOWIEDZ oDPOWIEDZ = db.ODPOWIEDZ.Find(id);
            if (oDPOWIEDZ == null)
            {
                return HttpNotFound();
            }
            return View(oDPOWIEDZ);
        }

        // POST: ODPOWIEDZ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ODPOWIEDZ oDPOWIEDZ = db.ODPOWIEDZ.Find(id);
            db.ODPOWIEDZ.Remove(oDPOWIEDZ);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
