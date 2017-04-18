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
<<<<<<< HEAD
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
=======
	public class Server
	{
		List<TcpClient> clientsPool;
		private int port;
		private TcpListener listener;
		private IView ch;

		public Server(int port, IView ch)
		{
			clientsPool = new List<TcpClient>();
			this.port = port;
			this.ch = ch;
		}

		public void Start()
		{
			IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
			listener = new TcpListener(ep);
>>>>>>> ed4fabfa7ebd57e25b740162d6dc41f2519e9a48

			listener.Start();

			Task task = new Task(() =>
			{
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