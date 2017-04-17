using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    public class MazeGame
    {
        List<TcpClient> players;

        public MazeGame()
        {
            players = new List<TcpClient>();
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
            players.Add(player);
        }
    }
}
