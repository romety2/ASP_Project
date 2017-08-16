using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ProjektNET.Models;

namespace ProjektNET.Controllers
{
    public class PogrzebyController : Controller
    {
        private ZakladPogrzebowy db = new ZakladPogrzebowy();

        public ActionResult SetTlo(string kolor)
        {
            Session["Tlo"] = kolor;
            var pogrzeby = db.Pogrzeby.Include(p => p.Grabarz).Include(p => p.Trup);
            return View("Index", pogrzeby.ToList());
        }

        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        public ActionResult Search()
        {
            return View(db.Pogrzeby.ToList());
        }

        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "PogrzebID,TrupID,GrabarzID,Data,Cena,Opis")] Pogrzeb pogrzeb,
        string min, string max, string tr, string gr)
        {
            var pogrzeby = from i in db.Pogrzeby select i;
            if ((min == null) || (max == null)) { min = "0"; max = "1000000000000000"; }
            if (pogrzeb.Opis == null) pogrzeb.Opis = "";
            double Min = 0;
            double Max = 1000000000000000;
            try
            {
                Min = Convert.ToDouble(min);
                Max = Convert.ToDouble(max);
            }
            catch(Exception e)
            {

            }
            pogrzeby = from i in db.Pogrzeby join t in db.Trupy on i.TrupID equals t.ID join g in db.Grabarze on i.GrabarzID equals g.ID
                       where (
                             ((g.Imie.ToLower().Contains(gr.ToLower()) || (g.Nazwisko.ToLower().Contains(gr.ToLower())) || (g.Pesel.ToLower().Contains(gr.ToLower()))) || tr == "") &&
                             ((t.Imie.ToLower().Contains(tr.ToLower()) || (t.Nazwisko.ToLower().Contains(tr.ToLower())) || (t.Pesel.ToLower().Contains(tr.ToLower()))) || tr == "") &&
                             (i.Cena >= Min && i.Cena <= Max) &&
                             (i.Opis.ToLower().Contains(pogrzeb.Opis.ToLower()) || pogrzeb.Opis == "")
                          )
                       select i;
            return View(pogrzeby);
        }

        // GET: Pogrzeby
        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        public ActionResult Index()
        {
            var pogrzeby = db.Pogrzeby.Include(p => p.Grabarz).Include(p => p.Trup);
            return View(pogrzeby.ToList());
        }

        // GET: Pogrzeby/Details/5
        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pogrzeb pogrzeb = db.Pogrzeby.Find(id);
            if (pogrzeb == null)
            {
                return HttpNotFound();
            }
            return View(pogrzeb);
        }

        // GET: Pogrzeby/Create
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Create()
        {
            ViewBag.GrabarzID = new SelectList(db.Grabarze.OrderBy(g => g.Pesel), "ID", "Pesel");
            ViewBag.TrupID = new SelectList(db.Trupy.OrderBy(t => t.Pesel), "ID", "Pesel");
            return View();
        }

        // POST: Pogrzeby/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Pracownik")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PogrzebID,TrupID,GrabarzID,Data,Cena,Opis")] Pogrzeb pogrzeb)
        {
            if (ModelState.IsValid)
            {
                db.Pogrzeby.Add(pogrzeb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GrabarzID = new SelectList(db.Grabarze.OrderBy(g => g.Pesel), "ID", "Pesel", pogrzeb.GrabarzID);
            ViewBag.TrupID = new SelectList(db.Trupy.OrderBy(t => t.Pesel), "ID", "Pesel", pogrzeb.TrupID);
            return View(pogrzeb);
        }

        // GET: Pogrzeby/Edit/5
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pogrzeb pogrzeb = db.Pogrzeby.Find(id);
            if (pogrzeb == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrabarzID = new SelectList(db.Grabarze.OrderBy(g => g.Pesel), "ID", "Pesel", pogrzeb.GrabarzID);
            ViewBag.TrupID = new SelectList(db.Trupy.OrderBy(t => t.Pesel), "ID", "Pesel", pogrzeb.TrupID);
            return View(pogrzeb);
        }

        // POST: Pogrzeby/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Pracownik")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PogrzebID,TrupID,GrabarzID,Data,Cena,Opis")] Pogrzeb pogrzeb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pogrzeb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GrabarzID = new SelectList(db.Grabarze.OrderBy(g => g.Pesel), "ID", "Pesel", pogrzeb.GrabarzID);
            ViewBag.TrupID = new SelectList(db.Trupy.OrderBy(t => t.Pesel), "ID", "Pesel", pogrzeb.TrupID);
            return View(pogrzeb);
        }

        // GET: Pogrzeby/Delete/5
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pogrzeb pogrzeb = db.Pogrzeby.Find(id);
            if (pogrzeb == null)
            {
                return HttpNotFound();
            }
            return View(pogrzeb);
        }

        // POST: Pogrzeby/Delete/5
        [Authorize(Roles = "Administrator, Pracownik")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pogrzeb pogrzeb = db.Pogrzeby.Find(id);
            db.Pogrzeby.Remove(pogrzeb);
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
