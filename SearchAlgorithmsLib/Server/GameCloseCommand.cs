using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	public class GameCloseCommand : IGameCommand
	{
		MazeGame game;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameCloseCommand"/> class.
        /// </summary>
        /// <param name="myGame">My game.</param>
        public GameCloseCommand(MazeGame myGame)
		{
			game = myGame;
		}

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="name">The name.</param>
        /// <param name="currentPlayer">The current player.</param>
        public void Execute(string[] args, string name, TcpClient currentPlayer = null)
        {
            GameInfo currenrGameInfo = MazeGame.gamesInfo[name];
            List<TcpClient> clients = MazeGame.gamesInfo[name].Players;
            JObject json = new JObject();
            json[""] = "game closed";
            bool found = false;
            int index = 0;
            TcpClient clientFound = null;

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
            // remove from list
            MazeGame.gamesInfo.Remove(name);

            // send json to client
            game.WriteMessage(new StreamWriter(clientFound.GetStream()), json.ToString(), currenrGameInfo);
        }
    }
}
