using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : Searcher<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BFS{T}"/> class.
        /// </summary>
        /// <param name="newComperator">The new comperator.</param>
        public BFS(IComparer<State<T>> newComperator) : base(newComperator)
        {
        }

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            AddToOpenList(searchable.GetInitialState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> n = PopOpenList();
                closed.Add(n);
                if (n.Equals(searchable.GetIGoallState()))
                    return BackTrace(n);
                List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    evaluatedNodes++;
                    if (!closed.Contains(s) && !OpenContains(s))
                    {
                        s.SetCost(s.GetCost() + n.GetCost());
                        s.SetCameFrom(n);
                        AddToOpenList(s);
                    }
                    else
                    {
                        State<T> currentFromQueue = null;
                        if (OpenContains(s))
                        {
                            currentFromQueue = GetStateFromQueue(s);
                            if (s.GetCost() + n.GetCost() < currentFromQueue.GetCost())
                            {
                                s.SetCost(s.GetCost() + n.GetCost());
                                PopStateFromQueue(currentFromQueue);
                                s.SetCameFrom(n);
                                AddToOpenList(s);
                            }
                        }
                    }
                }
            }

            return null;
        }

    }
}

