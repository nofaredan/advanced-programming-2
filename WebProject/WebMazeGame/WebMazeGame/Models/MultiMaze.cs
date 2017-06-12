using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using MazeLib;
using System.Threading;

namespace WebMazeGame
{
    public class MultiMaze
    {
      /*  public static Dictionary<string, GameInfo> gamesInfo { get; set; }
        private static Dictionary<string, IGameCommand> gameCommands;
        private static Mutex mutex, mutex2;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeGame"/> class.
        /// </summary>
        /// <param name="myMaze">My maze.</param>
        public MultiMaze(Maze myMaze)
        {
            mutex = new Mutex();
            mutex2 = new Mutex();

            if (gamesInfo == null)
            {
                gamesInfo = new Dictionary<string, GameInfo>();
            }

            GameInfo gameInfo = new GameInfo();
            gameInfo.Name = myMaze.Name;
            gameInfo.Maze = myMaze;
            gameInfo.IsStart = false;
            gameInfo.IsEnd = false;
            gameInfo.Players = new List<TcpClient>();
            gameCommands = new Dictionary<string, IGameCommand>();
            gameCommands.Add("list", new GameListCommand(this));
            gameCommands.Add("play", new GamePlayCommand(this));
            gameCommands.Add("close", new GameCloseCommand(this));

            gamesInfo.Add(myMaze.Name, gameInfo);
        }

        /// <summary>
        /// Adds the player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="name">The name.</param>
        public void AddPlayer(TcpClient player, string name)
        {
            mutex.WaitOne();
            GameInfo currenrGameInfo = gamesInfo[name];
            currenrGameInfo.Players.Add(player);

            // if its the second player
            if (currenrGameInfo.Players.Count == 2)
            {
                foreach (TcpClient currentPlayer in currenrGameInfo.Players)
                {
                    WriteMessage(new StreamWriter(currentPlayer.GetStream()), currenrGameInfo.Maze.ToJSON());
                }
                // game stated
                currenrGameInfo.IsStart = true;
            }

            mutex.ReleaseMutex();

            NetworkStream stream = player.GetStream();

            while (!currenrGameInfo.IsEnd)
            {
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                // play
                string commandLine = reader.ReadLine();
                if (!currenrGameInfo.IsEnd)
                {
                    string[] arr = commandLine.Split(' ');
                    string commandKey = arr[0];
                    string[] args = arr.Skip(1).ToArray();

                    mutex2.WaitOne();
                    gameCommands[commandKey].Execute(args, name, player);
                    mutex2.ReleaseMutex();
                }
            }

            if (gamesInfo.ContainsKey(name))
            {
                gamesInfo.Remove(name);
            }
        }

        /// <summary>
        /// Writes the message.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="message">The message.</param>
        public void WriteMessage(StreamWriter writer, string message, GameInfo currenrGameInfo = null)
        {
            // write maze to both players:
            writer.WriteLine(message);
            writer.Flush();

            // send for the end of the message:
            writer.WriteLine("end");
            writer.Flush();

            if (currenrGameInfo != null)
            {
                currenrGameInfo.IsEnd = true;
            }
        }*/
    }
}