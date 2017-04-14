using System;
using System.Net.Sockets;

namespace Client
{
	public class CloseConnection : ICommand
	{
		public CloseConnection()
		{
		}

		public bool Execute(string[] args, TcpClient client = null)
		{
			client.GetStream().Flush();
			client.GetStream().Close();
			client.Close();

			return false;
		}
	}
}
