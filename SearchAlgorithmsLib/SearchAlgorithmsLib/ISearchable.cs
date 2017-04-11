using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        State<T> getInitialState();
        State<T> getIGoallState();
        List<State<T>> getAllPossibleStates(State<T> state);
    }
}
