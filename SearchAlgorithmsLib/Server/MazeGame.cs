using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using MazeLib;

namespace Server
{
    public class MazeGame
    {
        private Dictionary<string, IGameCommand> gameCommands;
        public bool isStart { get; set; }
        public bool isEnd { get; set; }
        public Maze maze { get; set; }
        List<TcpClient> players;

        public MazeGame(Maze myMaze)
        {
            maze = myMaze;
            isStart = false;
            isEnd = false;
            players = new List<TcpClient>();
            gameCommands = new Dictionary<string, IGameCommand>();
            gameCommands.Add("play", new GamePlayCommand(this));
            gameCommands.Add("close", new GameCloseCommand(this));
        }

        public List<TcpClient> Players
        {
            get
            {
                return players;
            }

            set
            {
                players = value;
            }
        }

        public void addPlayer(TcpClient player)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            players.Add(player);
            
            // if its the second player
            if (players.Count == 2)
            {
                foreach (TcpClient currentPlayer in players)
                {
               //     WriteMessage(player, maze.ToJSON());
					WriteMessage(new StreamWriter(currentPlayer.GetStream()),maze.ToJSON());
                }
            }
		NetworkStream stream = player.GetStream();
		StreamReader reader = new StreamReader(stream);
		StreamWriter writer = new StreamWriter(stream);
            
                while (!isEnd)
                {
                    while (!isStart)
                    {
                        // first is waiting
                    }
                    // play
                   
                    string commandLine = reader.ReadLine();
                    //Console.WriteLine("2");
                    string[] arr = commandLine.Split(' ');
                    string commandKey = arr[0];
                    string[] args = arr.Skip(1).ToArray();
                    ConnectionInfo result = gameCommands[commandKey].Execute(args,players, player);

                }
            
               
        }

        public void WriteMessage(StreamWriter writer, string message)
        {
                // write maze to both players:
                writer.WriteLine(message);
                writer.Flush();
        }
    }
}
