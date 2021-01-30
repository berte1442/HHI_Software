using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HHI_InspectionSoftware.Controllers
{
    public class CreateTemplatesController : Controller
    {
        private HHIEntities5 db = new HHIEntities5();

        // GET: CreateTemplates
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Template template,
            [Bind(Include = "ID,Name,TemplateID")] Area area)
        {
            if (ModelState.IsValid)
            {
                db.Templates.Add(template);
                db.SaveChanges();
                int templateID = db.Templates.Max(item => item.ID);
                area.TemplateID = templateID;
                db.Areas.Add(area);
                db.SaveChanges();
            }
            
            return View();
        }
    }
}