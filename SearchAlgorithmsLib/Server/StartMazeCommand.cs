using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	public class StartMazeCommand : ICommand
	{
		IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartMazeCommand"/> class.
        /// </summary>
        /// <param name="newModel">The new model.</param>
        public StartMazeCommand(IModel newModel)
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
            ConnectionInfo connectionInfo = new ConnectionInfo();
            // NO ANSWER HERE - WAIT TO ANOTHER PLAYER
            connectionInfo.CloseConnection = true;
            connectionInfo.Answer = "";
            bool isAnswerNeeded = true;

            if (args[0] != "" && args[1] != "" && args[2] != "")
            {
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);

                // create new maze
                isAnswerNeeded = model.StartGame(name, rows, cols, client);
            }


            if (isAnswerNeeded)
			{
				// exist
				connectionInfo.Answer = "invalid command";
			}

			return connectionInfo;
		}
	}
}