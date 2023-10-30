using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAnCnpm
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "TrangHome", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "DeleteComment",
            url: "Product/DeleteComment/{commentId}",
             defaults: new { controller = "Product", action = "DeleteComment", commentId = UrlParameter.Optional }
            );

        }
    }
}
