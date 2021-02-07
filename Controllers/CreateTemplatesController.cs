using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Template template)
        {
            if (ModelState.IsValid)
            {
                db.Templates.Add(template);
                db.SaveChanges();

                foreach(var a in areas)
                {
                    a.TemplateID = template.ID;
                    db.Areas.Add(a);
                    db.SaveChanges();
                }
            }
            
            return View();
        }

        List<Area> areas = new List<Area>();
        public void AddArea(string areaName)
        {
            Area area = new Area();
            area.Name = areaName;
            areas.Add(area);
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