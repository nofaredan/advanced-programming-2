using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Configuration;

namespace Client
{
    class View
    {
        private static Dictionary<string, ICommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="View"/> class.
        /// </summary>
        public View()
        {
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new CloseConnection());
            commands.Add("solve", new CloseConnection());
            commands.Add("start", new StartCommand());
            commands.Add("play", new StartCommand());
            commands.Add("list", new StartCommand());
            commands.Add("join", new JoinCommand());
            commands.Add("close", new JoinCommand());
        }

        /// <summary>
        /// Connect.
        /// </summary>
        public void Connect()
        {
            string ipAdresss = ConfigurationManager.AppSettings["IP"].ToString();
            string strPort = ConfigurationManager.AppSettings["port"].ToString();
            int port = Int32.Parse(strPort);

            while (true)
            {
                // Send data to server
                string message = Console.ReadLine();

                Task task = new Task(() =>
                {
                    bool connectionAlive = true;
                    TcpClient client = new TcpClient();
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAdresss), port);

                    // comnect:
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

                            // check whats next (close connection or continue)
                            if (commands.ContainsKey(commandKey) )
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
                });
                task.Start();
                task.Wait();

            }
        }
      
    }
}