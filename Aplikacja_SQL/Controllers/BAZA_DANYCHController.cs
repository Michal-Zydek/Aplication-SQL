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
    public class BAZA_DANYCHController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();

        // GET: BAZA_DANYCH
        public ActionResult Index()
        {
            var bAZA_DANYCH = db.BAZA_DANYCH.Include(b => b.UZYTKOWNIK);
            return View(bAZA_DANYCH.ToList());
        }

        // GET: BAZA_DANYCH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAZA_DANYCH bAZA_DANYCH = db.BAZA_DANYCH.Find(id);
            if (bAZA_DANYCH == null)
            {
                return HttpNotFound();
            }
            return View(bAZA_DANYCH);
        }

        // GET: BAZA_DANYCH/Create
        public ActionResult Create()
        {
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE");
            return View();
        }

        // POST: BAZA_DANYCH/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BAZA,TYP,SCIEZKA,HASLO,LOGIN,NAZWA,ID_UZYTKOWNIKA")] BAZA_DANYCH bAZA_DANYCH)
        {
            if (ModelState.IsValid)
            {
                db.BAZA_DANYCH.Add(bAZA_DANYCH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", bAZA_DANYCH.ID_UZYTKOWNIKA);
            return View(bAZA_DANYCH);
        }

        // GET: BAZA_DANYCH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAZA_DANYCH bAZA_DANYCH = db.BAZA_DANYCH.Find(id);
            if (bAZA_DANYCH == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", bAZA_DANYCH.ID_UZYTKOWNIKA);
            return View(bAZA_DANYCH);
        }

        // POST: BAZA_DANYCH/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BAZA,TYP,SCIEZKA,HASLO,LOGIN,NAZWA,ID_UZYTKOWNIKA")] BAZA_DANYCH bAZA_DANYCH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bAZA_DANYCH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_UZYTKOWNIKA = new SelectList(db.UZYTKOWNIK, "ID_UZYTKOWNIKA", "IMIE", bAZA_DANYCH.ID_UZYTKOWNIKA);
            return View(bAZA_DANYCH);
        }

        // GET: BAZA_DANYCH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAZA_DANYCH bAZA_DANYCH = db.BAZA_DANYCH.Find(id);
            if (bAZA_DANYCH == null)
            {
                return HttpNotFound();
            }
            return View(bAZA_DANYCH);
        }

        // POST: BAZA_DANYCH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BAZA_DANYCH bAZA_DANYCH = db.BAZA_DANYCH.Find(id);
            db.BAZA_DANYCH.Remove(bAZA_DANYCH);
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
