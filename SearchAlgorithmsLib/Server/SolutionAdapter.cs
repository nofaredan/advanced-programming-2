using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server
{
	public class SolutionAdapter
	{
		private Solution<Position> solution;

		public SolutionAdapter(Solution<Position> newSolution)
		{
			solution = newSolution;
		}

		public string CreateString()
		{
			if (solution.Count == 0)
			{
				return "";
			}

			string result = "";

			State<Position> s = solution[solution.Count - 1];
			//int index = 1;
			Position firstPos = s.GetState();
			Position secondPos;

			for (int i = solution.Count - 2 ; i >= 0 ; i--)
			{
				secondPos = solution[i].GetState();
				if (secondPos.Row > firstPos.Row)
				{
					// DOWN
					result = result + "3";
				}
				else if (secondPos.Row < firstPos.Row)
				{
					// UP
					result = result + "2";
				}
				else if (secondPos.Col > firstPos.Col)
				{
					// RIGHT
					result = result + "1";
				}
				else
				{
					// LEFT
					result = result + "0";
				}
				firstPos = secondPos;
			}

			/*while (index < solution.Count)
			{
				secondPos = solution[index].GetState();
				if (secondPos.Row > firstPos.Row)
				{
					// DOWN
					result = result + "3";
				}
				else if (secondPos.Row < firstPos.Row)
				{
					// UP
					result = result + "2";
				}
				else if (secondPos.Col > firstPos.Col)
				{
					// RIGHT
					result = result + "1";
				}
				else
				{
					// LEFT
					result = result + "0";
				}
				firstPos = secondPos;
				index++;
			}*/
			return result;
		}
	}
}