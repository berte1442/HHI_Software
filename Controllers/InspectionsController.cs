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
        private HHIEntities4 db = new HHIEntities4();

        // GET: Inspections
        public ActionResult Index()
        {
            var inspection = db.Inspection.Include(i => i.Address).Include(i => i.Customer).Include(i => i.Inspector).Include(i => i.InspectionStatus).Include(i => i.Realtor);
            return View(inspection.ToList());
        }

        // GET: Inspections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspection.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        public ActionResult Create()
        {
            List<Address> addresses = db.Address.ToList();
            List<Customer> customers = db.Customer.ToList();
            List<Realtor> realtors = db.Realtor.ToList();

            ViewBag.AddressID = new SelectList(db.Address, "ID", "Address1", addresses.Last().ID);
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "FirstName", customers.Last().ID);
            ViewBag.InspectorID = new SelectList(db.Inspector, "ID", "FirstName");
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name");
            ViewBag.RealtorID = new SelectList(db.Realtor, "ID", "FirstName", realtors.Last().ID);
            return View();
        }

        // POST: Inspections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AddressID,CustomerID,InspectorID,RealtorID,InspectionDate,InspectionStatusID,Price")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.Inspection.Add(inspection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Address, "ID", "Address1", inspection.AddressID);
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "FirstName", inspection.CustomerID);
            ViewBag.InspectorID = new SelectList(db.Inspector, "ID", "FirstName", inspection.InspectorID);
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name", inspection.InspectionStatusID);
            ViewBag.RealtorID = new SelectList(db.Realtor, "ID", "FirstName", inspection.RealtorID);
            return View(inspection);
        }

        // GET: Inspections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspection.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Address, "ID", "Address1", inspection.AddressID);
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "FirstName", inspection.CustomerID);
            ViewBag.InspectorID = new SelectList(db.Inspector, "ID", "FirstName", inspection.InspectorID);
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name", inspection.InspectionStatusID);
            ViewBag.RealtorID = new SelectList(db.Realtor, "ID", "FirstName", inspection.RealtorID);
            return View(inspection);
        }

        // POST: Inspections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AddressID,CustomerID,InspectorID,RealtorID,InspectionDate,InspectionStatusID,Price")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Address, "ID", "Address1", inspection.AddressID);
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "FirstName", inspection.CustomerID);
            ViewBag.InspectorID = new SelectList(db.Inspector, "ID", "FirstName", inspection.InspectorID);
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name", inspection.InspectionStatusID);
            ViewBag.RealtorID = new SelectList(db.Realtor, "ID", "FirstName", inspection.RealtorID);
            return View(inspection);
        }

        // GET: Inspections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspection.Find(id);
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
            Inspection inspection = db.Inspection.Find(id);
            db.Inspection.Remove(inspection);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Back()
        {
            return RedirectToAction("Create", "Realtors");
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
