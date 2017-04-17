using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Server
{
    public class GamePlayCommand : IGameCommand
    {
        MazeGame game;
        public GamePlayCommand(MazeGame myGame)
        {
            game = myGame;
        }

        public ConnectionInfo Execute(string[] args, List<TcpClient> clients, TcpClient currentPlayer = null)
        {
            JObject json = new JObject();
            json["Name"] = game.maze.Name;
            json["Direction"] = args[0];
            bool found = false;
            int index = 0;
            TcpClient clientFound = null;
            while (!found && index< clients.Count)
            {
                if (clients[index] != currentPlayer)
                {
                    clientFound = clients[index];
                    found = true;
                }
                index++;
            }
			game.WriteMessage(new System.IO.StreamWriter(clientFound.GetStream()), json.ToString());
            return null;
        }
    }
}
