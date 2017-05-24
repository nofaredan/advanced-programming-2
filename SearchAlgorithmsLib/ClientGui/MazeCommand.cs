using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    public class MazeCommand : EventArgs
    {
        public string CommandType { get; set; }
        public string[] args { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeCommand"/> class.
        /// </summary>
        /// <param name="_type">The type.</param>
        /// <param name="_args">The arguments.</param>
        public MazeCommand(string _type, string[] _args)
        {
            CommandType = _type;
            args = _args;
        }
    }
}
