using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExcellentTasteMathijsPattipeilohy.Models;

namespace ExcellentTasteMathijsPattipeilohy.Controllers
{
    public class ReserveringsController : Controller
    {
        private ExcellentTasteDBEntities db = new ExcellentTasteDBEntities();

        // GET: Reserverings
        public ActionResult Index()
        {
            var reservering = db.Reservering.Include(r => r.Klant);
            return View(reservering.ToList());
        }

        // GET: Reserverings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservering reservering = db.Reservering.Find(id);
            if (reservering == null)
            {
                return HttpNotFound();
            }
            return View(reservering);
        }

        // GET: Reserverings/Create
        public ActionResult Create()
        {
            ViewBag.klantId = new SelectList(db.Klant, "klantId", "klantNaam");
            return View();
        }

        // POST: Reserverings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reserveringId,klantId,datum,tijd,tafel,aantalPersonen,status,datumToegevoegd,bonDatum")] Reservering reservering)
        {
            //variable met tijd nu
            var now = DateTime.Now;

            //datum vandaag reservering
            reservering.datumToegevoegd = now;

            //!bondatum in het sql script wat ik heb gekregen staat op not null dus ik ga er van uit dat dan de bon nu aangemaakt moet worden. datumtoegevoegd en bondatum zijn dan gewoon hetzelfde?
            reservering.bonDatum = now;

            //reservering status klant wel of niet komen opdagen 1= wel 0 = niet, automatisch 1
            reservering.status = 1;

            if (ModelState.IsValid)
            {
                db.Reservering.Add(reservering);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.klantId = new SelectList(db.Klant, "klantId", "klantNaam", reservering.klantId);
            return View(reservering);
        }

        // GET: Reserverings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservering reservering = db.Reservering.Find(id);
            if (reservering == null)
            {
                return HttpNotFound();
            }
            ViewBag.klantId = new SelectList(db.Klant, "klantId", "klantNaam", reservering.klantId);
            return View(reservering);
        }

        // POST: Reserverings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reserveringId,klantId,datum,tijd,tafel,aantalPersonen,status,datumToegevoegd,bonDatum")] Reservering reservering)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservering).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.klantId = new SelectList(db.Klant, "klantId", "klantNaam", reservering.klantId);
            return View(reservering);
        }

        // GET: Reserverings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservering reservering = db.Reservering.Find(id);
            if (reservering == null)
            {
                return HttpNotFound();
            }
            return View(reservering);
        }

        // POST: Reserverings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservering reservering = db.Reservering.Find(id);
            db.Reservering.Remove(reservering);
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
