using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6565);
            client.Connect(ep);
            while (true) {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Send data to server
                   Console.WriteLine("1");
                    string message = Console.ReadLine();
                    Console.WriteLine("2");
                    writer.Write(message);
                    writer.Flush();
                    Console.WriteLine("3");
                    // Get result from server
                    string result = reader.ReadString();
                    Console.WriteLine("Result = {0}", result);
                }
               }
            client.Close();
        }
    }
}
