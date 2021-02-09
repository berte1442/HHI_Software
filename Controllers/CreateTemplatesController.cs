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

        // GET: CreateTemplates
        public ActionResult Index()
        {
            return View();
        }

        // GET: CreateTemplates/Create
        public ActionResult Create()
        {
            return View("Save");
        }

        public ActionResult SaveGet(int? templateID, TemplateModel templateModel)
        {
            //TemplateModel templateModel = new TemplateModel();
            //templateModel.Template = db.Templates.Find(templateID);
            //templateModel.Areas = areas;

            return View("Save", templateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TemplateModel templateModel)
        {
            if (ModelState.IsValid)
            {
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

                }
                foreach (var a in db.Areas)
                {
                    if (a.TemplateID == templateID)
                    {
                        templateModel.Template.Areas.Add(a);
                    }
                }
                if (templateModel.HomeSystem.Name != null)
                {
                    templateModel.HomeSystem.TemplateID = templateID;
                    db.HomeSystems.Add(templateModel.HomeSystem);
                    db.SaveChanges();

                }
                foreach (var s in db.HomeSystems)
                {
                    if (s.TemplateID == templateID)
                    {
                        templateModel.Template.HomeSystems.Add(s);
                    }
                }
                return View(templateModel);
               
            }
            
            return View();
        }

        public ActionResult DeleteArea(int? id, int? templateID)
        {
            //int templateID = templateModel.Template.ID;
            Area area = db.Areas.Find(id);
            db.Areas.Remove(area);
            db.SaveChanges();
            TemplateModel templateModel = new TemplateModel();
            templateModel.Template = db.Templates.Find(templateID);
            List<Area> areas = new List<Area>();
            foreach (var a in db.Areas)
            {
                if (a.TemplateID == templateID)
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
            HomeSystem system = db.HomeSystems.Find(id);
            db.HomeSystems.Remove(system);
            db.SaveChanges();
            TemplateModel templateModel = new TemplateModel();
            templateModel.Template = db.Templates.Find(templateID);
            List<HomeSystem> systems = new List<HomeSystem>();
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

        public ActionResult CreateTemplate([Bind(Include = "ID,Name")] Template template, List<Area> areas)
        {
            if (ModelState.IsValid)
            {
                db.Templates.Add(template);
                db.SaveChanges();
                int templateID = db.Templates.Max(item => item.ID);

                foreach (var a in areas)
                {
                    a.TemplateID = templateID;
                    db.Areas.Add(a);
                    db.SaveChanges();
                }
            }

            return View();
        }
    }
}