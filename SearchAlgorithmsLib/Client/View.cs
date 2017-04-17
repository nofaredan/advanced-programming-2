using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Client
{
    class View
    {
        private static Dictionary<string, ICommand> commands;

        public View()
        {
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new CloseConnection());
            commands.Add("solve", new CloseConnection());
            commands.Add("start", new StartCommand());
            commands.Add("join", new JoinCommand());
        }

        public void Connect()
        {
            while (true)
            {
                // Send data to server
                string message = Console.ReadLine();

                new Task(() =>
                {

                    bool connectionAlive = true;
                    TcpClient client = new TcpClient();
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);

                    client.Connect(ep);

                    using (NetworkStream stream = client.GetStream())
                    using (StreamReader reader = new StreamReader(stream))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        while (connectionAlive)
                        {
                            string[] arr = message.Split(' ');
                            string commandKey = arr[0];

                            SendAndRecieve.Send(writer, message);

                            bool isValid = SendAndRecieve.RecieveInfo(reader);

                            // check whats next (close connection or continue)
                            if (commands.ContainsKey(commandKey) && isValid)
                            {
                                string[] arguments = arr.Skip(1).ToArray();
                                ICommand command = commands[commandKey];
                                connectionAlive = command.Execute(arguments, client);

                                if (connectionAlive)
                                {
                                    // NEED other input
                                    message = Console.ReadLine();
                                }
                            }
                        }
                    }
                    client.Close();
                }).Start();
            }
        }
      
    }
}
