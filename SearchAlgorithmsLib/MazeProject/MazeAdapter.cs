using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace MazeProject
{
    // an adapter from maze to searchable
    public class MazeAdapter : ISearchable<Position>
    {
        private Maze maze;
        public MazeAdapter(Maze myMaze)
        {
            maze = myMaze;
        }
        public List<State<Position>> getAllPossibleStates(State<Position> state)
        {
            State<Position> tempState;
            Position position = new Position();
            int newRow, newCol;
            List<State<Position>> list = new List<State<Position>>();
            int[,] arr = new int[4, 2] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

            for(int i=0; i<arr.GetLength(0); i++)
            {
                newRow = state.GetState().Row + arr[i, 0];
                newCol = state.GetState().Col + arr[i, 1];
                if (newRow >=0 && newRow<maze.Rows && newCol>=0 && newCol< maze.Cols &&
                    maze[newRow,newCol] == MazeLib.CellType.Free)
                {
                    position.Row = state.GetState().Row + arr[i, 0];
                    position.Col = state.GetState().Col + arr[i, 1];
                   // s = new State<Position>(position);
                   // s.SetCost(1);
                    tempState = State<Position>.StatePool.getState(position, 1);
                    list.Add(tempState);
                }
            }

            if (list == null)
            {
                list = new List<State<Position>>();
            }
            return list;
        }

        public State<Position> getIGoallState()
        {
			return State<Position>.StatePool.getState(maze.GoalPos, 1);
        }

        public State<Position> getInitialState()
        {
			return State<Position>.StatePool.getState(maze.InitialPos, 1);
        }
    }
}
