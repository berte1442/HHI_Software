using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HHI_InspectionSoftware.ViewModels;

namespace HHI_InspectionSoftware.Controllers
{
    public class CreateTemplatesController : Controller
    {
        private HHIEntities6 db = new HHIEntities6();

        public ActionResult Index()
        {
            return View(db.Templates.ToList());
        }

        //GET: CreateTemplates/Create
        //public ActionResult Save()
        //{
        //    //var areaIDs = db.Limitations.Include(x => x.Area).ToList();
        //    //var systemIDs = db.Limitations.Include(x => x.HomeSystem).ToList();
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Save(int? id, TemplateModel templateModel)
        {            
            int templateID = 0;
            bool doesExist = false;
            string name = null;

            /////////// uses id parameter to set templateModel.Template/////////
            if(id != null && templateModel.Template == null)
            {
                templateModel.Template = db.Templates.Find(id);
                templateID = (int)id;
                name = templateModel.Template.Name;
                doesExist = true;
            }
            ////////// uses templateModel parameter to set templateModel.Template//////////
            else if(templateModel.Template != null)
            {
                name = templateModel.Template.Name;
            }

            ///////// when creating new template, search through templates to determine if name is available /////////////
            if(name != null)
            {
                foreach (var t in db.Templates)
                {
                    if (t.Name == name)
                    {
                        doesExist = true;
                        templateID = t.ID;
                    }
                }
            }
            ////////// if name is available, save template to database ///////////
            if (!doesExist && templateModel.Template != null && templateModel.Template.Name != null)
            {
                db.Templates.Add(templateModel.Template);
                db.SaveChanges();
                templateID = templateModel.Template.ID;
            }
            
            ///////// when creating a new area, save area to database ///////////
            if (templateModel.Area != null && templateModel.Area.Name != null)
            {
                templateModel.Area.TemplateID = templateID;
                db.Areas.Add(templateModel.Area);
                db.SaveChanges();
                ViewBag.AreaID = templateModel.Area.ID;
            }

            //////// when creating a new system, save system to database ///////////
            if (templateModel.HomeSystem != null && templateModel.HomeSystem.Name != null)
            {
                templateModel.HomeSystem.TemplateID = templateID;
                db.HomeSystems.Add(templateModel.HomeSystem);
                db.SaveChanges();
                ViewBag.SystemID = templateModel.HomeSystem.ID;
            }

            ///////// sets templateModel.Template.Areas ///////////
            foreach (var a in db.Areas)
            {
                if (a.TemplateID == templateID)
                {
                    templateModel.Template.Areas.Add(a);
                }
            }

            ///////// sets templateModel.Template.HomeSystems //////////
            foreach (var s in db.HomeSystems)
            {
                if (s.TemplateID == templateID)
                {
                    templateModel.Template.HomeSystems.Add(s);
                }
            }

            //////// executes following code with creating new limitation ///////////////
            if (templateModel.Limitation != null && templateModel.Limitation.Name != null)
            {
                /////// checks database to see if limitation name for specific area or system is taken //////////
                var lName = db.Limitations.FirstOrDefault(x => x.Name == templateModel.Limitation.Name &&
                                         x.AreaID == templateModel.Limitation.AreaID &&
                                         x.SystemID == templateModel.Limitation.SystemID);

                /////// saves limitation to database if available /////////////
                if (lName == null)
                {
                    Limitation limitation = templateModel.Limitation;
                    db.Limitations.Add(limitation);
                    db.SaveChanges();
                }

                //////// retrieves all limitations for each area in templateModel //////////
                List<Limitation> limitations = new List<Limitation>();
                foreach (var l in db.Limitations)
                {
                    if (l.AreaID == templateModel.Limitation.AreaID)
                    {
                        limitations.Add(l);
                    }
                }
                templateModel.Limitations = limitations;
            }


            //////// executes following code with creating new checkItem ///////////////
            if (templateModel.CheckItem != null && templateModel.CheckItem.Name != null)
            {
                /////// checks database to see if checkItem name for specific area or system is taken //////////
                var ciName = db.CheckItems.FirstOrDefault(x => x.Name == templateModel.CheckItem.Name &&
                                            x.AreaID == templateModel.CheckItem.AreaID &&
                                            x.SystemID == templateModel.CheckItem.SystemID);

                //////// saves checkItem to database if available///////////
                if (ciName == null)
                {
                    CheckItem checkItem = templateModel.CheckItem;
                    db.CheckItems.Add(checkItem);
                    db.SaveChanges();
                }

                //////////// retrives all checkItems for each area in templateModel ////////////////
                List<CheckItem> checkItems = new List<CheckItem>();
                foreach (var c in db.CheckItems)
                {
                    if (c.AreaID == templateModel.CheckItem.AreaID)
                    {
                        checkItems.Add(c);
                    }
                }
                templateModel.CheckItems = checkItems;
            }

            /////// ensures templateModel.Template.ID is set ///////
            if (templateModel.Template != null)
            {
                if(templateModel.Template.ID == null)
                {
                    templateModel.Template.ID = templateID;
                }
                else
                {
                    templateModel.Template = db.Templates.Find(templateModel.Template.ID);
                }

                ////// ViewBag data created for views ////////
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
            templateModel.Template.Areas = areas;
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
            templateModel.Template.HomeSystems = systems;
            return View("Save", templateModel);
        }

        public ActionResult SaveLimitation(TemplateModel templateModel)
        {
            var lName = db.Limitations.FirstOrDefault(x => x.Name == templateModel.Limitation.Name &&
                                                 x.AreaID == templateModel.Limitation.AreaID &&
                                                 x.SystemID == templateModel.Limitation.SystemID);
            if (lName == null)
            {
                Limitation limitation = templateModel.Limitation;
                db.Limitations.Add(limitation);
                db.SaveChanges();
            }
            List<Limitation> limitations = new List<Limitation>();
            foreach(var l in db.Limitations)
            {
                if(l.AreaID == templateModel.Limitation.AreaID)
                {
                    limitations.Add(l);
                }
            }
            templateModel.Limitations = limitations;

            List<CheckItem> checkItems = new List<CheckItem>();
            foreach (var c in db.CheckItems)
            {
                if (c.AreaID == templateModel.CheckItem.AreaID)
                {
                    checkItems.Add(c);
                }
            }
            templateModel.CheckItems = checkItems;
            templateModel.Template = db.Templates.Find(templateModel.Template.ID);

            List<Area> areas = new List<Area>();
            foreach(var a in db.Areas)
            {
                if(a.TemplateID == templateModel.Template.ID)
                {
                    areas.Add(a);
                }
            }
            templateModel.Template.Areas = areas;

            List<HomeSystem> systems = new List<HomeSystem>();
            foreach(var s in db.HomeSystems)
            {
                if(s.TemplateID == templateModel.Template.ID)
                {
                    systems.Add(s);
                }
            }
            templateModel.Template.HomeSystems = systems;


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
            //templateModel.Limitation = null;
            return View("Save", templateModel);
        }

        public ActionResult SaveCheckItem(TemplateModel templateModel)
        {
            var ciName = db.CheckItems.FirstOrDefault(x => x.Name == templateModel.CheckItem.Name && 
                                                        x.AreaID == templateModel.CheckItem.AreaID &&
                                                        x.SystemID == templateModel.CheckItem.SystemID);
            if (ciName == null)
            {
                CheckItem checkItem = templateModel.CheckItem;
                db.CheckItems.Add(checkItem);
                db.SaveChanges();
            }
            List<CheckItem> checkItems = new List<CheckItem>();
            foreach (var c in db.CheckItems)
            {
                if (c.AreaID == templateModel.CheckItem.AreaID)
                {
                    checkItems.Add(c);
                }
            }
            templateModel.CheckItems = checkItems;

            templateModel.Template = db.Templates.Find(templateModel.Template.ID);

            List<Area> areas = new List<Area>();
            foreach (var a in db.Areas)
            {
                if (a.TemplateID == templateModel.Template.ID)
                {
                    areas.Add(a);
                }
            }
            templateModel.Template.Areas = areas;

            List<HomeSystem> systems = new List<HomeSystem>();
            foreach (var s in db.HomeSystems)
            {
                if (s.TemplateID == templateModel.Template.ID)
                {
                    systems.Add(s);
                }
            }
            templateModel.Template.HomeSystems = systems;


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
            //templateModel.Limitation = null;
            return View("Save", templateModel);
        }
    }
}