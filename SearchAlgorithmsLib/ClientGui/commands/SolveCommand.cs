using System;
using System.IO;
using System.Net.Sockets;

namespace ClientGui
{
    internal class SolveCommand : ICommand
    {
        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="model"></param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public RecieveInfo Execute(string[] args, IServerModel model, TcpClient client = null)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            string result = SendAndRecieve.RecieveInfo(reader);
            client.GetStream().Flush();
            client.GetStream().Close();
            client.Close();

            return new RecieveInfo(result, false, "solve");
        }
    }
}