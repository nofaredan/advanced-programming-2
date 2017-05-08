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
        public MazeCommand(string _type, string[] _args)
        {
            CommandType = _type;
            args = _args;
        }
    }
}
