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

<<<<<<< HEAD
        public MazeGame(Maze myMaze)
        {
            gamesInfo = new Dictionary<string, GameInfo>();
            GameInfo gameInfo = new GameInfo();
            gameInfo.name = myMaze.Name;
            gameInfo.maze = myMaze;
            gameInfo.isStart = false;
            gameInfo.isEnd = false;
            gameInfo.players = new List<TcpClient>();
=======
>>>>>>> ed4fabfa7ebd57e25b740162d6dc41f2519e9a48

		public MazeGame(Maze myMaze)
		{
			gamesInfo = new Dictionary<string, GameInfo>();
			GameInfo gameInfo = new GameInfo();
			gameInfo.maze = myMaze;
			gameInfo.isStart = false;
			gameInfo.isEnd = false;
			gameInfo.players = new List<TcpClient>();

<<<<<<< HEAD
            gameCommands = new Dictionary<string, IGameCommand>();
            gameCommands.Add("list", new GameListCommand(this));
            gameCommands.Add("play", new GamePlayCommand(this));
            gameCommands.Add("close", new GameCloseCommand(this));
        }
=======
			gamesInfo.Add(myMaze.Name, gameInfo);
>>>>>>> ed4fabfa7ebd57e25b740162d6dc41f2519e9a48

			gameCommands = new Dictionary<string, IGameCommand>();
			gameCommands.Add("play", new GamePlayCommand(this));
			gameCommands.Add("close", new GameCloseCommand(this));
		}

		public void addPlayer(TcpClient player, string name)
		{
			GameInfo currenrGameInfo = gamesInfo[name];
			currenrGameInfo.players.Add(player);

<<<<<<< HEAD
            NetworkStream stream = player.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
                while (!currenrGameInfo.isEnd)
                {
                  /*  while (!currenrGameInfo.isStart)
                    {
                        // first is waiting
                    }
                    */

                    // play
                    string commandLine = reader.ReadLine();
                    //Console.WriteLine("2");
                    string[] arr = commandLine.Split(' ');
                    string commandKey = arr[0];
                    string[] args = arr.Skip(1).ToArray();
                    gameCommands[commandKey].Execute(args, name, player);

                }               
        }
=======
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
>>>>>>> ed4fabfa7ebd57e25b740162d6dc41f2519e9a48

			NetworkStream stream = player.GetStream();
			StreamReader reader = new StreamReader(stream);
			StreamWriter writer = new StreamWriter(stream);
			while (!currenrGameInfo.isEnd)
			{
				while (!currenrGameInfo.isStart)
				{
					// first is waiting
				}
				// play

				string commandLine = reader.ReadLine();
				//Console.WriteLine("2");
				string[] arr = commandLine.Split(' ');
				string commandKey = arr[0];
				string[] args = arr.Skip(1).ToArray();
				ConnectionInfo result = gameCommands[commandKey].Execute(args, name, player);

			}
			//TODO:: DISPOSE CONNECTION ON END

		}

		public void WriteMessage(StreamWriter writer, string message)
		{
			// write maze to both players:
			writer.WriteLine(message);
			writer.Flush();
			writer.WriteLine("end");
			writer.Flush();
		}
	}
}