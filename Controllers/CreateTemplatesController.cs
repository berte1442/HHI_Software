using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HHI_InspectionSoftware.ViewModels;

namespace HHI_InspectionSoftware.Controllers
{
    public class CreateTemplatesController : Controller
    {
        private HHIEntities6 db = new HHIEntities6();

        //// GET: CreateTemplates
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: CreateTemplates/Create
        public ActionResult Save()
        {
            return View("Save");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TemplateModel templateModel)
        {
            if (ModelState.IsValid)
            {
                // Checks to see if template name is available
                // Aborts if not, saves if so
                bool nameTaken = false;
                foreach(var t in db.Templates)
                {
                    if(templateModel.Template.Name == t.Name)
                    {
                        nameTaken = true;
                    }
                }
                if (nameTaken)
                {
                    // name taken - abort save - find out how to prompt user to change name through view 
                    return View("Save", templateModel);
                }
                else
                {
                    db.Templates.Add(templateModel.Template);
                    db.SaveChanges();
                }
            }
            return View("Save", templateModel);
        }

        //public ActionResult SaveGet(int? templateID, TemplateModel templateModel)
        //{
        //    return View("Save", templateModel);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveArea(TemplateModel templateModel)
        {
            //if (ModelState.IsValid)
            //{
                var name = templateModel.Template.Name;
                bool doesExist = false;
                int templateID = 0;
                foreach(var t in db.Templates)
                {
                    if(t.Name == name)
                    {
                        doesExist = true;
                        templateID = t.ID;
                    }
                }
                if (!doesExist)
                {
                    db.Templates.Add(templateModel.Template);
                    db.SaveChanges();
                    templateID = templateModel.Template.ID;
                }
                    
                if (templateModel.Area.Name != null)
                {
                    templateModel.Area.TemplateID = templateID;
                    db.Areas.Add(templateModel.Area);
                    db.SaveChanges();
                    ViewBag.AreaID = templateModel.Area.ID;
                }

            //}
            foreach (var a in db.Areas)
            {
                if (a.TemplateID == templateID)
                {
                    templateModel.Template.Areas.Add(a);
                }
            }

            foreach (var s in db.HomeSystems)
            {
                if (s.TemplateID == templateID)
                {
                    templateModel.Template.HomeSystems.Add(s);
                }
            }

            templateModel.Template.ID = templateID;


            if(templateModel.Template.Areas != null)
            {
                ViewBag.Areas = new SelectList(templateModel.Template.Areas, "ID", "Name");
            }
            else
            {
                ViewBag.Areas = new SelectList("");
            }
            if (templateModel.Template.HomeSystems != null)
            {
                ViewBag.Systems = new SelectList(templateModel.Template.HomeSystems, "ID", "Name");
            }
            else
            {
                ViewBag.Systems = new SelectList("");
            }

            return View("Save", templateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSystem(TemplateModel templateModel)
        {
            //if (ModelState.IsValid)
            //{
                var name = templateModel.Template.Name;
                bool doesExist = false;
                int templateID = 0;
                foreach (var t in db.Templates)
                {
                    if (t.Name == name)
                    {
                        doesExist = true;
                        templateID = t.ID;
                    }
                }
                if (!doesExist)
                {
                    db.Templates.Add(templateModel.Template);
                    db.SaveChanges();
                    templateID = templateModel.Template.ID;
                }

                if (templateModel.HomeSystem.Name != null)
                {
                    templateModel.HomeSystem.TemplateID = templateID;
                    db.HomeSystems.Add(templateModel.HomeSystem);
                    db.SaveChanges();
                    ViewBag.SystemID = templateModel.HomeSystem.ID;
                }

                foreach (var a in db.Areas)
                {
                    if (a.TemplateID == templateID)
                    {
                        templateModel.Template.Areas.Add(a);
                    }
                }

                foreach (var s in db.HomeSystems)
                {
                    if (s.TemplateID == templateID)
                    {
                        templateModel.Template.HomeSystems.Add(s);
                    }
                }
                templateModel.Template.ID = templateID;

                //ViewBag.Areas = new SelectList(templateModel.Template.Areas, "ID", "Name");
                //ViewBag.Systems = new SelectList(templateModel.Template.HomeSystems, "ID", "Name");

                //return View("Save", templateModel);
            //}

            if (templateModel.Template.Areas != null)
            {
                ViewBag.Areas = new SelectList(templateModel.Template.Areas, "ID", "Name");
            }
            else
            {
                ViewBag.Areas = new SelectList("");
            }
            if (templateModel.Template.HomeSystems != null)
            {
                ViewBag.Systems = new SelectList(templateModel.Template.HomeSystems, "ID", "Name");
            }
            else
            {
                ViewBag.Systems = new SelectList("");
            }

            return View("Save", templateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteArea(TemplateModel templateModel)
        {
            //int templateID = templateModel.Template.ID;
            //TemplateModel templateModel = new TemplateModel();
            List<Area> areas = new List<Area>();
            Area area = db.Areas.Find(templateModel.Area.ID);
            if(area != null)
            {
                db.Areas.Remove(area);
                db.SaveChanges();
               // templateModel.Template = db.Templates.Find(templateID);
            }

            foreach (var a in db.Areas)
            {
                if (a.TemplateID == templateModel.Template.ID)
                {
                    areas.Add(a);
                }
            }
            templateModel.Areas = areas;
            return View("Save", templateModel);
        }

        public ActionResult DeleteSystem(int? id, int? templateID)
        {
            //int templateID = templateModel.Template.ID;
            TemplateModel templateModel = new TemplateModel();
            List<HomeSystem> systems = new List<HomeSystem>();
            HomeSystem system = db.HomeSystems.Find(id);

            if(system != null)
            {
                db.HomeSystems.Remove(system);
                db.SaveChanges();
                templateModel.Template = db.Templates.Find(templateID);
            }

            foreach (var s in db.HomeSystems)
            {
                if (s.TemplateID == templateID)
                {
                    systems.Add(s);
                }
            }
            templateModel.HomeSystems = systems;
            return View("Save", templateModel);
        }

        public ActionResult SaveLimitation(TemplateModel templateModel)
        {
            if (ModelState.IsValid)
            {
                Limitation limitation = templateModel.Limitation;
                db.Limitations.Add(limitation);
                db.SaveChanges();
            }
            List<Limitation> limitations = new List<Limitation>();
            foreach(var l in db.Limitations)
            {
                if(l.AreaID == templateModel.Area.ID)
                {
                    limitations.Add(l);
                }
            }
            templateModel.Limitations = limitations;

            return View("Save", templateModel);
        }

        public void SaveCheckItem(CheckItem checkItem)
        {
            if (ModelState.IsValid)
            {
                db.CheckItems.Add(checkItem);
                db.SaveChanges();
            }
        }

        //public ActionResult CreateTemplate([Bind(Include = "ID,Name")] Template template, List<Area> areas)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Templates.Add(template);
        //        db.SaveChanges();
        //        int templateID = db.Templates.Max(item => item.ID);

        //        foreach (var a in areas)
        //        {
        //            a.TemplateID = templateID;
        //            db.Areas.Add(a);
        //            db.SaveChanges();
        //        }
        //    }

        //    return View();
        //}
    }
}