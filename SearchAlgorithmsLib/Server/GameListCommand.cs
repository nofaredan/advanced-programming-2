using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class GameListCommand : IGameCommand
    {
        MazeGame game;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameListCommand"/> class.
        /// </summary>
        /// <param name="myGame">My game.</param>
        public GameListCommand(MazeGame myGame)
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
            List<string> list = new List<string>();
            foreach(GameInfo gameInfo in MazeGame.gamesInfo.Values)
            {
                if (gameInfo.players.Count!=2)
                {
                    // add name to list
                    list.Add(gameInfo.name);
                }
            }

            // create result:
            string result = JsonConvert.SerializeObject(list); ;
            game.WriteMessage(new StreamWriter(currentPlayer.GetStream()), result);
        }
    }
}
