using MazeLib;
using Newtonsoft.Json;
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
    public class MultiplayerController : ApiController
    {
        [HttpPost()]
        [Route("api/Multiplayer/GetList")]
        public IHttpActionResult GetList()
        {
             return Ok(JsonConvert.SerializeObject(Model.ShowList().Keys.ToArray()));
        }

        [HttpPost()]
        [Route("api/Multiplayer/Start")]
        public IHttpActionResult Start(GameInfo game)
        {
            // startgame
            JObject obj = Model.StartGame(game);
            
            if (obj == null)
            {
               return NotFound();
            }

            return Ok(obj);
        }

        [HttpPost()]
        [Route("api/Multiplayer/Join")]
        public IHttpActionResult Join(GameInfo game)
        {
            // startgame
            JObject obj = Model.JoinGame(game.Name);

            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }
    }
}
