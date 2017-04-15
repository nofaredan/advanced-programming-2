using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;

namespace Server
{
    public class Model : IModel
    {
		Dictionary<string, Maze> SingleplayerMazeList;
        Controller controller;

        public Model(Controller newController)
        {
            controller = newController;
			SingleplayerMazeList = new Dictionary<string, Maze>();
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
			if (SingleplayerMazeList.ContainsKey(name)){
				return SingleplayerMazeList[name];
			}

            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze maze = dfsMazeGenerator.Generate(rows, cols);
            maze.Name = name;

			SingleplayerMazeList.Add(maze.Name, maze);
            return maze;
        }
    }
}
