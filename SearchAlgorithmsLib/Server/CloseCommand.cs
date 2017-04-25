using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Close Command
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class CloseCommand : ICommand
	{
		IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseCommand"/> class.
        /// </summary>
        /// <param name="myModel">My model.</param>
        public CloseCommand(IModel myModel)
		{
			model = myModel;
		}

        /// <summary>
        /// Execute function.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ConnectionInfo Execute(string[] args, TcpClient client = null)
		{
			string name = args[0];
            return null;
		}
	}
}