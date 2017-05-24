using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Game information.
    /// </summary>
    public class GameInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether this game started.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is start; otherwise, <c>false</c>.
        /// </value>
        public bool IsStart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this game ended.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is end; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnd { get; set; }

        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<TcpClient> Players { get; set; }
    }
}
