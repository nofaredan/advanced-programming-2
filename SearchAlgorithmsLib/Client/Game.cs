using System;
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