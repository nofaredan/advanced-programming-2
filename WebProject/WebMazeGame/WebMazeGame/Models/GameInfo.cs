using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMazeGame.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GameInfo
    {
        public List<string> Players = new List<string>();
        public int Rows { get; set; }
        public int Cols { get; set; }
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

        public string GetOpponent(string id)
        {
            if (Players[0] == id)
            {
                return Players[1];
            }
            else if (Players[1] == id)
            {
                return Players[0];
            }

            return null;
        }
    }

}