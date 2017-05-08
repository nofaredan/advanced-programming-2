using System;
using System.Net.Sockets;
namespace ClientGui
{
	public interface ICommand
	{
        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        RecieveInfo Execute(string[] args, TcpClient client = null);
	}
}