using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
<<<<<<< HEAD
    public class GameInfo
    {
        public bool isStart { get; set; }
        public bool isEnd { get; set; }
        public Maze maze { get; set; }
        public string name { get; set; }
        public List<TcpClient> players { get; set; }
    }
}
=======
	public class GameInfo
	{
		public bool isStart { get; set; }
		public bool isEnd { get; set; }
		public Maze maze { get; set; }
		public List<TcpClient> players { get; set; }
	}
}
>>>>>>> ed4fabfa7ebd57e25b740162d6dc41f2519e9a48
