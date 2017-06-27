using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(WebMazeGame.App_Start.Startup))]
namespace WebMazeGame.App_Start
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}