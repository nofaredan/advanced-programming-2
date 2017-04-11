using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
	public class CompareStates<T> : IComparer<State<T>>
    {
        public int Compare(State<T> x, State<T> y)
        {
            return (int)(x.GetCost() - y.GetCost());
        }
	}
}
