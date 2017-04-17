using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server
{
    public class ListCommand : ICommand
    {
        IModel model;
        public ListCommand(IModel newModel)
        {
            model = newModel;
        }

        public ConnectionInfo Execute(string[] args, TcpClient client = null)
        {
            Dictionary<string, MazeGame> list = model.ShowList();
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.Answer = JsonConvert.SerializeObject(list.Keys.ToArray());
            connectionInfo.CloseConnection = false;
            return connectionInfo;
        }
    }
}
