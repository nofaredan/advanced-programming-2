using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        private List<State<T>> open;
        protected int evaluatedNodes;
        IComparer<State<T>> comperator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Searcher{T}"/> class.
        /// </summary>
        /// <param name="newComperator">The new comperator.</param>
        public Searcher(IComparer<State<T>> newComperator)
        {
            evaluatedNodes = 0;
            comperator = newComperator;
            open = new List<State<T>>();
        }

        /// <summary>
        /// Pops from open list.
        /// </summary>
        /// <returns></returns>
        protected State<T> popOpenList()
        {
            // evaluatedNodes++;
            State<T> first = open.First();
            open.Remove(first);
            open.Sort(comperator);
            return first;
        }

        /// <summary>
        /// Gets the size of the open list.
        /// </summary>
        /// <value>
        /// The size of the open list.
        /// </value>
        public int OpenListSize
        {
            get { return open.Count; }
        }

        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        /// <summary>
        /// Return if open list contains the state or not.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public bool openContains(State<T> state)
        {
            return open.Contains(state);
        }

        /// <summary>
        /// Adds object to open list.
        /// </summary>
        /// <param name="state">The state.</param>
        public void addToOpenList(State<T> state)
        {
            open.Add(state);
            open.Sort(comperator);
        }

        /// <summary>
        /// Gets the state from queue.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public State<T> getStateFromQueue(State<T> s)
        {
            foreach (State<T> state in open)
            {
                if (s.Equals(state))
                {
                    return state;
                }
            }
            return null;
        }

        /// <summary>
        /// Pops the state from queue.
        /// </summary>
        /// <param name="state">The state.</param>
        public void popStateFromQueue(State<T> state)
        {
            List<State<T>> temp = new List<State<T>>();
            foreach (State<T> s in open)
            {
                if (state != s)
                {
                    temp.Add(s);
                }
            }

            open = temp;
            open.Sort(comperator);
        }

        /// <summary>
        /// Back trace.
        /// </summary>
        /// <param name="goal">The goal.</param>
        /// <returns></returns>
        protected Solution<T> backTrace(State<T> goal)
        {
            Solution<T> solution = new Solution<T>();
            State<T> currentState = goal;
            while (currentState != null)
            {
                solution.Add(currentState);
                currentState = currentState.GetCameFrom();
            }

            return solution;
        }

        /// <summary>
        /// Searches on the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);
    }
}
