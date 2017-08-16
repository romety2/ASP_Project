using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ProjektNET.Models;

namespace ProjektNET.Controllers
{
    public class TrupyController : Controller
    {
        private ZakladPogrzebowy db = new ZakladPogrzebowy();

        public ActionResult SetTlo(string kolor)
        {
            Session["Tlo"] = kolor;
            return View("Index", db.Trupy.ToList());
        }

        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        public ActionResult Search()
        {
            return View(db.Trupy.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        public ActionResult Search([Bind(Include = "ID,Imie,Nazwisko,Pesel,DataSmierci")] Trup trup)
        {
            var trupy = from i in db.Trupy select i;
            if (trup.Imie == null) trup.Imie = "";
            if (trup.Nazwisko == null) trup.Nazwisko = "";
            if (trup.Pesel == null) trup.Pesel = "";
            trupy = from i in db.Trupy
                    where (
                             (i.Imie.ToLower().Contains(trup.Imie.ToLower()) || trup.Imie == "") &&
                             (i.Nazwisko.ToLower().Contains(trup.Nazwisko.ToLower()) || trup.Nazwisko == "") &&
                             (i.Pesel.ToLower().Contains(trup.Pesel.ToLower()) || trup.Pesel == "")
                          )
                    select i;
            return View(trupy);
        }

        // GET: Trupy
        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        public ActionResult Index()
        {
            return View(db.Trupy.ToList());
        }

        // GET: Trupy/Details/5
        [Authorize(Roles = "Administrator, Pracownik, Klient")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trup trup = db.Trupy.Find(id);
            if (trup == null)
            {
                return HttpNotFound();
            }
            return View(trup);
        }

        // GET: Trupy/Create
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trupy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Create([Bind(Include = "ID,Imie,Nazwisko,Pesel,DataSmierci")] Trup trup)
        {
            if (ModelState.IsValid)
            {
                db.Trupy.Add(trup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trup);
        }

        // GET: Trupy/Edit/5
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trup trup = db.Trupy.Find(id);
            if (trup == null)
            {
                return HttpNotFound();
            }
            return View(trup);
        }

        // POST: Trupy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Edit([Bind(Include = "ID,Imie,Nazwisko,Pesel,DataSmierci")] Trup trup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trup);
        }

        // GET: Trupy/Delete/5
        [Authorize(Roles = "Administrator, Pracownik")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trup trup = db.Trupy.Find(id);
            if (trup == null)
            {
                return HttpNotFound();
            }
            return View(trup);
        }

        // POST: Trupy/Delete/5
        [Authorize(Roles = "Administrator, Pracownik")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trup trup = db.Trupy.Find(id);
            db.Trupy.Remove(trup);
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
