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
    [Authorize(Roles = "medewerker")]
    public class KlantsController : Controller
    {
        private ExcellentTasteDBEntities db = new ExcellentTasteDBEntities();

        // GET: Klants
        public ActionResult Index()
        {
            return View(db.Klant.ToList());
        }

        // GET: Klants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klant klant = db.Klant.Find(id);
            if (klant == null)
            {
                return HttpNotFound();
            }
            return View(klant);
        }

        // GET: Klants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Klants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "klantId,klantNaam,telefoon,status")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                db.Klant.Add(klant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klant);
        }

        // GET: Klants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klant klant = db.Klant.Find(id);
            if (klant == null)
            {
                return HttpNotFound();
            }
            return View(klant);
        }

        // POST: Klants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "klantId,klantNaam,telefoon,status")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klant);
        }

        // GET: Klants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klant klant = db.Klant.Find(id);
            if (klant == null)
            {
                return HttpNotFound();
            }
            return View(klant);
        }

        // POST: Klants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klant klant = db.Klant.Find(id);
            db.Klant.Remove(klant);
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
