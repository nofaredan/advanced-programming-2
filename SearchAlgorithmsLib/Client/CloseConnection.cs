using System;
using System.IO;
using System.Net.Sockets;

namespace Client
{
	public class CloseConnection : ICommand
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseConnection"/> class.
        /// </summary>
        public CloseConnection()
		{
		}

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
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