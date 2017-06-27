using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebMazeGame.Models;

namespace WebMazeGame.Controllers
{
    /// <summary>
    /// SingleGameController class
    /// </summary>
    public class SingleGameController : ApiController
    {
        private static Maze maze;

        /// <summary>
        /// GenerateMaze class
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("api/SingleGame/GenerateMaze")]
        public IHttpActionResult GenerateMaze(Game game)
        {
            maze = Model.GenerateMaze(game.Name, game.Rows, game.Cols);
            JObject obj = JObject.Parse(maze.ToJSON());
            return Ok(obj);
        }

        /// <summary>
        /// Solve the maze 
        /// </summary>
        /// <param name="solveRequest"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("api/SingleGame/SolveMaze")]
        public IHttpActionResult SolveMaze(SolveRequest solveRequest)
        {
            string searchAlgo = "1";
            if (solveRequest.SearchAlgo == "BFS")
            {
                searchAlgo = "0";
            }
             SolveInfo solveInfo = Model.SolveMaze(solveRequest.Name, searchAlgo);
             SolutionAdapter solutionAdapter = new SolutionAdapter(solveInfo.Solution);
             string strSolution = solutionAdapter.CreateString();

             JObject solutionObj = new JObject();
             solutionObj["Name"] = solveRequest.Name;
             solutionObj["Solution"] = strSolution;

             return Ok(solutionObj);
        }

    }
}
