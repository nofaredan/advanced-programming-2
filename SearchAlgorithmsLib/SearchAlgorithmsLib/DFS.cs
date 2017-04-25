using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class DFS<T> : Searcher<T>
    {
        Stack<State<T>> states = new Stack<State<T>>();
        List<State<T>> greys = new List<State<T>>();
        List<State<T>> blacks = new List<State<T>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DFS{T}"/> class.
        /// </summary>
        /// <param name="newComperator">The new comperator.</param>
        public DFS(IComparer<State<T>> newComperator) : base(newComperator)
        {
        }

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            State<T> temp;
            states.Push(searchable.getInitialState());
            // while stack is not empty:
            while (states.Count != 0)
            {
                temp = states.Pop();
                greys.Add(temp);
                if (temp.Equals(searchable.getIGoallState()))
                {
                    return backTrace(temp);
                }
                List<State<T>> succerssors = searchable.getAllPossibleStates(temp);
                foreach (State<T> s in succerssors)
                {
                    evaluatedNodes++;
                    // if s is white
                    if (!greys.Contains(s) && !blacks.Contains(s))
                    {
                        s.SetCameFrom(temp);
                        states.Push(s);
                    }
                }
                ChangeColor(temp, greys, blacks);
            }
            return null;
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="original">The original.</param>
        /// <param name="changeTo">The change to.</param>
        private void ChangeColor(State<T> state, List<State<T>> original, List<State<T>> changeTo)
        {
            if (original.Contains(state))
            {
                original.Remove(state);
            }
            changeTo.Add(state);
        }

    }
}
