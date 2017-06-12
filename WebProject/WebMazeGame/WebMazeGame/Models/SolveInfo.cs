using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace WebMazeGame
{
	public class SolveInfo
	{
		int nodesEvaluated;
		Solution<Position> solution;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolveInfo"/> class.
        /// </summary>
        /// <param name="nEvaluated">The n evaluated.</param>
        /// <param name="mySolution">My solution.</param>
        public SolveInfo(int nEvaluated, Solution<Position> mySolution)
		{
			nodesEvaluated = nEvaluated;
			solution = mySolution;
		}

        /// <summary>
        /// Gets or sets the nodes evaluated.
        /// </summary>
        /// <value>
        /// The nodes evaluated.
        /// </value>
        public int NodesEvaluated
		{
			get
			{
				return nodesEvaluated;
			}

			set
			{
				nodesEvaluated = value;
			}
		}

        /// <summary>
        /// Gets or sets the solution.
        /// </summary>
        /// <value>
        /// The solution.
        /// </value>
        public Solution<Position> Solution
		{
			get
			{
				return solution;
			}

			set
			{
				solution = value;
			}
		}
	}
}