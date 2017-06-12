using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace WebMazeGame
{
    public class MazeAdapter : ISearchable<Position>
    {
        private Maze maze;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeAdapter"/> class.
        /// </summary>
        /// <param name="myMaze">My maze.</param>
        public MazeAdapter(Maze myMaze)
        {
            maze = myMaze;
            State<Position>.StatePool.InitDictionary();
        }

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> state)
        {
            State<Position> tempState;
            Position position = new Position();
            int newRow, newCol;
            List<State<Position>> list = new List<State<Position>>();

            // direction matrix:
            int[,] arr = new int[4, 2] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

            // go over the direction matrix and add states (neighbors) to the list:
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                newRow = state.GetState().Row + arr[i, 0];
                newCol = state.GetState().Col + arr[i, 1];
                if (newRow >= 0 && newRow < maze.Rows && newCol >= 0 && newCol < maze.Cols &&
                    maze[newRow, newCol] == MazeLib.CellType.Free)
                {
                    position.Row = state.GetState().Row + arr[i, 0];
                    position.Col = state.GetState().Col + arr[i, 1];
                    tempState = State<Position>.StatePool.GetState(position, 1);
                    list.Add(tempState);
                }
            }
            // if the list is null, return an empty one.
            if (list == null)
            {
                list = new List<State<Position>>();
            }
            return list;
        }

        /// <summary>
        /// Gets the state of the i goall.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetIGoallState()
        {
            return State<Position>.StatePool.GetState(maze.GoalPos, 1);
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetInitialState()
        {
            return State<Position>.StatePool.GetState(maze.InitialPos, 1);
        }
    }
}
