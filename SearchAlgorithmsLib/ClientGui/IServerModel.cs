using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    public interface IServerModel : INotifyPropertyChanged
    {
        string ServerIP { get; set; }
        int ServerPort { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }
        int SearchAlgorithm { get; set; }
        void SaveSettings();
        void CancelSettings();

        event EventHandler<Recive> reciveFromServer;
       
        
       // connect to client
        bool connectClient();

        void Send(string message);

        void Recieve();
    }
}
