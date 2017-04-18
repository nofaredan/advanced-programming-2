using System;
using System.IO;
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
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            SendAndRecieve.RecieveInfo(reader);
            client.GetStream().Flush();
			client.GetStream().Close();
			client.Close();

			return false;
		}
	}
}
