using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
class Program
{
    static void Main(string[] args)
    {
        var temp = new CompareStates<int>();
        ISearcher<int> ser = new BFS<int>(temp);

        Dictionary<int, List<State<int>>> Adj = new Dictionary<int, List<State<int>>>();
        State<int> one = new State<int>(1);
        State<int> two = new State<int>(2);
        State<int> three = new State<int>(3);
        State<int> four = new State<int>(4);
        State<int> five = new State<int>(5);
        State<int> six = new State<int>(6);

        one.SetCost(1);
        two.SetCost(20);
        three.SetCost(3);
        four.SetCost(4);
        five.SetCost(5);
        six.SetCost(6);

        Adj[1] = new List<State<int>> { two, three };
        Adj[2] = new List<State<int>> { four, five };
        Adj[3] = new List<State<int>> { two, six };
        Adj[4] = new List<State<int>>();
        Adj[5] = new List<State<int>> { six };

        TestSearchable<int> test1 = new TestSearchable<int>(one, six, Adj);
        Solution<int> sol = ser.Search(test1);

        printSol(sol);
    }


    static void printSol<T>(Solution<T> s)
    {
        for (int i = s.Count-1; i>=0; i--)
        {
            Console.Write("{0}->",s[i]);
        }
        Console.WriteLine();
        Console.ReadLine();
    }
}

