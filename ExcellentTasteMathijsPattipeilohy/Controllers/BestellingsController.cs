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

namespace ExcellentTasteMathijsPattipeilohy.Controllers
{
    public class BestellingsController : Controller
    {
        private ExcellentTasteDBEntities db = new ExcellentTasteDBEntities();

        // GET: Bestellings Bar
        public ActionResult Bar()
        {
            //linq statment die dranken weergeeft die nog niet klaar zijn voor barman
            return View(db.Bestelling.Where(s => s.ConsumptieItem.ConsumptieGroep.Consumptie.consumptieCode == "drk").Where(s => s.isKlaar == false));
        }

        // GET: Bestellings Kok
        public ActionResult Kok()
        {
            //linq statement die al het eten weergeeft die nog niet klaar zijn
            return View(db.Bestelling.Where(s => s.ConsumptieItem.ConsumptieGroep.Consumptie.consumptieCode != "drk").Where(s => s.isKlaar == false));
        }

        // GET: Bestellings Index
        public ActionResult Index()
        {
            return View(db.Bestelling.Where(s => s.isKlaar == true));
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
        public ActionResult Create([Bind(Include = "bestellingId,reserveringId,consumptieItemCode,aantal,dateTimeBereidingConsumptie,prijs,totaal,isKlaar")] Bestelling bestelling)
        {
            //session van get method met reservering id
            var reserveringid = Convert.ToInt32(Session["reserveringid"]);
            bestelling.reserveringId = reserveringid;

            //variabele met datum tijd nu
            var now = DateTime.Now;

            //bestellings tijd nu
            bestelling.dateTimeBereidingConsumptie = now;

            //status bestelling isKlaar is false
            bestelling.isKlaar = false;

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

        // GET: Bestellings/Editbar/5
        public ActionResult Editbar(int? id)
        {
            //session bestellingid voor post method edit
            Session["bestellingid"] = id;

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

        // POST: Bestellings/Editbar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editbar([Bind(Include = "bestellingId,reserveringId,consumptieItemCode,aantal,dateTimeBereidingConsumptie,prijs,totaal,isKlaar")] Bestelling bestelling)
        {
            //session van get method met bestelling id
            var bestellingid = Convert.ToInt32(Session["bestellingid"]);

            // data met session id ophalen
            var currentbestelling = db.Bestelling.AsNoTracking().Single(s => s.bestellingId == bestellingid);

            //data van db opgehaald en weer teruggestored via edit zodat deze niet verloren gaat
            bestelling.reserveringId = currentbestelling.reserveringId;
            bestelling.consumptieItemCode = currentbestelling.consumptieItemCode;
            bestelling.aantal = currentbestelling.aantal;
            bestelling.dateTimeBereidingConsumptie = currentbestelling.dateTimeBereidingConsumptie;

            //status bestelling isKlaar is true
            bestelling.isKlaar = true;

            if (ModelState.IsValid)
            {
                db.Entry(bestelling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Bar");
            }
            return View(bestelling);
        }

        // GET: Bestellings/Editkok/5
        public ActionResult Editkok(int? id)
        {
            //session bestellingid voor post method edit
            Session["bestellingid"] = id;

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

        // POST: Bestellings/Editkok/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editkok([Bind(Include = "bestellingId,reserveringId,consumptieItemCode,aantal,dateTimeBereidingConsumptie,prijs,totaal,isKlaar")] Bestelling bestelling)
        {
            //session van get method met bestelling id
            var bestellingid = Convert.ToInt32(Session["bestellingid"]);

            // data met session id ophalen
            var currentbestelling = db.Bestelling.AsNoTracking().Single(s => s.bestellingId == bestellingid);

            //data van db opgehaald en weer teruggestored via edit zodat deze niet verloren gaat
            bestelling.reserveringId = currentbestelling.reserveringId;
            bestelling.consumptieItemCode = currentbestelling.consumptieItemCode;
            bestelling.aantal = currentbestelling.aantal;
            bestelling.dateTimeBereidingConsumptie = currentbestelling.dateTimeBereidingConsumptie;

            //status bestelling isKlaar is true
            bestelling.isKlaar = true;

            if (ModelState.IsValid)
            {
                db.Entry(bestelling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Kok");
            }
            return View(bestelling);
        }

        // GET: Bestellings/Edit/5
        public ActionResult Edit(int? id)
        {
            //session bestellingid voor post method edit
            Session["bestellingid"] = id;

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

        // POST: Bestellings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bestellingId,reserveringId,consumptieItemCode,aantal,dateTimeBereidingConsumptie,prijs,totaal,isKlaar")] Bestelling bestelling)
        {
            //session van get method met bestelling id
            var bestellingid = Convert.ToInt32(Session["bestellingid"]);

            // data met session id ophalen
            var currentbestelling = db.Bestelling.AsNoTracking().Single(s => s.bestellingId == bestellingid);

            //data van db opgehaald en weer teruggestored via edit zodat deze niet verloren gaat
            bestelling.reserveringId = currentbestelling.reserveringId;
            bestelling.consumptieItemCode = currentbestelling.consumptieItemCode;
            bestelling.aantal = currentbestelling.aantal;
            bestelling.dateTimeBereidingConsumptie = currentbestelling.dateTimeBereidingConsumptie;
            
            //status bestelling isKlaar is true
            bestelling.isKlaar = false;

            if (ModelState.IsValid)
            {
                db.Entry(bestelling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bestelling);
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
