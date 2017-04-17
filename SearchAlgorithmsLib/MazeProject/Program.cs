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
        
        static void Main(string[] args)
        {
            CompareSolvers(320, 320);
            Console.ReadLine();
        }

        private static void CompareSolvers(int rows, int cols)
        {
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
			MazeAdapter mazeSearchable = new MazeAdapter(maze); // Isearchable
            Console.WriteLine(maze.ToString());

            var temp = new CompareStates<Position>();

            // BFS search:
            BFS<Position> bfs = new BFS<Position>(temp);
			Solution<Position> bfsSol = bfs.Search(mazeSearchable);

            // DFS search
            DFS<Position> dfs = new DFS<Position>(temp);
            Solution<Position> dfsSol =  dfs.Search(mazeSearchable);

			// print solutions
			//printSol(bfsSol);

			Console.WriteLine("BFS"+bfs.getNumberOfNodesEvaluated());

			//printSol(dfsSol);

			Console.WriteLine("DFS"+dfs.getNumberOfNodesEvaluated());
        }
		static void printSol(Solution<Position> s)
		{
			for (int i = s.Count - 1 ; i > 0 ; i--)
			{
				Console.Write("{0}->", s[i]);
			}
			Console.WriteLine();
			Console.ReadLine();
		}
    }
}
