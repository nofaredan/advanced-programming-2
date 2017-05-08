using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    public class ViewModel : INotifyPropertyChanged
    {
        private IServerModel model;
        public event PropertyChangedEventHandler PropertyChanged;
 

        public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
        public event EventHandler<Recive> result;

        public ViewModel(IServerModel model)
        {
            this.model = model;

            Task connectTask = new Task(() => {
                this.model.connectClient();
            });
            connectTask.Start();

            this.model.reciveFromServer += ReciveInfo;
            model.PropertyChanged +=
               delegate (Object sender, PropertyChangedEventArgs e)
               {
                   NotifyPropertyChanged("VM_" + e.PropertyName);
               };
        }

        public void NotifyPropertyChanged(string propName)
        {
             if (this.PropertyChanged != null)
             {
                 this.PropertyChanged(this, new PropertyChangedEventArgs("VM_"+propName));
             }
        }

          public void Send(string message)
          {
              model.Send(message);
          }

        private void ReciveInfo(object sender, Recive e)
        {
            result.Invoke(this, e);
        }

        //----------> SETTINGS/////////////////


        public void SaveSettings()
        {
            model.SaveSettings();
        }

        public void CancelSettings()
        {
            model.CancelSettings();
        }

        public string VM_ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }

        public int VM_ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        public int VM_MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        public int VM_MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        public int VM_SearchAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set
            {
                model.SearchAlgorithm = value;
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }




    }
}
