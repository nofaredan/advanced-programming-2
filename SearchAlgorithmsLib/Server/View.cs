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
            server = new Server(5555, this);
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
                   // while (true)
                    //{
                        //Console.WriteLine("1");
                        string commandLine = reader.ReadLine();
                        //Console.WriteLine("2");
                        Console.WriteLine("Got command: {0}", commandLine);
                        ConnectionInfo result = controller.ExecuteCommand(commandLine, client);
                        writer.WriteLine(result.Answer);
                        writer.Flush();

                        if (result.CloseConnection)
                        {
                            stream.Flush();
                            stream.Close();
                            client.Close();
                            return;
                        }
                   // }
                }
              //  client.Close();
            }).Start();
        }
    }
}
