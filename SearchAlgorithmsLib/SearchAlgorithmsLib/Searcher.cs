using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T>: ISearcher<T>
    {
	private List<State<T>> open;
	protected int evaluatedNodes;
        IComparer<State<T>> comperator;

        public Searcher(IComparer<State<T>> newComperator)
        {
            evaluatedNodes = 0;
            comperator = newComperator;
			open = new List<State<T>>();
        }
        protected State<T> popOpenList()
        {
           // evaluatedNodes++;
            State<T> first = open.First();
            open.Remove(first);
			open.Sort(comperator);
            return first;
        }

        // a property of openList
       public int OpenListSize
        {
            get { return open.Count; }
        }

        // ISearcher’s methods:
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        public bool openContains(State<T> state)
        {
            return open.Contains(state);
        }

        public void addToOpenList(State<T> state)
        {
            open.Add(state);
			open.Sort(comperator);
        }

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

        public void popStateFromQueue(State<T> state)
        {
			List<State<T>> temp = new List<State<T>>();
            foreach (State<T> s in open)
            {
                if (state != s)
                {
                    temp.Add (s);
                }
            }
              
            open = temp; 
			open.Sort(comperator);
          //  evaluatedNodes--;
        }

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

        public abstract Solution<T> Search(ISearchable<T> searchable);
    }
}
