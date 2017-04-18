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
<<<<<<< HEAD
    public class Model : IModel
    {
        Dictionary<string, Maze> singleplayerMazeList;
        Dictionary<string, MazeGame> games;
        Dictionary<Maze, SolveInfo> solutionsList;
        Controller controller;

        public Model(Controller newController)
        {
            controller = newController;
            singleplayerMazeList = new Dictionary<string, Maze>();
            games = new Dictionary<string, MazeGame>();
            solutionsList = new Dictionary<Maze, SolveInfo>();
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            if (singleplayerMazeList.ContainsKey(name))
            {
                return null;
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

        public bool StartGame(string name, int rows, int cols, TcpClient client)
        {
            // if the game doesn't exist
            if (games.ContainsKey(name))
            {
                return true;
            }
            // generate
            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze maze = dfsMazeGenerator.Generate(rows, cols);
            maze.Name = name;

            // add to games
            MazeGame game = new MazeGame(maze);
            games[name] =  game;

            game.addPlayer(client, name);

            return false;
        }

        public Dictionary<string, MazeGame> ShowList()
        {
            List<string> list = new List<string>();
            foreach (string nameGame in games.Keys)
            { 
                if (MazeGame.gamesInfo[nameGame].players.Count !=2)
                {
                    list.Add(nameGame);
                }
            }
            return games;
        }

        public Maze JoinGame(string name, TcpClient client)
        {
            if (!games.ContainsKey(name))
            {
                return null;
            }
            MazeGame game = games[name];

            game.addPlayer(client, name);

            return MazeGame.gamesInfo[name].maze;
        }


    }
}
=======
	public class Model : IModel
	{
		Dictionary<string, Maze> singleplayerMazeList;
		Dictionary<string, MazeGame> games;
		Dictionary<Maze, SolveInfo> solutionsList;
		Controller controller;

		public Model(Controller newController)
		{
			controller = newController;
			singleplayerMazeList = new Dictionary<string, Maze>();
			games = new Dictionary<string, MazeGame>();
			solutionsList = new Dictionary<Maze, SolveInfo>();
		}

		public Maze GenerateMaze(string name, int rows, int cols)
		{
			if (singleplayerMazeList.ContainsKey(name))
			{
				return null;
			}

			DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
			Maze maze = dfsMazeGenerator.Generate(rows, cols);


			//Maze maze;
			/*using (var fil = System.IO.File.OpenWrite("nofar"))
			{
				using (var writer = new System.IO.StreamWriter(fil))
				{
					writer.Write(maze.ToJSON());
				}
			}*/

			/*using (var fil = System.IO.File.OpenRead("nofar"))
			{
				using (var reader = new System.IO.StreamReader(fil))
				{
					maze = Maze.FromJSON(reader.ReadToEnd());
				}
			}*/

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

		public bool StartGame(string name, int rows, int cols, TcpClient client)
		{
			// if the game doesn't exist
			if (games.ContainsKey(name))
			{
				return true;
			}
			// generate
			DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
			Maze maze = dfsMazeGenerator.Generate(rows, cols);
			maze.Name = name;

			// add to games
			MazeGame game = new MazeGame(maze);
			games[name] = game;

			game.addPlayer(client, name);

			return false;
		}

		public Dictionary<string, MazeGame> ShowList()
		{
			return games;
		}

		public Maze JoinGame(string name, TcpClient client)
		{
			if (!games.ContainsKey(name))
			{
				return null;
			}
			MazeGame game = games[name];

			game.addPlayer(client, name);

			return MazeGame.gamesInfo[name].maze;
		}


	}
}
>>>>>>> ed4fabfa7ebd57e25b740162d6dc41f2519e9a48
