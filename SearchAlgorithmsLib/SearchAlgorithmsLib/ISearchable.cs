using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        State<T> getInitialState();

        /// <summary>
        /// Gets the state of the i goall.
        /// </summary>
        /// <returns></returns>
        State<T> getIGoallState();

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        List<State<T>> getAllPossibleStates(State<T> state);
    }
}
