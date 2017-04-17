using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class JoinCommand : ICommand
    {
        public bool Execute(string[] args, TcpClient client = null)
        {
            Game.server = client;
            Game.StartGame();
            // connection alive
            return true;
        }
    }
}
