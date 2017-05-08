using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace ClientGui
{
    public class Model : IServerModel
    {
        private string message = "";
        private static Dictionary<string, ICommand> commands;
        public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
        public event PropertyChangedEventHandler PropertyChanged;
        public event System.EventHandler<Recive> reciveFromServer;

        public Model()
        {
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateCommand());
            /*commands.Add("solve", new CloseConnection());
            commands.Add("start", new StartCommand());
            commands.Add("play", new StartCommand());
            commands.Add("list", new StartCommand());
            commands.Add("join", new JoinCommand());
            commands.Add("close", new JoinCommand());*/

            ServerIP = Properties.Settings.Default.ServerIP;
            ServerPort = Properties.Settings.Default.ServerPort;
            MazeRows = Properties.Settings.Default.MazeRows;
            MazeCols = Properties.Settings.Default.MazeCols;
            SearchAlgorithm = Properties.Settings.Default.SearchAlgorithm;
        }

        public bool connectClient()
        {
            while (true)
            {

                while (message.CompareTo("") == 0)
                {

                }

                Task task = new Task(() =>
                {
                    bool connectionAlive = true;
                    TcpClient client = new TcpClient();
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ServerIP), ServerPort);

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

                            message = "";

                            // check whats next (close connection or continue)
                            if (commands.ContainsKey(commandKey))
                            {
                                string[] arguments = arr.Skip(1).ToArray();
                                ICommand command = commands[commandKey];
                                RecieveInfo recieveInfo = command.Execute(arguments, client);
                                connectionAlive = recieveInfo.connectionAlive;

                                Task viewTask = new Task(() => {
                                    reciveFromServer.Invoke(this, new Recive(recieveInfo.typeCommand, recieveInfo.result));
                                });
                                viewTask.Start();

                                if (connectionAlive)
                                {
                                    // NEED other input
                                    while (message.CompareTo("") == 0)
                                    {

                                    }

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

        public void Send(string message)
        {
            this.message = message;
        }

        public void Recieve()
        {
            throw new NotImplementedException();
        }




        //      SETTINGS      //

        public string ServerIP { get; set; }
        public int ServerPort { get; set; }
        public int MazeRows { get; set; }
        public int MazeCols { get; set; }
        public int SearchAlgorithm { get; set; }
       
        // OK
        public void SaveSettings()
        {
            Properties.Settings.Default.ServerIP = ServerIP;
            Properties.Settings.Default.ServerPort = ServerPort;
            Properties.Settings.Default.MazeRows = MazeRows;
            Properties.Settings.Default.MazeCols = MazeCols;
            Properties.Settings.Default.SearchAlgorithm = SearchAlgorithm;

            //Properties.Settings.Default.Save();
        }

        public void CancelSettings()
        {
            ServerIP = Properties.Settings.Default.ServerIP;
            ServerPort = Properties.Settings.Default.ServerPort;
            MazeRows = Properties.Settings.Default.MazeRows;
            MazeCols = Properties.Settings.Default.MazeCols;
            SearchAlgorithm = Properties.Settings.Default.SearchAlgorithm;

        }

    }
}
