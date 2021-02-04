using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HHI_InspectionSoftware.ViewModels;

namespace HHI_InspectionSoftware.Controllers
{
    public class BookInspectionsController : Controller
    {
        private HHIEntities6 db = new HHIEntities6();

        // GET: BookInspections
        public ActionResult Index()
        {
            return View();
        }

        // GET: BookInspections/Create
        public ActionResult Create()
        {
            ViewBag.InspectorID = new SelectList(db.Inspectors, "ID", "FirstName");
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name");
            ViewBag.TemplateID = new SelectList(db.Templates, "ID", "Name");
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name");

            var realtors = db.Realtors;

            BookInspectionModel viewModel = new BookInspectionModel
            {
                Realtors = realtors.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Address1,City,State,Zip,SquareFeet")] Address address,
            [Bind(Include = "ID,FirstName,LastName,Phone,Email")] Customer customer,
            [Bind(Include = "ID,FirstName,LastName,Phone,Email")] Realtor realtor,
            [Bind(Include = "ID,AddressID,CustomerID,InspectorID,RealtorID,InspectionDate,InspectionStatusID,Price,InspectionReportID")] Inspection inspection
            )
        {
            if (ModelState.IsValid)
            {
                if (realtor.FirstName != null)
                {
                    db.Realtors.Add(realtor);
                    db.Addresses.Add(address);
                    db.Customers.Add(customer);
                    db.SaveChanges();

                    inspection.AddressID = address.ID;
                    inspection.CustomerID = customer.ID;
                    inspection.RealtorID = realtor.ID;
                    db.Inspections.Add(inspection);

                    db.SaveChanges();
                }
                else
                {
                    db.Addresses.Add(address);
                    db.Customers.Add(customer);
                    db.SaveChangesAsync();

                    inspection.AddressID = address.ID;
                    inspection.CustomerID = customer.ID;
                    db.Inspections.Add(inspection);

                    db.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", "Inspections");
        }
    }
}