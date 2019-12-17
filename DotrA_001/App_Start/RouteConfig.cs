using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DotrA_001
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Shop",
                url: "Shop/{page}",
                defaults: new { controller = "Shop", action = "Index", page = 1 },
                constraints: new { page = @"\d" }
            );

            routes.MapRoute(
                name: "Members",
                url: "Members/Edit/{id}",
                defaults: new { controller = "Members", action = "Edit", id = UrlParameter.Optional },
                constraints: new { id = @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
