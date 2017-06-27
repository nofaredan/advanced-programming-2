using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;
using WebMazeGame.Models;
using Newtonsoft.Json.Linq;

namespace WebMazeGame
{
    public static class Model
    {
        private static Dictionary<string, Maze> singleplayerMazeList = new Dictionary<string, Maze>();
        public static Dictionary<string, GameInfo> MultiplayerMazeList=new Dictionary<string, GameInfo>();
       private static Dictionary<Maze, SolveInfo> solutionsList = new Dictionary<Maze, SolveInfo>();
       // Controller controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="newController">The new controller.</param>
       /* public Model(Controller newController)
        {
            controller = newController;
            singleplayerMazeList = new Dictionary<string, Maze>();
            MultiplayerMazeList = new Dictionary<string, MazeGame>();
        }*/

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        public static Maze GenerateMaze(string name, int rows, int cols)
        {
            Console.WriteLine(singleplayerMazeList);
            if (singleplayerMazeList.ContainsKey(name))
            {
                return null;
            }

            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze maze = dfsMazeGenerator.Generate(rows, cols);
            maze.Name = name;
            // add to single player list:
            singleplayerMazeList.Add(maze.Name, maze);
            return maze;
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="typeAlgo">The type algo.</param>
        /// <returns></returns>
        public static SolveInfo SolveMaze(string name, string typeAlgo)
        {
            Maze maze = singleplayerMazeList[name];
            MazeAdapter mazeSearchable = new MazeAdapter(maze); // Isearchable
            var temp = new CompareStates<Position>();
            Solution<Position> solution = null;
            int nodesEvaluated = 0;

            if (solutionsList.ContainsKey(maze))
            {
                return solutionsList[maze];
            }

            // find solution of the maze:
            switch (typeAlgo)
            {
                case "0":
                    BFS<Position> bfs = new BFS<Position>(temp);
                    solution = bfs.Search(mazeSearchable);
                    nodesEvaluated = bfs.GetNumberOfNodesEvaluated();
                    break;
                case "1":
                    DFS<Position> dfs = new DFS<Position>(temp);
                    solution = dfs.Search(mazeSearchable);
                    nodesEvaluated = dfs.GetNumberOfNodesEvaluated();
                    break;
            }
            SolveInfo solveInfo = new SolveInfo(nodesEvaluated, solution);
            // add the solution to the solutions dictionary
            solutionsList.Add(maze, solveInfo);
            return solveInfo;
        }


        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public static JObject StartGame(GameInfo game)
        {
            // if the game exist
            if (MultiplayerMazeList.ContainsKey(game.Name))
            {
                return null;
            }

            // generate
            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze maze = dfsMazeGenerator.Generate(game.Rows, game.Cols);
            maze.Name = game.Name;

            // add maze to game info
            game.Maze = maze;

            // add to games
            //  MultiMaze game = new MultiMaze(maze);
            MultiplayerMazeList[game.Name] = game;

            // return maze
            return JObject.Parse(maze.ToJSON());
        }

        public static bool AddMultiPlayer(string game,string id)
        {
            // if the game exist
            if (MultiplayerMazeList.ContainsKey(game))
            {
                GameInfo currentGame = MultiplayerMazeList[game];
                currentGame.Players.Add(id);

                // game started
                if (currentGame.Players.Count == 2)
                {
                    return true;
                }
                //return JObject.Parse(currentGame.Maze.ToJSON());
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public static void RemoveMultiGame(string game)
        {
            // cant be at game
            MultiplayerMazeList.Remove(game);
        }

        /// <summary>
        /// Shows the list.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, GameInfo> ShowList()
        {
            // returns only un-active games
            Dictionary<string, GameInfo> tempList = new Dictionary<string, GameInfo>();

            foreach (KeyValuePair<string, GameInfo> entry in MultiplayerMazeList)
            {
                if (entry.Value.Players.Count < 2)
                {
                    tempList[entry.Key] = entry.Value;
                }
            }

            return tempList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetOpponent(string game, string id)
        {
            string opponent = null;
            // if the game exist
            if (MultiplayerMazeList.ContainsKey(game))
            {
                GameInfo currentGame = MultiplayerMazeList[game];
                opponent = currentGame.GetOpponent(id);
            }

            return  opponent;
        }

        
        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public static JObject JoinGame(string name)
        {
            if (!MultiplayerMazeList.ContainsKey(name))
            {
                return null;
            }

            GameInfo game = MultiplayerMazeList[name];
            return JObject.Parse(game.Maze.ToJSON()); ;
        }
    }
}