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
            new Task(() => {
                while (gameAlive)
                {
                    // wait for join
                    using (NetworkStream stream = server.GetStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        bool answer = SendAndRecieve.RecieveInfo(reader);   
                        
                        //string message =               
                    }
                }

            }).Start();
        }
    }
}
