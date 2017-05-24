using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Windows.Input;

namespace ClientGui
{
    public class Model : IServerModel
    {
        private string message = "";
        private static Dictionary<string, ICommand> commands;
        private static Dictionary<string, ICommand> updateCommands;

        public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
        public event PropertyChangedEventHandler PropertyChanged;
        public event System.EventHandler<Recive> reciveFromServer;
        public event System.EventHandler<Recive> updateFromServer;
        public event System.EventHandler<Recive> connection;
        public event System.EventHandler<Recive> initializeEvent;

        private int mazeRows;
        private int mazeCols;
        private int currentRow;
        private int currentCol;
        private int otherCurrentRow;
        private int otherCurrentCol;
        private List<string> multiplayerList;
        private string gameName;
        public Maze Maze { get; set; }
        private bool connectionAlive;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        public Model()
        {
            multiplayerList = new List<string>();
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateCommand());
            commands.Add("start", new StartCommand());
            commands.Add("join", new JoinCommand());

            updateCommands = new Dictionary<string, ICommand>();
            updateCommands.Add("solve", new SolveCommand());
            updateCommands.Add("list", new SendMultiPlayerListCommand());

            ServerIP = Properties.Settings.Default.ServerIP;
            ServerPort = Properties.Settings.Default.ServerPort;
            MazeRows = Properties.Settings.Default.MazeRows;
            MazeCols = Properties.Settings.Default.MazeCols;
            SearchAlgorithm = Properties.Settings.Default.SearchAlgorithm;
        }

        /// <summary>
        /// Connects the client.
        /// </summary>
        public void ConnectClient()
        {
            bool isConected = true;
            while (isConected)
            {
                // wait for a message
                while (message.CompareTo("") == 0)
                {

                }

                Task task = new Task(() =>
                {
                    connectionAlive = true;
                    TcpClient client = new TcpClient();
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ServerIP), ServerPort);

                    // comnect:
                    try
                    {
                        client.Connect(ep);
                    }
                    catch (SocketException e)
                    {
                        connection.Invoke(this, new Recive("connection", "can't find server"));
                        isConected = false;
                        return;
                    }

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
                            if (commands.ContainsKey(commandKey) || updateCommands.ContainsKey(commandKey))
                            {
                                System.EventHandler<Recive> temp = (commands.ContainsKey(commandKey)) ? reciveFromServer : updateFromServer;
                                Dictionary<string, ICommand> tempCommands = (commands.ContainsKey(commandKey)) ? commands : updateCommands;

                                string[] arguments = arr.Skip(1).ToArray();
                                ICommand command = tempCommands[commandKey];
                                RecieveInfo recieveInfo = command.Execute(arguments, this, client);
                                connectionAlive = recieveInfo.connectionAlive;

                                Task viewTask = new Task(() =>
                                {
                                    temp.Invoke(this, new Recive(recieveInfo.typeCommand, recieveInfo.result));
                                });
                                viewTask.Start();
                            }
                            if (connectionAlive)
                            {
                                // NEED other input
                                while (message.CompareTo("") == 0)
                                {

                                }
                            }
                        }
                    }
                    client.Close();
                    message = "";
                });
                task.Start();
                task.Wait();

            }
        }

        /// <summary>
        /// Games the over.
        /// </summary>
        public void GameOver()
        {
            connectionAlive = false;
            message = "close";
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Initializes the game.
        /// </summary>
        public void InitializeGame()
        {
            initializeEvent.Invoke(this, new Recive("initializeGame", null));
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Gets or sets the current row.
        /// </summary>
        /// <value>
        /// The current row.
        /// </value>
        public int CurrentRow
        {
            get
            {
                return this.currentRow;
            }
            set
            {
                this.currentRow = value;
                NotifyPropertyChanged("CurrentRow");
            }
        }

        /// <summary>
        /// Gets or sets the current col.
        /// </summary>
        /// <value>
        /// The current col.
        /// </value>
        public int CurrentCol
        {
            get
            {
                return this.currentCol;
            }
            set
            {
                this.currentCol = value;
                NotifyPropertyChanged("CurrentCol");
            }

        }

        /// <summary>
        /// Gets or sets the other current row.
        /// </summary>
        /// <value>
        /// The other current row.
        /// </value>
        public int OtherCurrentRow
        {
            get
            {
                return this.otherCurrentRow;
            }
            set
            {
                this.otherCurrentRow = value;
                NotifyPropertyChanged("OtherCurrentRow");
            }
        }

        /// <summary>
        /// Gets or sets the other current col.
        /// </summary>
        /// <value>
        /// The other current col.
        /// </value>
        public int OtherCurrentCol
        {
            get
            {
                return this.otherCurrentCol;
            }
            set
            {
                this.otherCurrentCol = value;
                NotifyPropertyChanged("OtherCurrentCol");
            }

        }

        /// <summary>
        /// Gets or sets the game multi player list.
        /// </summary>
        /// <value>
        /// The game multi player list.
        /// </value>
        public List<string> GameMultiPlayerList
        {
            get
            {
                return this.multiplayerList;
            }
            set
            {
                this.multiplayerList = value;
                NotifyPropertyChanged("GameMultiPlayerList");
            }
        }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>
        /// The name of the game.
        /// </value>
        public string GameName
        {
            get
            {
                return this.gameName;
            }
            set
            {
                this.gameName = value;
                NotifyPropertyChanged("GameName");
            }
        }
        /// <summary>
        /// Gets or sets the end row.
        /// </summary>
        /// <value>
        /// The end row.
        /// </value>
        public int EndRow { get; set; }

        /// <summary>
        /// Gets or sets the end col.
        /// </summary>
        /// <value>
        /// The end col.
        /// </value>
        public int EndCol { get; set; }

        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>
        /// The server ip.
        /// </value>
        public string ServerIP { get; set; }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server port.
        /// </value>
        public int ServerPort { get; set; }
        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        public int MazeRows
        {
            get
            {
                return this.mazeRows;
            }
            set
            {
                this.mazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>
        /// The maze cols.
        /// </value>
        public int MazeCols
        {
            get
            {
                return this.mazeCols;
            }
            set
            {
                this.mazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        /// <summary>
        /// Gets or sets the search algorithm.
        /// </summary>
        /// <value>
        /// The search algorithm.
        /// </value>
        public int SearchAlgorithm { get; set; }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            Properties.Settings.Default.ServerIP = ServerIP;
            Properties.Settings.Default.ServerPort = ServerPort;
            Properties.Settings.Default.MazeRows = MazeRows;
            Properties.Settings.Default.MazeCols = MazeCols;
            Properties.Settings.Default.SearchAlgorithm = SearchAlgorithm;
        }

        /// <summary>
        /// Cancels the settings.
        /// </summary>
        public void CancelSettings()
        {
            ServerIP = Properties.Settings.Default.ServerIP;
            ServerPort = Properties.Settings.Default.ServerPort;
            MazeRows = Properties.Settings.Default.MazeRows;
            MazeCols = Properties.Settings.Default.MazeCols;
            SearchAlgorithm = Properties.Settings.Default.SearchAlgorithm;

        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="isSinglePlayer">if set to <c>true</c> [is single player].</param>
        public void MovePlayer(Key key, bool isSinglePlayer)
        {
            bool isFinish = MoveOneStep(key, isSinglePlayer);
            // goal position
            if (isFinish)
            {
                RecieveInfo recieveInfo = new RecieveInfo("", false, "win");
                reciveFromServer.Invoke(this, new Recive(recieveInfo.typeCommand, recieveInfo.result));
            }
        }

        /// <summary>
        /// Other player won.
        /// </summary>
        public void OtherPlayerWon()
        {
            reciveFromServer.Invoke(this, new Recive("player won", "Other player won"));
        }

        /// <summary>
        /// Moves the one step.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="isSinglePlayer">if set to <c>true</c> [is single player].</param>
        /// <returns></returns>
        private bool MoveOneStep(Key key, bool isSinglePlayer)
        {
            Object[] arrResult = GetNewRowAndCol(key, currentRow, currentCol);
            int newRow = (int)arrResult[0];
            int newCol = (int)arrResult[1];
            string direction = (string)arrResult[2];

            if (!isSinglePlayer)
            {
                this.Send("play " + direction);
            }
            // if valid:
            if (newRow >= 0 && newRow < Maze.Rows && newCol >= 0 && newCol < Maze.Cols
                && Maze[newRow, newCol] != MazeLib.CellType.Wall)
            {
                CurrentRow = newRow;
                CurrentCol = newCol;
            }
            return (newRow == Maze.GoalPos.Row && newCol == Maze.GoalPos.Col);
        }

        /// <summary>
        /// Gets the new row and col.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="currentRowPlace">The current row place.</param>
        /// <param name="currentColPlace">The current col place.</param>
        /// <returns></returns>
        public Object[] GetNewRowAndCol(Key key, int currentRowPlace, int currentColPlace)
        {
            Object[] arrResult = new Object[3];
            int newRow = currentRowPlace;
            int newCol = currentColPlace;
            string direction = "";
            switch (key)
            {
                case Key.Up:
                    direction = "up";
                    newRow -= 1;
                    break;
                case Key.Down:
                    direction = "down";
                    newRow += 1;
                    break;
                case Key.Left:
                    direction = "left";
                    newCol -= 1;
                    break;
                case Key.Right:
                    direction = "right";
                    newCol += 1;
                    break;
            }

            arrResult[0] = newRow;
            arrResult[1] = newCol;
            arrResult[2] = direction;
            return arrResult;
        }

        /// <summary>
        /// Moves the player solve.
        /// </summary>
        /// <param name="key">The key.</param>
        public void MovePlayerSolve(Key key)
        {
            MoveOneStep(key, true);
        }

        /// <summary>
        /// Restarts the player.
        /// </summary>
        public void RestartPlayer()
        {
            CurrentRow = Maze.InitialPos.Row;
            CurrentCol = Maze.InitialPos.Col;
        }

    }
}
