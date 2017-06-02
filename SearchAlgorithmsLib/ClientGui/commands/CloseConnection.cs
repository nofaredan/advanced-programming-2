using System;
using System.IO;
using System.Net.Sockets;

namespace ClientGui
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
        public RecieveInfo Execute(string[] args, IServerModel model, TcpClient client = null)
		{
         /*   NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            string result = SendAndRecieve.RecieveInfo(reader);
            client.GetStream().Flush();
			client.GetStream().Close();
			client.Close();
            */
			return new RecieveInfo("close", false, "close");
		}
       
    }
}