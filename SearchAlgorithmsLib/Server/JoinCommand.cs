using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class JoinCommand : ICommand
    {
        IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinCommand"/> class.
        /// </summary>
        /// <param name="myModel">My model.</param>
        public JoinCommand(IModel myModel)
        {
            model = myModel;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public ConnectionInfo Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.CloseConnection = true;
            connectionInfo.Answer = model.JoinGame(name, client);
          
			return connectionInfo;
		}
	}
}