using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookingHotels.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Hotel",
                    action = "List_of_hotels",
                    category = (string)null,
                    page = 1
                }
            );

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Hotel", action = "List_of_hotels", location = (string)null },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(null,
                "{location}",
                new { controller = "Hotel", action = "List_of_hotels", page = 1 }
            );

            routes.MapRoute(null,
                "{location}/Page{page}",
                new { controller = "Hotel", action = "List_of_hotels" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
