using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	public class CloseCommand : ICommand
	{
		IModel model;
		public CloseCommand(IModel myModel)
		{
			model = myModel;
		}
		public ConnectionInfo Execute(string[] args, TcpClient client = null)
		{
			string name = args[0];

			// here we need to dispose netwrok connection
			throw new NotImplementedException();
		}
	}
}