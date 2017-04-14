using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
		private static Dictionary<string, ICommand> commands;

        static void Main(string[] args)
        {
			commands = new Dictionary<string, ICommand>();
			commands.Add("generate", new CloseConnection());

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
						// Send data to server
						string message = Console.ReadLine();

						string[] arr = message.Split(' ');
						string commandKey = arr[0];
						
						// send message
						writer.WriteLine(message);
	                    writer.Flush();
	                    // Get result from server
						string result = reader.ReadToEnd();
	                    Console.WriteLine("Result = {0}", result);


						// check whats next (close connection or continue)
						if (commands.ContainsKey(commandKey))
						{
							string[] arguments = arr.Skip(1).ToArray();
							ICommand command = commands[commandKey];
							connectionAlive = command.Execute(arguments, client);
						}
                	}
               }
            client.Close();
        }
    }
}
