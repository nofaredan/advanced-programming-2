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
        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
		{
			View view = new View();
			view.Connect();
		}
	}



}