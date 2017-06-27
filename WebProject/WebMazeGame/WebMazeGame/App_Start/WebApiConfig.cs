using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebMazeGame
{
    /// <summary>
    /// WebApiConfig class
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}"
                //defaults: new { id = System.Web.Http.RouteParameter.Optional   }
            );
        }
    }
}
