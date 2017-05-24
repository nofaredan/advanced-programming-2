using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;

namespace Server
{
	public class GenerateMazeCommand : ICommand
	{
		private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateMazeCommand(IModel model)
		{
			this.model = model;
		}

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public ConnectionInfo Execute(string[] args, TcpClient client)
		{
            Maze maze = null;
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.CloseConnection = true;

            if (args[0]!="" && args[1] != "" && args[2] != "")
            {
                // maze information:
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
                maze = model.GenerateMaze(name, rows, cols);
            }
            // if the maze already exist
			if (maze == null)
			{
				connectionInfo.Answer = "invalid command";
			}
			else
			{
				connectionInfo.Answer = maze.ToJSON();
			}
			return connectionInfo;
		}
	}
}