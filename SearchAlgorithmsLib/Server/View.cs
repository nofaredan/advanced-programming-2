using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    /// <summary>
    /// View Class
    /// </summary>
    /// <seealso cref="Server.IView" />
    class View : IView
    {
        Server server;
        Controller controller;
        /// <summary>
        /// Initializes a new instance of the <see cref="View"/> class.
        /// </summary>
        /// <param name="newController">The new controller.</param>
        public View(Controller newController)
        {
            server = new Server(this);
            server.Start();
            controller = newController;
        }

        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient(TcpClient client)
		{
            // creates new task
			new Task(() =>
			{
				using (NetworkStream stream = client.GetStream())
				using (StreamReader reader = new StreamReader(stream))
				using (StreamWriter writer = new StreamWriter(stream))
				{
                    // read:
					string commandLine = reader.ReadLine();
					Console.WriteLine("Got command: {0}", commandLine);
					ConnectionInfo result = controller.ExecuteCommand(commandLine, client);
                    // write:
					writer.WriteLine(result.Answer);
					writer.Flush();

                    // if needed to close connection:
					if (result.CloseConnection)
					{
						stream.Flush();
						stream.Close();
						client.Close();
						return;
					}
				}
			}).Start();
		}
	}
}