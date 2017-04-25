using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
	public class CompareStates<T> : IComparer<State<T>>
    {
        /// <summary>
        /// Compares the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public int Compare(State<T> x, State<T> y)
        {
            return (int)(x.GetCost() - y.GetCost());
        }
	}
}
