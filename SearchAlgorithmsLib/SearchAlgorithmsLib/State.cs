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

        /// <summary>
        /// Initializes a new instance of the <see cref="State{T}"/> class.
        /// </summary>
        /// <param name="newState">The new state.</param>
        /// <param name="newCost">The new cost.</param>
        public State(T newState, double newCost)
        {
            state = newState;
            cost = newCost;
        }

        /// <summary>
        /// Gets the came from.
        /// </summary>
        /// <returns></returns>
        public State<T> GetCameFrom()
        {
            return cameFrom;
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <returns></returns>
        public T GetState()
        {
            return state;
        }

        /// <summary>
        /// Sets the cost.
        /// </summary>
        /// <param name="newCost">The new cost.</param>
        public void SetCost(double newCost)
        {
            cost = newCost;
        }

        /// <summary>
        /// Sets the came from.
        /// </summary>
        /// <param name="state">The state.</param>
        public void SetCameFrom(State<T> state)
        {
            cameFrom = state;
        }

        /// <summary>
        /// Gets the cost.
        /// </summary>
        /// <returns></returns>
        public double GetCost()
        {
            return cost;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="State{T}"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public State(T state)
        {
            this.state = state;
        }

        /// <summary>
        /// Equalses the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public override bool Equals(Object s)
        {
            return this.GetHashCode().Equals(((State<T>)s).GetHashCode());
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return state.ToString().GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return state.ToString();
        }

        /// <summary>
        /// State Pool Class.
        /// </summary>
        public class StatePool
        {
            private static Dictionary<int, State<T>> dictionary = new Dictionary<int, State<T>>();
            /// <summary>
            /// Gets the state.
            /// </summary>
            /// <param name="stateKey">The state key.</param>
            /// <param name="cost">The cost.</param>
            /// <returns></returns>
            public static State<T> getState(T stateKey, double cost)
            {
                if (!dictionary.ContainsKey(stateKey.ToString().GetHashCode()))
                {
                    dictionary.Add(stateKey.ToString().GetHashCode(), new State<T>(stateKey, cost));

                }
                return dictionary[(stateKey.ToString().GetHashCode())];
            }

            /// <summary>
            /// Initializes the dictionary.
            /// </summary>
            public static void initDictionary()
            {
                dictionary.Clear();
            }
        }
    }
}
