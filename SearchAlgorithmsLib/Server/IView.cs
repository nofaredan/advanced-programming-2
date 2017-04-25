using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
	public interface IView
	{
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="tcpClient">The TCP client.</param>
        void HandleClient(TcpClient tcpClient);
	}
}