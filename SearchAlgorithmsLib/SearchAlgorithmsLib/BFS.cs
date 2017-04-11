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
        public BFS(IComparer<State<T>> newComperator):base(newComperator)
        {
        }

        public override Solution<T> Search(ISearchable<T> searchable)
        {
            addToOpenList(searchable.getInitialState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> n = popOpenList();
                closed.Add(n);
                if (n.Equals(searchable.getIGoallState()))
                    return backTrace(n);
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !openContains(s))
                    {
                        s.SetCost(s.GetCost()+n.GetCost());
                         s.SetCameFrom(n);
                        addToOpenList(s);
                    }
                    else
                    {
                        State<T> currentFromQueue = null;
                        if (openContains(s))
                        {
                            currentFromQueue = getStateFromQueue(s);
                            if (s.GetCost() + n.GetCost() < currentFromQueue.GetCost())
                            {
                                s.SetCost(s.GetCost() + n.GetCost());
                                popStateFromQueue(currentFromQueue);
                                s.SetCameFrom(n);
                                addToOpenList(s);
                            }
                        }
                    }
                }
            }
            
            return null;
        }

    }
}

