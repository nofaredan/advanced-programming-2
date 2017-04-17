using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class GameInfo
    {
        public bool isStart { get; set; }
        public bool isEnd { get; set; }
        public Maze maze { get; set; }
        public List<TcpClient> players { get; set; }
    }
}
