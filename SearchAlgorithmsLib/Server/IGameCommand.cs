using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
<<<<<<< HEAD
    interface IGameCommand
    {
        void Execute(string[] args, string name, TcpClient currentPlayer = null);
    }
}
=======
	interface IGameCommand
	{
		ConnectionInfo Execute(string[] args, string name, TcpClient currentPlayer = null);
	}
}
>>>>>>> ed4fabfa7ebd57e25b740162d6dc41f2519e9a48
