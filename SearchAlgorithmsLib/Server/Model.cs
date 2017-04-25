﻿using System;
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
		Dictionary<string, MazeGame> games;
		Dictionary<Maze, SolveInfo> solutionsList;
		Controller controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="newController">The new controller.</param>
        public Model(Controller newController)
		{
			controller = newController;
			singleplayerMazeList = new Dictionary<string, Maze>();
			games = new Dictionary<string, MazeGame>();
			solutionsList = new Dictionary<Maze, SolveInfo>();
		}

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        public Maze GenerateMaze(string name, int rows, int cols)
		{
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

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Shows the list.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, MazeGame> ShowList()
		{
			return games;
		}

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
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