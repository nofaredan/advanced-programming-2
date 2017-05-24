using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    class SendMultiPlayerListCommand : ICommand
    {
        public RecieveInfo Execute(string[] args, IServerModel model, TcpClient client = null)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            string result = SendAndRecieve.RecieveInfo(reader);
            client.GetStream().Flush();
            client.GetStream().Close();
            client.Close();
            result = result.Substring(1,result.Length-2);
            if (result!="")
            {
                List<string> temp = new List<string>();
               List<string> arr = result.Split(',').ToList();
                foreach(string s in arr)
                {
                    temp.Add(s.Substring(1, s.Length - 2));
                }
                model.GameMultiPlayerList = temp;
            }
            else
            {
                model.GameMultiPlayerList = new List<string>();
            }
            
            return new RecieveInfo(result, false, "list");
        }
    }
}
