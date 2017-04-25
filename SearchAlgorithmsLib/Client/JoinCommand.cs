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
        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public bool Execute(string[] args, TcpClient client = null)
		{
			Game.server = client;
			Game.StartGame();
			// connection alive
			return true;
		}
	}
}