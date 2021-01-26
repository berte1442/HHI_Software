using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HHI_InspectionSoftware;

namespace HHI_InspectionSoftware.Controllers
{
    public class HomeSystemsController : Controller
    {
        private HHIEntities4 db = new HHIEntities4();

        // GET: HomeSystems
        public ActionResult Index()
        {
            return View(db.HomeSystem.ToList());
        }

        // GET: HomeSystems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeSystem homeSystem = db.HomeSystem.Find(id);
            if (homeSystem == null)
            {
                return HttpNotFound();
            }
            return View(homeSystem);
        }

        // GET: HomeSystems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeSystems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] HomeSystem homeSystem)
        {
            if (ModelState.IsValid)
            {
                db.HomeSystem.Add(homeSystem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homeSystem);
        }

        // GET: HomeSystems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeSystem homeSystem = db.HomeSystem.Find(id);
            if (homeSystem == null)
            {
                return HttpNotFound();
            }
            return View(homeSystem);
        }

        // POST: HomeSystems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] HomeSystem homeSystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homeSystem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(homeSystem);
        }

        // GET: HomeSystems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeSystem homeSystem = db.HomeSystem.Find(id);
            if (homeSystem == null)
            {
                return HttpNotFound();
            }
            return View(homeSystem);
        }

        // POST: HomeSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeSystem homeSystem = db.HomeSystem.Find(id);
            db.HomeSystem.Remove(homeSystem);
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
