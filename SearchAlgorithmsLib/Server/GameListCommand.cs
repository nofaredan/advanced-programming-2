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
        public GameListCommand(MazeGame myGame)
        {
            game = myGame;
        }

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


            string result = JsonConvert.SerializeObject(list); ;
            game.WriteMessage(new StreamWriter(currentPlayer.GetStream()), result);
        }
    }
}
