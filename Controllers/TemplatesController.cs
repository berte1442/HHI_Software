//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using HHI_InspectionSoftware;
//using HHI_InspectionSoftware.ViewModels;

//namespace HHI_InspectionSoftware.Controllers
//{
//    public class TemplatesController : Controller
//    {
//        private HHIEntities6 db = new HHIEntities6();

//        // GET: Templates
//        public ActionResult Index()
//        {
//            return View(db.Templates.ToList());
//        }

//        // GET: Templates/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Template template = db.Templates.Find(id);
//            if (template == null)
//            {
//                return HttpNotFound();
//            }
//            return View(template);
//        }

//        // GET: Templates/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Templates/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "ID,Name")] Template template)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Templates.Add(template);
//                db.SaveChanges();
//                return RedirectToAction("Create", "Areas", new { template.ID });
//            }

//            return View(template);
//        }


//        // GET: Templates/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Template template = db.Templates.Find(id);
//            if (template == null)
//            {
//                return HttpNotFound();
//            }
//            var areas = db.Areas.Where(x => x.TemplateID == id);
//            var systems = db.Areas.Where(x => x.TemplateID == id);
//            ViewBag.AreasID = new SelectList(areas, "ID", "Name");
//            ViewBag.SystemsID = new SelectList(systems, "ID", "Name");
//            ViewBag.TemplateID = id;
//            return View(template);
//        }

//        // POST: Templates/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "ID,Name")] Template template)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(template).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(template);
//        }

//        // GET: Templates/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Template template = db.Templates.Find(id);
//            if (template == null)
//            {
//                return HttpNotFound();
//            }
//            return View(template);
//        }

//        // POST: Templates/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            // add confirmation message before delete

//            Template template = db.Templates.Find(id);
//            IEnumerable<Area> areas = db.Areas.Where(x => x.TemplateID == id);
//            IEnumerable<HomeSystem> systems = db.HomeSystems.Where(x => x.TemplateID == id);
//            foreach(var a in areas)
//            {
//                IEnumerable<CheckItem> checkItems = db.CheckItems.Where(x => x.AreaID == a.ID);
//                foreach(var c in checkItems)
//                {
//                    IEnumerable<Comment> comments = db.Comments.Where(x => x.CheckItemID == c.ID);
//                    foreach(var co in comments)
//                    {
//                        db.Comments.Remove(co);
//                    }
//                    db.CheckItems.Remove(c);
//                }
//                db.Areas.Remove(a);
//            }
//            foreach(var s in systems)
//            {
//                IEnumerable<CheckItem> checkItems = db.CheckItems.Where(x => x.SystemID == s.ID);
//                foreach (var c in checkItems)
//                {
//                    IEnumerable<Comment> comments = db.Comments.Where(x => x.CheckItemID == c.ID);
//                    foreach(var co in comments)
//                    {
//                        db.Comments.Remove(co);
//                    }
//                    db.CheckItems.Remove(c);
//                }
//                db.HomeSystems.Remove(s);
//            }
//            db.Templates.Remove(template);
//            db.SaveChanges();

//            return RedirectToAction("Index");
//        }

//        //public ActionResult CreateNewTemplate()
//        //{
//        //    TemplateModel templateModel = new TemplateModel();
//        //    templateModel.Areas = db.Areas.ToList();
//        //    templateModel.Categories = db.Categories.ToList();
//        //    templateModel.CheckItems = db.CheckItems.ToList();
//        //    templateModel.Comments = db.Comments.ToList();
//        //    templateModel.HomeSystems = db.HomeSystems.ToList();
//        //    templateModel.Images = db.Images.ToList();
//        //    templateModel.Limitations = db.Limitations.ToList();
//        //    templateModel.Templates = db.Templates.ToList();

//        //    //ViewBag.AreasID = new SelectList(db.Areas, "ID", "Name", "IsSelected");

//        //    return View(templateModel);
//        //}

//        ////[HttpPost]
//        ////[ValidateAntiForgeryToken]
//        //public ActionResult CreateTemplate([Bind(Include = "ID,Name")] Template template, List<Area> areas)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        db.Templates.Add(template);
//        //        db.SaveChanges();
//        //        int templateID = db.Templates.Max(item => item.ID);

//        //        foreach(var a in areas)
//        //        {
//        //            a.TemplateID = templateID;
//        //            db.Areas.Add(a);
//        //            db.SaveChanges();
//        //        }
//        //    }

//        //    return View();
//        //}

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
