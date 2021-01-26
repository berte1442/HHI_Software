using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;



namespace HHI_InspectionSoftware.Controllers
{
    public class BookInspectionController : Controller
    {
        private HHIEntities4 db = new HHIEntities4();

        // GET: BookInspection
        public ActionResult Index()
        {
            return View(db.Customer.ToList()) ;
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult CreateCustomer()
        {
            int count = db.Customer.ToList().Count;
            Customer customer = db.Customer.ToList()[count -1];
            // try to find a way to add LastName to displayed text in dropdownlist
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "FirstName", customer.ID);

            return View();
        }  
        
        //public ActionResult CreateAddress()
        //{
        //    int count = db.Address.ToList().Count;
        //    Address address = db.Address.ToList()[count -1];
        //    // try to find a way to add LastName to displayed text in dropdownlist
        //    ViewBag.AddressID = new SelectList(db.Address, "ID", "Address1", address.ID);

        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer([Bind(Include = "ID,FirstName,LastName,Phone,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                db.SaveChanges();
                //db.Dispose();
                return RedirectToAction("CreateAddress");
            }

            return View(customer);
        }
        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Phone,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
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