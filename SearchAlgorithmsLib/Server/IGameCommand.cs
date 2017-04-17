﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IGameCommand
    {
        ConnectionInfo Execute(string[] args, string name, TcpClient currentPlayer = null);
    }
}
