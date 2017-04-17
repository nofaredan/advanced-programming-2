using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server
{
    public class SolveInfo
    {
        int nodesEvaluated;
        Solution<Position> solution;

        public SolveInfo(int nEvaluated, Solution<Position> mySolution)
        {
            nodesEvaluated = nEvaluated;
            solution = mySolution;
        }
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
