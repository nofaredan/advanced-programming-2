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
        public State<T> this [int idx] {
            get
            {
                return path[idx];
            }
        }

        public void Add(State<T> value)
        {
            path.Add(value);
        }

        // a property of openList
        public int Count
        {
            get { return path.Count; }
        }


     
    }
}
