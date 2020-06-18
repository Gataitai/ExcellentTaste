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
    public class ConsumptieItemsController : Controller
    {
        private ExcellentTasteDBEntities db = new ExcellentTasteDBEntities();

        // GET: ConsumptieItems
        public ActionResult Index()
        {
            var consumptieItem = db.ConsumptieItem.Include(c => c.ConsumptieGroep);
            return View(consumptieItem.ToList());
        }

        // GET: ConsumptieItems/Create
        public ActionResult Create()
        {
            ViewBag.consumptieGroepCode = new SelectList(db.ConsumptieGroep, "consumptieGroepCode", "consumptieGroepNaam");
            return View();
        }

        // POST: ConsumptieItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "consumptieItemCode,consumptieGroepCode,consumptieItemNaam,prijs")] ConsumptieItem consumptieItem)
        {
            if (ModelState.IsValid)
            {
                db.ConsumptieItem.Add(consumptieItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.consumptieGroepCode = new SelectList(db.ConsumptieGroep, "consumptieGroepCode", "consumptieGroepNaam", consumptieItem.consumptieGroepCode);
            return View(consumptieItem);
        }

        // GET: ConsumptieItems/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumptieItem consumptieItem = db.ConsumptieItem.Find(id);
            if (consumptieItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.consumptieGroepCode = new SelectList(db.ConsumptieGroep, "consumptieGroepCode", "consumptieGroepNaam", consumptieItem.consumptieGroepCode);
            return View(consumptieItem);
        }

        // POST: ConsumptieItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "consumptieItemCode,consumptieGroepCode,consumptieItemNaam,prijs")] ConsumptieItem consumptieItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumptieItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.consumptieGroepCode = new SelectList(db.ConsumptieGroep, "consumptieGroepCode", "consumptieGroepNaam", consumptieItem.consumptieGroepCode);
            return View(consumptieItem);
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
