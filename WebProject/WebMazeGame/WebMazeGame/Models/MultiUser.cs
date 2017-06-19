using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMazeGame.Models;

namespace WebMazeGame.Models
{
    public class MultiUser
    {
        public GameInfo info { get; set; }
        public string UserId { get; set; }
    }
}