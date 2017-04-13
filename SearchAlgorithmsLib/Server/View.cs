using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    class View: IView
    {
        Server server;
        Controller controller;
        public View(Controller newController)
        {
            server = new Server(6565, this);
            server.Start();
            controller = newController;
        }

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                  //  Console.WriteLine("1");
                    string commandLine = reader.ReadLine();
                  //  Console.WriteLine("2");
                      Console.WriteLine("Got command: {0}", commandLine);
                    string result = controller.ExecuteCommand(commandLine, client);
                    Console.WriteLine("3");
                    writer.Write(result);
                }
                client.Close();
            }).Start();
        }
    }
}
