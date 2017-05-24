using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Controller.
    /// </summary>
    public class Controller
	{
		private Dictionary<string, ICommand> commands;
		private IModel model;
		private IView view;

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
		{
			model = new Model(this);
			view = new View(this);

			commands = new Dictionary<string, ICommand>();
			commands.Add("generate", new GenerateMazeCommand(model));
			commands.Add("solve", new SolveMazeCommand(model));
			commands.Add("start", new StartMazeCommand(model));
			commands.Add("list", new ListCommand(model));
			commands.Add("join", new JoinCommand(model));
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public ConnectionInfo ExecuteCommand(string commandLine, TcpClient client)
		{
			string[] arr = commandLine.Split(' ');
			string commandKey = arr[0];

            // if the commands don't contain the command key
			if (!commands.ContainsKey(commandKey))
			{
				ConnectionInfo connectionInfo = new ConnectionInfo();
				connectionInfo.Answer = "invalid command";
				connectionInfo.CloseConnection = false;
				return connectionInfo;
			}

			string[] args = arr.Skip(1).ToArray();
			ICommand command = commands[commandKey];
			return command.Execute(args, client);
		}
	}
}