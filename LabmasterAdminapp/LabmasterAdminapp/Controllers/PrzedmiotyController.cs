using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LabmasterAdminapp.Models;

namespace LabmasterAdminapp.Controllers
{
    public class PrzedmiotyController : Controller
    {
        private PZEntities db = new PZEntities();

        // GET: Przedmioty
        public ActionResult Index()
        {
            var user = User.Identity.Name;
            int id = db.Prowadzacy.Where(s => s.adname == user).ToList().ElementAt(0).id;
            var przedmioty = db.Przedmioty.Where(s => s.id_prowadzacego == id);
            przedmioty = przedmioty.Include(p => p.Prowadzacy);
            return View(przedmioty.ToList());
        }

        // GET: Przedmioty/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmioty przedmioty = db.Przedmioty.Find(id);
            if (przedmioty == null)
            {
                return HttpNotFound();
            }
            return View(przedmioty);
        }

        // GET: Przedmioty/Create
        public ActionResult Create()
        {
            var user = User.Identity.Name;
            ICollection<Prowadzacy> prowadzacy = new List<Prowadzacy>();
            prowadzacy.Add(db.Prowadzacy.Where(s => s.adname == user).ToList().ElementAt(0));
            ViewBag.id_prowadzacego = new SelectList(prowadzacy, "id", "imie");
            return View();
        }

        public ActionResult Zajecia(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ICollection<Zajecia> zajecia = new List<Zajecia>();
            List<Zajecia> list = db.Zajecia.Where(s => s.id_przedmiotu == id).ToList();
            foreach(var z in list) {
                zajecia.Add(z);
            }
            return View(zajecia);
        }

        // POST: Przedmioty/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_prowadzacego,nazwa")] Przedmioty przedmioty)
        {
            if (ModelState.IsValid)
            {
                db.Przedmioty.Add(przedmioty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_prowadzacego = new SelectList(db.Prowadzacy, "id", "imie", przedmioty.id_prowadzacego);
            return View(przedmioty);
        }

        // GET: Przedmioty/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmioty przedmioty = db.Przedmioty.Find(id);
            if (przedmioty == null)
            {
                return HttpNotFound();
            }
            var user = User.Identity.Name;
            ICollection<Prowadzacy> prowadzacy = new List<Prowadzacy>();
            prowadzacy.Add(db.Prowadzacy.Where(s => s.adname == user).ToList().ElementAt(0));
            ViewBag.id_prowadzacego = new SelectList(prowadzacy, "id", "imie", przedmioty.id_prowadzacego);
            return View(przedmioty);
        }

        // POST: Przedmioty/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_prowadzacego,nazwa")] Przedmioty przedmioty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przedmioty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_prowadzacego = new SelectList(db.Prowadzacy, "id", "imie", przedmioty.id_prowadzacego);
            return View(przedmioty);
        }

        // GET: Przedmioty/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmioty przedmioty = db.Przedmioty.Find(id);
            if (przedmioty == null)
            {
                return HttpNotFound();
            }
            return View(przedmioty);
        }

        // POST: Przedmioty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Przedmioty przedmioty = db.Przedmioty.Find(id);
            db.Przedmioty.Remove(przedmioty);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ZajeciaCreate(int? id)
        {
            return RedirectToAction("Create", "Zajecia", new { id = id });
        }
        public ActionResult ZajeciaDelete(int? id)
        {
            return RedirectToAction("Delete", "Zajecia", new { id = id });
        }
        public ActionResult ZajeciaEdit(int? id)
        {
            return RedirectToAction("Edit", "Zajecia", new { id = id });
        }
        public ActionResult ZajeciaObecnosci(int? id)
        {
            return RedirectToAction("Obecnosci", "Zajecia", new { id = id });
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
