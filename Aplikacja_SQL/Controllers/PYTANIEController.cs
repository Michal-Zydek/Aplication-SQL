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
    public class PYTANIEController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();

        // GET: PYTANIE
        public ActionResult Index()
        {
            var pYTANIE = db.PYTANIE.Include(p => p.TEST);
            return View(pYTANIE.ToList());
        }

        // GET: PYTANIE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PYTANIE pYTANIE = db.PYTANIE.Find(id);
            if (pYTANIE == null)
            {
                return HttpNotFound();
            }
            return View(pYTANIE);
        }

        // GET: PYTANIE/Create
        public ActionResult Create()
        {
            ViewBag.ID_TEST = new SelectList(db.TEST, "ID_TEST", "NAZWA");
            return View();
        }

        // POST: PYTANIE/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PYTANIE,PYTANIE1,ID_TEST")] PYTANIE pYTANIE)
        {
            if (ModelState.IsValid)
            {
                db.PYTANIE.Add(pYTANIE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_TEST = new SelectList(db.TEST, "ID_TEST", "NAZWA", pYTANIE.ID_TEST);
            return View(pYTANIE);
        }

        // GET: PYTANIE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PYTANIE pYTANIE = db.PYTANIE.Find(id);
            if (pYTANIE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_TEST = new SelectList(db.TEST, "ID_TEST", "NAZWA", pYTANIE.ID_TEST);
            return View(pYTANIE);
        }

        // POST: PYTANIE/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PYTANIE,PYTANIE1,ID_TEST")] PYTANIE pYTANIE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pYTANIE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_TEST = new SelectList(db.TEST, "ID_TEST", "NAZWA", pYTANIE.ID_TEST);
            return View(pYTANIE);
        }

        // GET: PYTANIE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PYTANIE pYTANIE = db.PYTANIE.Find(id);
            if (pYTANIE == null)
            {
                return HttpNotFound();
            }
            return View(pYTANIE);
        }

        // POST: PYTANIE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PYTANIE pYTANIE = db.PYTANIE.Find(id);
            db.PYTANIE.Remove(pYTANIE);
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
