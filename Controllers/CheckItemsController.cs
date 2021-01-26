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
    public class CheckItemsController : Controller
    {
        private HHIEntities4 db = new HHIEntities4();

        // GET: CheckItems
        public ActionResult Index()
        {
            var checkItem = db.CheckItem.Include(c => c.Area).Include(c => c.HomeSystem);
            return View(checkItem.ToList());
        }

        // GET: CheckItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckItem checkItem = db.CheckItem.Find(id);
            if (checkItem == null)
            {
                return HttpNotFound();
            }
            return View(checkItem);
        }

        // GET: CheckItems/Create
        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(db.Area, "ID", "Name");
            ViewBag.SystemID = new SelectList(db.HomeSystem, "ID", "Name");
            return View();
        }

        // POST: CheckItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,SystemID,AreaID")] CheckItem checkItem)
        {
            if (ModelState.IsValid)
            {
                db.CheckItem.Add(checkItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AreaID = new SelectList(db.Area, "ID", "Name", checkItem.AreaID);
            ViewBag.SystemID = new SelectList(db.HomeSystem, "ID", "Name", checkItem.SystemID);
            return View(checkItem);
        }

        // GET: CheckItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckItem checkItem = db.CheckItem.Find(id);
            if (checkItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Area, "ID", "Name", checkItem.AreaID);
            ViewBag.SystemID = new SelectList(db.HomeSystem, "ID", "Name", checkItem.SystemID);
            return View(checkItem);
        }

        // POST: CheckItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,SystemID,AreaID")] CheckItem checkItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.Area, "ID", "Name", checkItem.AreaID);
            ViewBag.SystemID = new SelectList(db.HomeSystem, "ID", "Name", checkItem.SystemID);
            return View(checkItem);
        }

        // GET: CheckItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckItem checkItem = db.CheckItem.Find(id);
            if (checkItem == null)
            {
                return HttpNotFound();
            }
            return View(checkItem);
        }

        // POST: CheckItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckItem checkItem = db.CheckItem.Find(id);
            db.CheckItem.Remove(checkItem);
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
