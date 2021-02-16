using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HHI_InspectionSoftware
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
                "Templates",
                "Areas/Create/{templateID}",
            new { controller = "Areas", action = "Create"}
            );

            routes.MapRoute(
                 "Areas",
                 "HomeSystems/Create/{templateID}",
             new { controller = "HomeSystems", action = "Create" }
             );
                
            routes.MapRoute(
                 "HomeSystems",
                 "CheckItems/Create/{templateID}",
             new { controller = "CheckItems", action = "Create" }
             );

            routes.MapRoute(
                name: "CreateTemplates",
                url: "CreateTemplates/Save/{id}",
                defaults: new { controller = "CreateTemplates", action = "Save", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
