using System;
using System.Net.Sockets;
namespace Client
{
	public interface ICommand
	{
		bool Execute(string[] args, TcpClient client = null);
	}
}
