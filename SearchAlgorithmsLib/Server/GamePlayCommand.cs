using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Server
{
	public class GamePlayCommand : IGameCommand
	{
		MazeGame game;
		public GamePlayCommand(MazeGame myGame)
		{
			game = myGame;
		}

		public ConnectionInfo Execute(string[] args, string name, TcpClient currentPlayer = null)
		{
			List<TcpClient> clients = MazeGame.gamesInfo[name].players;
			JObject json = new JObject();
			json["Name"] = name;
			json["Direction"] = args[0];
			bool found = false;
			int index = 0;
			TcpClient clientFound = null;
			// ConnectionInfo connectionInfo = new ConnectionInfo();

			// find client
			while (!found && index < clients.Count)
			{
				if (clients[index] != currentPlayer)
				{
					clientFound = clients[index];
					found = true;
				}
				index++;
			}
			// send json to client
			game.WriteMessage(new StreamWriter(clientFound.GetStream()), json.ToString());
			return null;
		}
	}
}