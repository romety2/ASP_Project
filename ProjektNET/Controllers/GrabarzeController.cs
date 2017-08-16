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
    public class GrabarzeController : Controller
    {
        private ZakladPogrzebowy db = new ZakladPogrzebowy();

        public ActionResult SetTlo(string kolor)
        {
            Session["Tlo"] = kolor;
            return View("Index", db.Grabarze.ToList());
        }

        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        public ActionResult Search()
        {
            return View(db.Grabarze.ToList());
        }

        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "ID,Imie,Nazwisko,Pesel,DataUrodzenia")] Grabarz grabarz)
        {
            var grabarze = from i in db.Grabarze select i;
            if (grabarz.Imie == null) grabarz.Imie = "";
            if (grabarz.Nazwisko == null) grabarz.Nazwisko = "";
            if (grabarz.Pesel == null) grabarz.Pesel = "";
            grabarze = from i in db.Grabarze
                       where (
                             (i.Imie.ToLower().Contains(grabarz.Imie.ToLower()) || grabarz.Imie == "") &&
                             (i.Nazwisko.ToLower().Contains(grabarz.Nazwisko.ToLower()) || grabarz.Nazwisko == "") &&
                             (i.Pesel.ToLower().Contains(grabarz.Pesel.ToLower()) || grabarz.Pesel == "")
                          )
                    select i;
            return View(grabarze);
        }

        // GET: Grabarze
        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        public ActionResult Index()
        {
            return View(db.Grabarze.ToList());
        }

        // GET: Grabarze/Details/5
        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grabarz grabarz = db.Grabarze.Find(id);
            if (grabarz == null)
            {
                return HttpNotFound();
            }
            return View(grabarz);
        }

        // GET: Grabarze/Create
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grabarze/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Create([Bind(Include = "ID,Imie,Nazwisko,Pesel,DataUrodzenia")] Grabarz grabarz)
        {
            if (ModelState.IsValid)
            {
                db.Grabarze.Add(grabarz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grabarz);
        }

        // GET: Grabarze/Edit/5
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grabarz grabarz = db.Grabarze.Find(id);
            if (grabarz == null)
            {
                return HttpNotFound();
            }
            return View(grabarz);
        }

        // POST: Grabarze/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Pracownik")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Imie,Nazwisko,Pesel,DataUrodzenia")] Grabarz grabarz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grabarz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grabarz);
        }

        // GET: Grabarze/Delete/5
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grabarz grabarz = db.Grabarze.Find(id);
            if (grabarz == null)
            {
                return HttpNotFound();
            }
            return View(grabarz);
        }

        // POST: Grabarze/Delete/5
        [Authorize(Roles = "Administrator, Pracownik")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grabarz grabarz = db.Grabarze.Find(id);
            db.Grabarze.Remove(grabarz);
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
