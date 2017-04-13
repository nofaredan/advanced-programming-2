using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
	public class State<T>
    {
        private T state;
        private double cost;
        private State<T> cameFrom = null;

        public State(T newState, double newCost)
        {
            state = newState;
            cost = newCost;
        }


        public State<T> GetCameFrom()
        {
            return cameFrom;
        }

        public T GetState()
        {
            return state;
        }

        public void SetCost(double newCost)
        {
            cost = newCost;
        }

        public void SetCameFrom(State<T> state)
        {
            cameFrom = state;
        }

        public double GetCost()
        {
            return cost;
        }

        public State(T state)
        {
            this.state = state;
        }

       public override bool Equals(Object s)
        {
			return this.GetHashCode().Equals(((State<T>)s).GetHashCode());
        }

		public override int GetHashCode()
		{
			return state.ToString().GetHashCode();
		}

		public override string ToString()
		{
			return state.ToString();
		}

		public class StatePool
        {
            private static Dictionary<int, State<T>> dictionary = new Dictionary<int, State<T>>();
            public static State<T> getState(T stateKey, double cost)
            {
                if (!dictionary.ContainsKey(stateKey.ToString().GetHashCode()))
                {
                    dictionary.Add(stateKey.ToString().GetHashCode(), new State<T>(stateKey, cost));

                }
                return dictionary[(stateKey.ToString().GetHashCode())];
            }
        }
    } 
}
