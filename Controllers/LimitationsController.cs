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
        private HHIEntities5 db = new HHIEntities5();

        // GET: Limitations
        public ActionResult Index()
        {
            var limitations = db.Limitations.Include(l => l.Area).Include(l => l.HomeSystem);
            return View(limitations.ToList());
        }

        // GET: Limitations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Limitation limitation = db.Limitations.Find(id);
            if (limitation == null)
            {
                return HttpNotFound();
            }
            return View(limitation);
        }

        // GET: Limitations/Create
        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name");
            ViewBag.SystemID = new SelectList(db.HomeSystems, "ID", "Name");
            return View();
        }

        // POST: Limitations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,SystemID,Description,AreaID")] Limitation limitation)
        {
            if (ModelState.IsValid)
            {
                db.Limitations.Add(limitation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name", limitation.AreaID);
            ViewBag.SystemID = new SelectList(db.HomeSystems, "ID", "Name", limitation.SystemID);
            return View(limitation);
        }

        // GET: Limitations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Limitation limitation = db.Limitations.Find(id);
            if (limitation == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name", limitation.AreaID);
            ViewBag.SystemID = new SelectList(db.HomeSystems, "ID", "Name", limitation.SystemID);
            return View(limitation);
        }

        // POST: Limitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,SystemID,Description,AreaID")] Limitation limitation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(limitation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name", limitation.AreaID);
            ViewBag.SystemID = new SelectList(db.HomeSystems, "ID", "Name", limitation.SystemID);
            return View(limitation);
        }

        // GET: Limitations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Limitation limitation = db.Limitations.Find(id);
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
            Limitation limitation = db.Limitations.Find(id);
            db.Limitations.Remove(limitation);
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
