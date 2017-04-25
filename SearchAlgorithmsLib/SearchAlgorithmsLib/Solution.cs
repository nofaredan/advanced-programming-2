using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        private List<State<T>> path = new List<State<T>>();

        /// <summary>
        /// Gets the <see cref="State{T}"/> with the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="State{T}"/>.
        /// </value>
        /// <param name="idx">The index.</param>
        /// <returns></returns>
        public State<T> this [int idx] {
            get
            {
                return path[idx];
            }
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(State<T> value)
        {
            path.Add(value);
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get { return path.Count; }
        }


     
    }
}
