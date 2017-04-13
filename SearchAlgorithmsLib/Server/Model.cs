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
        Maze maze;
        Controller controller;

        public Model(Controller newController)
        {
            controller = newController;
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze maze = dfsMazeGenerator.Generate(rows, cols);
            maze.Name = name;
            return maze;
        }
    }
}
