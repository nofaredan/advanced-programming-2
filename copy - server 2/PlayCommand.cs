using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class PlayCommand : ICommand
    {
        IModel model;
        public PlayCommand(IModel myModel)
        {
            model = myModel;
        }
        public ConnectionInfo Execute(string[] args, TcpClient client = null)
        {
            string move = args[0];

            throw new NotImplementedException();
        }
    }
}
