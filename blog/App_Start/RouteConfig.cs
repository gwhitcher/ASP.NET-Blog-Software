using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace blog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Blog SEO title",
                url: "post/{id}/{Slug}",
                defaults: new { controller = "Blog", action = "Details", id = UrlParameter.Optional, Slug = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Blog",
                url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
