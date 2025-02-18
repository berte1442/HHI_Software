﻿using System;
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
    public class RealtorsController : Controller
    {
        private HHIEntities6 db = new HHIEntities6();

        // GET: Realtors
        public ActionResult Index()
        {
            return View(db.Realtors.ToList());
        }

        // GET: Realtors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Realtor realtor = db.Realtors.Find(id);
            if (realtor == null)
            {
                return HttpNotFound();
            }
            return View(realtor);
        }

        // GET: Realtors/Create
        public ActionResult Create()
        {
            ViewBag.RealtorsID = new SelectList(db.Realtors, "ID", "FirstName");
            return View();
        }

        // POST: Realtors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Phone,Email")] Realtor realtor)
        {
            if (ModelState.IsValid)
            {
                db.Realtors.Add(realtor);
                db.SaveChanges();
                return RedirectToAction("Create", "Inspections");
            }

            return View(realtor);
        }

        [Route("inspections/create/{realtorID}")]
        public ActionResult Select(int realtorID)
        {
            return RedirectToAction("Create", "Inspections", new { realtorID });
        }

        // GET: Realtors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Realtor realtor = db.Realtors.Find(id);
            if (realtor == null)
            {
                return HttpNotFound();
            }
            return View(realtor);
        }

        // POST: Realtors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Phone,Email")] Realtor realtor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realtor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(realtor);
        }

        // GET: Realtors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Realtor realtor = db.Realtors.Find(id);
            if (realtor == null)
            {
                return HttpNotFound();
            }
            return View(realtor);
        }

        // POST: Realtors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Realtor realtor = db.Realtors.Find(id);
            db.Realtors.Remove(realtor);
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
