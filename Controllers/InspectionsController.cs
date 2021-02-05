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
    public class InspectionsController : Controller
    {
        private HHIEntities6 db = new HHIEntities6();

        // GET: Inspections
        public ActionResult Index()
        {
            var inspections = db.Inspections.Include(i => i.Address).Include(i => i.Customer).Include(i => i.Inspector).Include(i => i.InspectionStatus).Include(i => i.Realtor).Include(i => i.InspectionReport).Include(i => i.Template);
            return View(inspections.ToList());
        }

        // GET: Inspections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // GET: Inspections/Create
        public ActionResult Create(int? realtorID)
        {
            if (realtorID != null)
            {
                ViewBag.SetRealtorID = realtorID;
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "Address1");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FirstName");
            ViewBag.InspectorID = new SelectList(db.Inspectors, "ID", "FirstName");
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name");
            ViewBag.RealtorID = new SelectList(db.Realtors, "ID", "FirstName");
            ViewBag.TemplateID = new SelectList(db.Templates, "ID", "Name");
            return View();
        }

        // POST: Inspections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AddressID,CustomerID,InspectorID,RealtorID,InspectionDate,InspectionStatusID,Price,InspectionReportID")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.Inspections.Add(inspection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "Address1", inspection.AddressID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FirstName", inspection.CustomerID);
            ViewBag.InspectorID = new SelectList(db.Inspectors, "ID", "FirstName", inspection.InspectorID);
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name", inspection.InspectionStatusID);
            ViewBag.RealtorID = new SelectList(db.Realtors, "ID", "FirstName", inspection.RealtorID);
            ViewBag.InspectionReportID = new SelectList(db.InspectionReports, "ID", "Name", inspection.InspectionReportID);
            return View(inspection);
        }

        // GET: Inspections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "Address1", inspection.AddressID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FirstName", inspection.CustomerID);
            ViewBag.InspectorID = new SelectList(db.Inspectors, "ID", "FirstName", inspection.InspectorID);
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name", inspection.InspectionStatusID);
            ViewBag.RealtorID = new SelectList(db.Realtors, "ID", "FirstName", inspection.RealtorID);
            ViewBag.InspectionReportID = new SelectList(db.InspectionReports, "ID", "Name", inspection.InspectionReportID);
            ViewBag.TemplateID = new SelectList(db.Templates, "ID", "Name", inspection.TemplateID);
            return View(inspection);
        }

        // POST: Inspections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AddressID,CustomerID,InspectorID,RealtorID,InspectionDate,InspectionStatusID,Price,InspectionReportID,TemplateID")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "Address1", inspection.AddressID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FirstName", inspection.CustomerID);
            ViewBag.InspectorID = new SelectList(db.Inspectors, "ID", "FirstName", inspection.InspectorID);
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name", inspection.InspectionStatusID);
            ViewBag.RealtorID = new SelectList(db.Realtors, "ID", "FirstName", inspection.RealtorID);
            ViewBag.InspectionReportID = new SelectList(db.InspectionReports, "ID", "Name", inspection.InspectionReportID);
            ViewBag.TemplateID = new SelectList(db.Templates, "ID", "Name", inspection.TemplateID);
            return View(inspection);
        }

        // GET: Inspections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // POST: Inspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inspection inspection = db.Inspections.Find(id);
            db.Inspections.Remove(inspection);
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
