using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Windows.Input;
using System.Windows.Data;

namespace ClientGui
{
    public class ViewModel : INotifyPropertyChanged
    {
        private IServerModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
        public event EventHandler<Recive> result;
        public event EventHandler<Recive> updateResult;
        public event EventHandler<Recive> gameFlow;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ViewModel(IServerModel model)
        {
            this.model = model;

            Task connectTask = new Task(() => {
                this.model.ConnectClient();
            });
            connectTask.Start();

            this.model.reciveFromServer += ReciveInfo;
            this.model.updateFromServer += Update;
            this.model.connection += ReciveInfo;
            this.model.initializeEvent += InitializeGame;

            model.PropertyChanged +=
               delegate (Object sender, PropertyChangedEventArgs e)
               {
                   NotifyPropertyChanged("VM_" + e.PropertyName);
               };
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyPropertyChanged(string propName)
        {
             if (this.PropertyChanged != null)
             {
                 this.PropertyChanged(this, new PropertyChangedEventArgs(/*"VM_"+*/propName));
             }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(string message)
          {
              model.Send(message);
          }

        /// <summary>
        /// Updates the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Update(object sender, Recive e)
        {
            updateResult.Invoke(this, e);
        }

        /// <summary>
        /// Recives the information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ReciveInfo(object sender, Recive e)
        {
            result.Invoke(this, e);
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            model.SaveSettings();
        }

        /// <summary>
        /// Cancels the settings.
        /// </summary>
        public void CancelSettings()
        {
            model.CancelSettings();
        }

        /// <summary>
        /// Gets or sets the vm server ip.
        /// </summary>
        /// <value>
        /// The vm server ip.
        /// </value>
        public string VM_ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }

        /// <summary>
        /// Gets or sets the vm server port.
        /// </summary>
        /// <value>
        /// The vm server port.
        /// </value>
        public int VM_ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        /// <summary>
        /// Gets or sets the vm maze rows.
        /// </summary>
        /// <value>
        /// The vm maze rows.
        /// </value>
        public int VM_MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        /// <summary>
        /// Gets or sets the vm maze cols.
        /// </summary>
        /// <value>
        /// The vm maze cols.
        /// </value>
        public int VM_MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        /// <summary>
        /// Gets or sets the vm search algorithm.
        /// </summary>
        /// <value>
        /// The vm search algorithm.
        /// </value>
        public int VM_SearchAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set
            {
                model.SearchAlgorithm = value;
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }

        /// <summary>
        /// Gets or sets the vm current row.
        /// </summary>
        /// <value>
        /// The vm current row.
        /// </value>
        public int VM_CurrentRow
        {
            get { return model.CurrentRow; }
            set
            {
                model.CurrentRow = value;
                NotifyPropertyChanged("CurrentRow");
            }

        }

       /// <summary>
        /// Gets or sets the vm current col.
        /// </summary>
        /// <value>
        /// The vm current col.
        /// </value>
        public int VM_CurrentCol
        {
            get { return model.CurrentCol; }
            set
            {
                model.CurrentCol = value;
                NotifyPropertyChanged("CurrentCol");
            }
        }

        /// <summary>
        /// Gets or sets the vm other current row.
        /// </summary>
        /// <value>
        /// The vm other current row.
        /// </value>
        public int VM_OtherCurrentRow
        {
            get { return model.OtherCurrentRow; }
            set
            {
                model.OtherCurrentRow = value;
                NotifyPropertyChanged("OtherCurrentRow");
            }

        }

        /// <summary>
        /// Gets or sets the vm other current col.
        /// </summary>
        /// <value>
        /// The vm other current col.
        /// </value>
        public int VM_OtherCurrentCol
        {
            get { return model.OtherCurrentCol; }
            set
            {
                model.OtherCurrentCol = value;
                NotifyPropertyChanged("OtherCurrentCol");
            }
        }

        /// <summary>
        /// Gets or sets the name of the vm game.
        /// </summary>
        /// <value>
        /// The name of the vm game.
        /// </value>
        public string VM_GameName
        {
            get { return model.GameName; }
            set
            {
                model.GameName = value;
                NotifyPropertyChanged("GameName");
            }
        }

        /// <summary>
        /// Gets the vm game multi player list.
        /// </summary>
        /// <value>
        /// The vm game multi player list.
        /// </value>
        public CollectionView VM_GameMultiPlayerList
        {
            get { return new CollectionView(model.GameMultiPlayerList); }
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="keyCode">The key code.</param>
        /// <param name="isSinglePlayer">if set to <c>true</c> [is single player].</param>
        public void MovePlayer(Key keyCode, bool isSinglePlayer)
        {
            model.MovePlayer(keyCode, isSinglePlayer);
        }

        /// <summary>
        /// Moves the player solve.
        /// </summary>
        /// <param name="keyCode">The key code.</param>
        public void MovePlayerSolve(Key keyCode)
        {
            model.MovePlayerSolve(keyCode);
        }

        /// <summary>
        /// Restarts the player.
        /// </summary>
        public void RestartPlayer()
        {
            model.RestartPlayer();
        }

        /// <summary>
        /// Initializes the game.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void InitializeGame(object sender = null, Recive e = null)
        {
            if (sender == null)
            {
                gameFlow.Invoke(this, new Recive("initializeGame", null));
            }
            else
            {
                gameFlow.Invoke(sender, e);

            }
        }
    }
}
