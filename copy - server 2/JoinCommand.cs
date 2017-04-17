using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class JoinCommand : ICommand
    {
        IModel model;
        public JoinCommand(IModel myModel)
        {
            model = myModel;
        }
        public ConnectionInfo Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            Maze maze = model.JoinGame(name, client);
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.Answer = maze.ToJSON();
            connectionInfo.CloseConnection = false;

            return connectionInfo;
        }
    }
}
