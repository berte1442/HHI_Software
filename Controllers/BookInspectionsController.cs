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
            ViewBag.InspectorID = new SelectList(db.Inspectors, "ID", "FullName");
            ViewBag.InspectionStatusID = new SelectList(db.InspectionStatus, "ID", "Name", 2);
            ViewBag.TemplateID = new SelectList(db.Templates, "ID", "Name");
            ViewBag.RealtorID = new SelectList(db.Realtors, "ID", "FullName");

            BookInspectionModel viewModel = new BookInspectionModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookInspectionModel viewModel)
        {
            //below if statement will determine if a realtor has been picked or if any other error has been passed

            if (ModelState.IsValid || (viewModel.RealtorID == 0 && viewModel.Realtor.FirstName != null))
            {
                if (viewModel.Realtor.FirstName != null)
                {
                    db.Realtors.Add(viewModel.Realtor);
                    db.SaveChanges();
                    viewModel.Inspection.RealtorID = viewModel.Realtor.ID;
                }
                else
                {
                    viewModel.Inspection.RealtorID = viewModel.RealtorID;
                }
            }
            //else if (!ModelState.IsValid || viewModel.RealtorID == 0 && viewModel.Realtor.FirstName == null)
            //{

                db.Addresses.Add(viewModel.Address);
                db.Customers.Add(viewModel.Customer);
                db.SaveChanges();

                viewModel.Inspection.AddressID = viewModel.Address.ID;
                viewModel.Inspection.CustomerID = viewModel.Customer.ID;
                viewModel.Inspection.InspectionStatusID = viewModel.InspectionStatusID;
                viewModel.Inspection.TemplateID = viewModel.TemplateID;
                viewModel.Inspection.InspectorID = viewModel.InspectorID;

                db.Inspections.Add(viewModel.Inspection);

                db.SaveChanges();
            //}
            return RedirectToAction("Index", "Inspections");
        }




        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create([Bind(Include = "ID,Address1,City,State,Zip,SquareFeet")] Address address,
        //        [Bind(Include = "ID,FirstName,LastName,Phone,Email")] Customer customer,
        //        [Bind(Include = "ID,FirstName,LastName,Phone,Email")] Realtor realtor,
        //        [Bind(Include = "ID,AddressID,CustomerID,InspectorID,RealtorID,InspectionDate,InspectionStatusID,Price,InspectionReportID")] Inspection inspection
        //        )
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (realtor.FirstName != null)
        //            {
        //                db.Realtors.Add(realtor);
        //                db.SaveChanges();
        //                inspection.RealtorID = realtor.ID;
        //            }
        //                db.Addresses.Add(address);
        //                db.Customers.Add(customer);
        //                db.SaveChangesAsync();

        //                inspection.AddressID = address.ID;
        //                inspection.CustomerID = customer.ID;
        //                db.Inspections.Add(inspection);

        //                db.SaveChangesAsync();                
        //        }

        //        return RedirectToAction("Index", "Inspections");
        //    }
    }
        }