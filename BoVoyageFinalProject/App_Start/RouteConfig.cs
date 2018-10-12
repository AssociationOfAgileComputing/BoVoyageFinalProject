using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BoVoyageFinalProject
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "AboutRoute",
                url: "a-propos",
                defaults: new { controller = "Home", action = "About" }
                );

            routes.MapRoute(
                name: "ContactRoute",
                url: "contact",
                defaults: new { controller = "Home", action = "Contact" }
                );

            routes.MapRoute(
                name: "DetailVoyageRoute",
                url: "details-voyage{nom}",
                defaults: new { controller = "Home", action = "Details", id = UrlParameter.Optional }
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
