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
    public class UZYTKOWNIKController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();

        // GET: UZYTKOWNIK
        public ActionResult Index()
        {
            return View(db.UZYTKOWNIK.ToList());
        }

        // GET: UZYTKOWNIK/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UZYTKOWNIK uZYTKOWNIK = db.UZYTKOWNIK.Find(id);
            if (uZYTKOWNIK == null)
            {
                return HttpNotFound();
            }
            return View(uZYTKOWNIK);
        }

        // GET: UZYTKOWNIK/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UZYTKOWNIK/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_UZYTKOWNIKA,IMIE,NAZWISKO,NR_INDEKSU,EMAIL,TYP_KONTA,LOGIN,HASLO")] UZYTKOWNIK uZYTKOWNIK)
        {
            if (ModelState.IsValid)
            {
                db.UZYTKOWNIK.Add(uZYTKOWNIK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uZYTKOWNIK);
        }

        // GET: UZYTKOWNIK/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UZYTKOWNIK uZYTKOWNIK = db.UZYTKOWNIK.Find(id);
            if (uZYTKOWNIK == null)
            {
                return HttpNotFound();
            }
            return View(uZYTKOWNIK);
        }

        // POST: UZYTKOWNIK/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_UZYTKOWNIKA,IMIE,NAZWISKO,NR_INDEKSU,EMAIL,TYP_KONTA,LOGIN,HASLO")] UZYTKOWNIK uZYTKOWNIK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uZYTKOWNIK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uZYTKOWNIK);
        }

        // GET: UZYTKOWNIK/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UZYTKOWNIK uZYTKOWNIK = db.UZYTKOWNIK.Find(id);
            if (uZYTKOWNIK == null)
            {
                return HttpNotFound();
            }
            return View(uZYTKOWNIK);
        }

        // POST: UZYTKOWNIK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UZYTKOWNIK uZYTKOWNIK = db.UZYTKOWNIK.Find(id);
            db.UZYTKOWNIK.Remove(uZYTKOWNIK);
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
