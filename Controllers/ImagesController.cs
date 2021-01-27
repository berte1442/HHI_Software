﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HHI_InspectionSoftware;

namespace HHI_InspectionSoftware.Controllers
{
    public class ImagesController : Controller
    {
        private HHIEntities4 db = new HHIEntities4();

        // GET: Images
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Images imageModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + extension;
            imageModel.FilePath = "~/Photos/" + fileName;
            // reconfigure below code to save images to newly created folder for specific inspection

            fileName = Path.Combine(Server.MapPath("~/Photos/"), fileName);
            imageModel.ImageFile.SaveAs(fileName);

            if (ModelState.IsValid)
            {
                db.Images.Add(imageModel);
                db.SaveChanges();
                return RedirectToAction("Index", "Images");
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            Images imageModel = new Images();
            imageModel = db.Images.Where(i => i.ID == id).FirstOrDefault();

            return View();
        }



        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,FilePath")] Images images)
        {
            if (ModelState.IsValid)
            {
                db.Images.Add(images);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(images);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,FilePath")] Images images)
        {
            if (ModelState.IsValid)
            {
                db.Entry(images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(images);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Images images = db.Images.Find(id);
            db.Images.Remove(images);
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
