using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebMazeGame.Models;

namespace WebMazeGame.Controllers
{
    public class UserController : ApiController
    {
        private static List<User> users = new List<User>();

        [HttpPost]
        public void AddUser(User user)
        {
            users.Add(user);
            Console.WriteLine("I'm in");
        }
    }
}
