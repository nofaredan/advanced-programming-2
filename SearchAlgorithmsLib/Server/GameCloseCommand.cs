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
        public GameCloseCommand(MazeGame myGame)
        {
            game = myGame;
        }

        public void Execute(string[] args, string name, TcpClient currentPlayer = null)
        {
            List<TcpClient> clients = MazeGame.gamesInfo[name].players;
            JObject json = new JObject();
            json[""] = "game closed";
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
            // remove from list
            MazeGame.gamesInfo.Remove(name);

            // send json to client
            game.WriteMessage(new StreamWriter(clientFound.GetStream()), json.ToString());
        }
    }
}
