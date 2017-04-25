using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server
{
	public class ListCommand : ICommand
	{
		IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCommand"/> class.
        /// </summary>
        /// <param name="newModel">The new model.</param>
        public ListCommand(IModel newModel)
		{
			model = newModel;
		}

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public ConnectionInfo Execute(string[] args, TcpClient client = null)
		{
			Dictionary<string, MazeGame> list = model.ShowList();
			ConnectionInfo connectionInfo = new ConnectionInfo();
			connectionInfo.Answer = JsonConvert.SerializeObject(list.Keys.ToArray());
			connectionInfo.CloseConnection = false;
			return connectionInfo;
		}
	}
}