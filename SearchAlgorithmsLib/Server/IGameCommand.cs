using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IGameCommand
    {
        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="name">The name.</param>
        /// <param name="currentPlayer">The current player.</param>
        void Execute(string[] args, string name, TcpClient currentPlayer = null);
    }
}