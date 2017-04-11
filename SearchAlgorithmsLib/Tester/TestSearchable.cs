using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

public class TestSearchable<T> : ISearchable<T>
{
    private State<T> from, to;
    Dictionary<T, List<State<T>>> Adj;

    public TestSearchable(State<T> from, State<T> to, Dictionary<T, List<State<T>>> Adj)
    {
        this.from = from;
        this.to = to;
        this.Adj = Adj;
    }

    public State<T> getInitialState()
    {
        return from;
    }

    public State<T> getIGoallState()
    {
        return to;
    }

    public List<State<T>> getAllPossibleStates(State<T> state)
    {
        List<State<T>> states = new List<State<T>>();
        State<T> tempState;
        // if found the vertex
        if (Adj.ContainsKey(state.GetState()))
        {
            foreach (State<T> s in Adj[state.GetState()])
            {   
                tempState = State<T>.StatePool.getState(s.GetState(), s.GetCost());
             //   tempState = new State<T>(s);
               // tempState.SetCost(s.GetCost());
                states.Add(tempState);
            }
        }

        // if vertex does not exist or no neighbors found
        if (states == null)
        {
            states = new List<State<T>>();
        }
        return states;
    }


}