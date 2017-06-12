using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
    }
}
