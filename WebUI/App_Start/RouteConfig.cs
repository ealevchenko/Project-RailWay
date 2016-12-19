using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(null,
            //"",
            //new
            //{
            //    controller = "MT",
            //    action = "Index",
            //    idstart = 0,
            //    id = 0
            //}
            //);

            //routes.MapRoute(null,
            //"ID{id}",
            //new { controller = "MT", action = "Index", idstart = 0 },
            //new { id = @"\d+" }
            //);

            //routes.MapRoute(null,
            //"IDStart{idstart}",
            //new { controller = "MT", action = "Index", id = idstart }
            //);

            routes.MapRoute(null,
            "IDStart{idstart}/ID{id}",
            new { controller = "MT", action = "Index" },
            new { id = @"\d+" }
            );
            
            
            
            
            
            
            //routes.MapRoute(
            //    name: null,
            //    url: "IDStart",
            //    defaults: new { Controller = "MT", action = "Index" }
            //);
            //routes.MapRoute(null,
            //    "{IDStart}/ID",
            //    new { controller = "MT", action = "Index" },
            //    new { ID = @"\d+" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", ID = UrlParameter.Optional }
            );
        }
    }
}
