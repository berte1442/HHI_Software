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
    public class LimitationsController : Controller
    {
        private HHIEntities4 db = new HHIEntities4();

        // GET: Limitations
        public ActionResult Index()
        {
            var limitation = db.Limitation.Include(l => l.HomeSystem);
            return View(limitation.ToList());
        }

        // GET: Limitations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Limitation limitation = db.Limitation.Find(id);
            if (limitation == null)
            {
                return HttpNotFound();
            }
            return View(limitation);
        }

        // GET: Limitations/Create
        public ActionResult Create()
        {
            ViewBag.SystemID = new SelectList(db.HomeSystem, "ID", "Name");
            return View();
        }

        // POST: Limitations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,SystemID,Description")] Limitation limitation)
        {
            if (ModelState.IsValid)
            {
                db.Limitation.Add(limitation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SystemID = new SelectList(db.HomeSystem, "ID", "Name", limitation.SystemID);
            return View(limitation);
        }

        // GET: Limitations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Limitation limitation = db.Limitation.Find(id);
            if (limitation == null)
            {
                return HttpNotFound();
            }
            ViewBag.SystemID = new SelectList(db.HomeSystem, "ID", "Name", limitation.SystemID);
            return View(limitation);
        }

        // POST: Limitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,SystemID,Description")] Limitation limitation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(limitation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SystemID = new SelectList(db.HomeSystem, "ID", "Name", limitation.SystemID);
            return View(limitation);
        }

        // GET: Limitations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Limitation limitation = db.Limitation.Find(id);
            if (limitation == null)
            {
                return HttpNotFound();
            }
            return View(limitation);
        }

        // POST: Limitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Limitation limitation = db.Limitation.Find(id);
            db.Limitation.Remove(limitation);
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
