using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using MazeProject;
using System.Net.Sockets;

namespace Server
{
    public class Model : IModel
    {
		Dictionary<string, Maze> singleplayerMazeList;
        Dictionary<string, Maze> multiplayerMazeList;
        Dictionary<string, MazeGame> games;
        Dictionary<Maze, SolveInfo> solutionsList;
        Controller controller;

        public Model(Controller newController)
        {
            controller = newController;
			singleplayerMazeList = new Dictionary<string, Maze>();
            multiplayerMazeList = new Dictionary<string, Maze>();
            games = new Dictionary<string, MazeGame>();
            solutionsList = new Dictionary<Maze, SolveInfo>();
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
			if (singleplayerMazeList.ContainsKey(name)){
				return singleplayerMazeList[name];
			}

            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze maze = dfsMazeGenerator.Generate(rows, cols);
            maze.Name = name;

			singleplayerMazeList.Add(maze.Name, maze);
            return maze;
        }

        public SolveInfo SolveMaze(string name, string typeAlgo)
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
                    nodesEvaluated = bfs.getNumberOfNodesEvaluated();
                    break;
                case "1":
                    DFS<Position> dfs = new DFS<Position>(temp);
                    solution = dfs.Search(mazeSearchable);
                    nodesEvaluated = dfs.getNumberOfNodesEvaluated();
                    break;
            }
            SolveInfo solveInfo = new SolveInfo(nodesEvaluated, solution);
            // add the solution to the solutions dictionary
            solutionsList.Add(maze, solveInfo);
            return solveInfo;
        }

        public void StartGame(string name, int rows, int cols, TcpClient client)
        {
            // if the game doesn't exist
            if (!multiplayerMazeList.ContainsKey(name))
            {
                DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
                Maze maze = dfsMazeGenerator.Generate(rows, cols);
                maze.Name = name;

                MazeGame game = new MazeGame();
                games.Add(name, game);

                multiplayerMazeList.Add(maze.Name, maze);
            }
        }

        public Dictionary<string, Maze> ShowList()
        {
            return multiplayerMazeList;
        }

        public Maze JoinGame(string name, TcpClient client)
        {
            if (!multiplayerMazeList.ContainsKey(name))
            {
                Console.WriteLine("there's no maze "+name);
                return null;
            }
            Maze maze = multiplayerMazeList[name];
            MazeGame game = games[name];

            // send to all players besides this one:
            foreach (TcpClient player in game.Players)
            {
                
            }


            game.addPlayer(client);

            return maze;
        }
    }
}
