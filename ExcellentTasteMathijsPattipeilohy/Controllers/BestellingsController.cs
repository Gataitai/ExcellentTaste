using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExcellentTasteMathijsPattipeilohy.Models;
using PagedList;

namespace ExcellentTasteMathijsPattipeilohy.Controllers
{
    public class BestellingsController : Controller
    {
        private ExcellentTasteDBEntities db = new ExcellentTasteDBEntities();

        // GET: Bestellings
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var bestellings = from s in db.Bestelling select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                bestellings = bestellings.Where(s => s.ConsumptieItem.consumptieItemNaam.Contains(searchString));
            }

            return View(bestellings.OrderBy(i => i.dateTimeBereidingConsumptie));
        }

        // GET: Bestellings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bestelling bestelling = db.Bestelling.Find(id);
            if (bestelling == null)
            {
                return HttpNotFound();
            }
            return View(bestelling);
        }

        // GET: Bestellings/Create
        public ActionResult Create(int? id)
        {
            //session reservering id voor post method
            Session["reserveringid"] = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservering reservering = db.Reservering.Find(id);
            if (reservering == null)
            {
                return HttpNotFound();
            }


            ViewBag.consumptieItemCode = new SelectList(db.ConsumptieItem, "consumptieItemCode", "consumptieItemNaam");
            return View();
        }

        // POST: Bestellings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bestellingId,reserveringId,consumptieItemCode,aantal,dateTimeBereidingConsumptie,prijs,totaal")] Bestelling bestelling)
        {
            //session van get method met reservering id
            var reserveringid = Convert.ToInt32(Session["reserveringid"]);
            bestelling.reserveringId = reserveringid;

            //variabele met datum tijd nu
            var now = DateTime.Now;

            //bestellings tijd nu
            bestelling.dateTimeBereidingConsumptie = now;

            //viewbag met items menu
            ViewBag.consumptieItemCode = new SelectList(db.ConsumptieItem, "consumptieItemCode", "consumptieItemNaam", bestelling.consumptieItemCode);
            

            if (ModelState.IsValid)
            {
                db.Bestelling.Add(bestelling);
                db.SaveChanges();
                return RedirectToAction("create");
            }

            return View(bestelling);
        }

        // GET: Bestellings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bestelling bestelling = db.Bestelling.Find(id);
            if (bestelling == null)
            {
                return HttpNotFound();
            }
            ViewBag.consumptieItemCode = new SelectList(db.ConsumptieItem, "consumptieItemCode", "consumptieGroepCode", bestelling.consumptieItemCode);
            ViewBag.reserveringId = new SelectList(db.Reservering, "reserveringId", "betalingswijze", bestelling.reserveringId);
            return View(bestelling);
        }

        // POST: Bestellings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bestellingId,reserveringId,consumptieItemCode,aantal,dateTimeBereidingConsumptie,prijs,totaal")] Bestelling bestelling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bestelling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.consumptieItemCode = new SelectList(db.ConsumptieItem, "consumptieItemCode", "consumptieGroepCode", bestelling.consumptieItemCode);
            ViewBag.reserveringId = new SelectList(db.Reservering, "reserveringId", "betalingswijze", bestelling.reserveringId);
            return View(bestelling);
        }

        // GET: Bestellings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bestelling bestelling = db.Bestelling.Find(id);
            if (bestelling == null)
            {
                return HttpNotFound();
            }
            return View(bestelling);
        }

        // POST: Bestellings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bestelling bestelling = db.Bestelling.Find(id);
            db.Bestelling.Remove(bestelling);
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
