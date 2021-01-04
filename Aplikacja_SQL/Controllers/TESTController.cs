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
    public class TESTController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();

        // GET: TEST
        public ActionResult Index()
        {
            var tEST = db.TEST.Include(t => t.BAZA_DANYCH).Include(t => t.UZYTKOWNIK);
            return View(tEST.ToList());
        }

        // GET: TEST/Details/ssss
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEST tEST = db.TEST.Find(id);
            if (tEST == null)
            {
                return HttpNotFound();
            }
            return View(tEST);
        }

        // GET: TEST/Create
        public ActionResult Create()
        {
            ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH, "ID_BAZA", "TYP");
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE");
            return View();
        }

        // POST: TEST/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TEST,NAZWA,CZAS_START,CZAS_STOP,KLUCZ,ID_BAZA,ID_UZYTKOWNIKA")] TEST tEST)
        {
            if (ModelState.IsValid)
            {
                db.TEST.Add(tEST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH, "ID_BAZA", "TYP", tEST.ID_BAZA);
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", tEST.ID_UZYTKOWNIKA);
            return View(tEST);
        }

        // GET: TEST/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEST tEST = db.TEST.Find(id);
            if (tEST == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH, "ID_BAZA", "TYP", tEST.ID_BAZA);
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", tEST.ID_UZYTKOWNIKA);
            return View(tEST);
        }

        // POST: TEST/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TEST,NAZWA,CZAS_START,CZAS_STOP,KLUCZ,ID_BAZA,ID_UZYTKOWNIKA")] TEST tEST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tEST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_BAZA = new SelectList(db.BAZA_DANYCH, "ID_BAZA", "TYP", tEST.ID_BAZA);
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", tEST.ID_UZYTKOWNIKA);
            return View(tEST);
        }

        // GET: TEST/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEST tEST = db.TEST.Find(id);
            if (tEST == null)
            {
                return HttpNotFound();
            }
            return View(tEST);
        }

        // POST: TEST/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TEST tEST = db.TEST.Find(id);
            db.TEST.Remove(tEST);
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
