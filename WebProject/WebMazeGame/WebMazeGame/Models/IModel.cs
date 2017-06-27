using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;
using WebMazeGame.Models;

namespace WebMazeGame
{
    /// <summary>
    /// IModel
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        Maze GenerateMaze(string name, int rows, int cols);

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="typeAlgo">The type algo.</param>
        /// <returns></returns>
        SolveInfo SolveMaze(string name, string typeAlgo);

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        /*bool StartGame(string name, int rows, int cols, TcpClient client);
        */
        /// <summary>
        /// Shows the list.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, MultiMaze> ShowList();
        /*
        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        string JoinGame(string name, TcpClient client);
        */
    }
}