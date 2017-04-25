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
		public static Dictionary<string, GameInfo> gamesInfo { get; set; }
		private static Dictionary<string, IGameCommand> gameCommands;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeGame"/> class.
        /// </summary>
        /// <param name="myMaze">My maze.</param>
        public MazeGame(Maze myMaze)
        {
            gamesInfo = new Dictionary<string, GameInfo>();
            GameInfo gameInfo = new GameInfo();
            gameInfo.name = myMaze.Name;
            gameInfo.maze = myMaze;
            gameInfo.isStart = false;
            gameInfo.isEnd = false;
            gameInfo.players = new List<TcpClient>();
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
        public void addPlayer(TcpClient player, string name)
		{
			GameInfo currenrGameInfo = gamesInfo[name];
			currenrGameInfo.players.Add(player);

            // if its the second player
            if (currenrGameInfo.players.Count == 2)
            {
                foreach (TcpClient currentPlayer in currenrGameInfo.players)
                {
                    WriteMessage(new StreamWriter(currentPlayer.GetStream()), currenrGameInfo.maze.ToJSON());
                }
                // game stated
                currenrGameInfo.isStart = true;
            }

            NetworkStream stream = player.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
                while (!currenrGameInfo.isEnd)
                {
                    // play
                    string commandLine = reader.ReadLine();
                    string[] arr = commandLine.Split(' ');
                    string commandKey = arr[0];
                    string[] args = arr.Skip(1).ToArray();
                    gameCommands[commandKey].Execute(args, name, player);
                }               
        }

        /// <summary>
        /// Writes the message.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="message">The message.</param>
        public void WriteMessage(StreamWriter writer, string message)
		{
			// write maze to both players:
			writer.WriteLine(message);
			writer.Flush();

            // send for the end of the message:
			writer.WriteLine("end");
			writer.Flush();
		}
	}
}