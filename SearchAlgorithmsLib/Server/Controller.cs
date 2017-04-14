using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Controller
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        private IView view;

        public Controller()
        {
            model = new Model(this);
            view = new View(this);

            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            // more commands...
        }
		public ConnectionInfo ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
			if (!commands.ContainsKey(commandKey))
			{
				ConnectionInfo connectionInfo = new ConnectionInfo();
				connectionInfo.Answer = "Command not found";
				connectionInfo.CloseConnection = false;
				return connectionInfo;
			}
				
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
    }
}
