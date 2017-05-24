using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace MazeProject
{
    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            CompareSolvers(320, 320);
            Console.ReadLine();
        }

        /// <summary>
        /// Compares the solvers.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        private static void CompareSolvers(int rows, int cols)
        {
            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze maze = dfsMazeGenerator.Generate(rows, cols);
            MazeAdapter mazeSearchable = new MazeAdapter(maze); // Isearchable
            Console.WriteLine(maze.ToString());

            var temp = new CompareStates<Position>();

            // BFS search:
            BFS<Position> bfs = new BFS<Position>(temp);
            Solution<Position> bfsSol = bfs.Search(mazeSearchable);

            // DFS search
            DFS<Position> dfs = new DFS<Position>(temp);
            Solution<Position> dfsSol = dfs.Search(mazeSearchable);

            Console.WriteLine("BFS" + bfs.GetNumberOfNodesEvaluated());

            Console.WriteLine("DFS" + dfs.GetNumberOfNodesEvaluated());
        }
    }
}