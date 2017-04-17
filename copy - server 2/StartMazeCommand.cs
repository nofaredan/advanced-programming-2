using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class StartMazeCommand: ICommand
    {
        IModel model;
        public StartMazeCommand(IModel newModel)
        {
            model = newModel;
        }

        public ConnectionInfo Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            // create new maze
            model.StartGame(name, rows, cols, client);

            ConnectionInfo connectionInfo = new ConnectionInfo();
           // *** NO ANSWER HERE - WAIT TO ANOTHER PLAYER ****
            connectionInfo.CloseConnection = false;
            return connectionInfo;
        }
    }
}
