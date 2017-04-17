using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;

namespace Server
{
    public interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);
        SolveInfo SolveMaze(string name, string typeAlgo);
        bool StartGame(string name, int rows, int cols, TcpClient client);
        Dictionary<string, MazeGame> ShowList();
        Maze JoinGame(string name, TcpClient client);
    }
}
