﻿using System;
using System.Collections.Generic;
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

        public ConnectionInfo Execute(string[] args, List<TcpClient> clients, TcpClient player = null)
        {
            throw new NotImplementedException();
        }
    }
}