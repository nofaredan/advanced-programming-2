﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Client
{
	public class Game
	{
		private static bool gameAlive;
		public static TcpClient server { get; set; }

<<<<<<< HEAD
        public static void StartGame()
        {
            gameAlive = true;
            new Task(() => {
                // wait for join
                NetworkStream stream = server.GetStream();
                StreamReader reader = new StreamReader(stream);
                
                    while (gameAlive)
                    {
                        bool answer = SendAndRecieve.RecieveInfo(reader);          
                    }
                
            }).Start();
        }
    }
}
=======
		public static void StartGame()
		{
			gameAlive = true;
			new Task(() =>
			{
				// wait for join
				NetworkStream stream = server.GetStream();
				StreamReader reader = new StreamReader(stream);

				while (gameAlive)
				{
					bool answer = SendAndRecieve.RecieveInfo(reader);
				}


			}).Start();
		}
	}
}
>>>>>>> ed4fabfa7ebd57e25b740162d6dc41f2519e9a48
