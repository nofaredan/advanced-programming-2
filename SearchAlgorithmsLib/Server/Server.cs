using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    public class Server
    {
        List<TcpClient> clientsPool;
        private TcpListener listener;
        private IView ch;

        public Server(IView ch)
        {
            clientsPool = new List<TcpClient>();
            this.ch = ch;
        }

        public void Start()
        {
            string ipAdresss = ConfigurationManager.AppSettings["IP"].ToString();
            string strPort = ConfigurationManager.AppSettings["port"].ToString();
            int port = Int32.Parse(strPort);

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAdresss), port);
            listener = new TcpListener(ep);

            listener.Start();

            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                                               
                        clientsPool.Add(client);
                        ch.HandleClient(client);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }
        public void Stop()
        {
            listener.Stop();
        }
    }
    }
