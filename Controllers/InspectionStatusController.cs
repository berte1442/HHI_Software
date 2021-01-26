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
    public class InspectionStatusController : Controller
    {
        private HHIEntities4 db = new HHIEntities4();

        // GET: InspectionStatus
        public ActionResult Index()
        {
            return View(db.InspectionStatus.ToList());
        }

        // GET: InspectionStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InspectionStatus inspectionStatus = db.InspectionStatus.Find(id);
            if (inspectionStatus == null)
            {
                return HttpNotFound();
            }
            return View(inspectionStatus);
        }

        // GET: InspectionStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InspectionStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] InspectionStatus inspectionStatus)
        {
            if (ModelState.IsValid)
            {
                db.InspectionStatus.Add(inspectionStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inspectionStatus);
        }

        // GET: InspectionStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InspectionStatus inspectionStatus = db.InspectionStatus.Find(id);
            if (inspectionStatus == null)
            {
                return HttpNotFound();
            }
            return View(inspectionStatus);
        }

        // POST: InspectionStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] InspectionStatus inspectionStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspectionStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inspectionStatus);
        }

        // GET: InspectionStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InspectionStatus inspectionStatus = db.InspectionStatus.Find(id);
            if (inspectionStatus == null)
            {
                return HttpNotFound();
            }
            return View(inspectionStatus);
        }

        // POST: InspectionStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InspectionStatus inspectionStatus = db.InspectionStatus.Find(id);
            db.InspectionStatus.Remove(inspectionStatus);
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
