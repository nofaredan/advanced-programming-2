using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

using System.Windows.Shapes;

namespace ClientGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        private static Window currentWindow;
        private static Dictionary<string, IViewCommand> commands;
        private static Dictionary<string, IViewCommand> windowMessages;
        private static Dictionary<string, IUpdateCommand> updateCommands;
        private static Mutex mutex;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            mutex = new Mutex();
            viewModel = new ViewModel(new Model());
            commands = new Dictionary<string, IViewCommand>();
            windowMessages = new Dictionary<string, IViewCommand>();
            updateCommands = new Dictionary<string, IUpdateCommand>();
            commands.Add("generate", new ViewGenerateMode());
            commands.Add("start", new ViewStartMode());
            commands.Add("join", new ViewJoinMode());
            commands.Add("win", new ViewWinMode());

            updateCommands.Add("solve", new ViewSolveMode());
            updateCommands.Add("list", new ViewSendMultiPlayerList());

            windowMessages.Add("invalid command", new MessageUser());
            windowMessages.Add("can't find server", new MessageUser());
            windowMessages.Add("Other player won", new MessageUser());

            InitializeComponent();
            viewModel.result += ReciveInfo;
            viewModel.updateResult += Update;
            viewModel.gameFlow += UpdateScreen;
            DataContext = viewModel;


            viewModel.PropertyChanged +=
               delegate (Object sender, PropertyChangedEventArgs e)
               {
                   //  NotifyPropertyChanged("V_" + e.PropertyName);
               };

        }

        /// <summary>
        /// Handles the Click event of the Settings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            currentWindow = new SettingsWindow(viewModel);
            this.Hide();
            ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the SinglePlayer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            currentWindow = new InitializeSinglePlayer(viewModel);
            ((InitializeSinglePlayer)currentWindow).commands += commandGame;
            this.Hide();
            ShowDialog();

            //this.Close();
        }

        /// <summary>
        /// Handles the Click event of the MultiPlayer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {
            currentWindow = new MultiPlayerMenu(viewModel);
            ((MultiPlayerMenu)currentWindow).commands += commandGame;
            this.Hide();
            ShowDialog();
        }

        /// <summary>
        /// Commands the game.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void commandGame(object sender, MazeCommand e)
        {
            string message = e.CommandType;
            foreach (string arg in e.args)
            {
                message += " " + arg;
            }
            viewModel.Send(message);
        }

        /// <summary>
        /// Updates the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Update(object sender, Recive e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (updateCommands.ContainsKey(e.type))
                {
                    updateCommands[e.type].Execute(e.result, currentWindow);
                }
            });
        }

        /// <summary>
        /// Recives the information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ReciveInfo(object sender, Recive e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (windowMessages.ContainsKey(e.result))
                {
                    windowMessages[e.result].Execute(e.result, viewModel);

                    // hide multiplayer waiting window
                    if (currentWindow is MultiPlayerMenu)
                    {
                        ((MultiPlayerMenu)(currentWindow)).HideWaiting();
                    }
                    else
                    {
                        singleplayer_button.IsEnabled = false;
                        multiplayer_button.IsEnabled = false;
                        settings_button.IsEnabled = false;
                    }
                }
                else
                {
                    Window oldWindow = currentWindow;
                    if (commands.ContainsKey(e.type))
                    {
                        oldWindow.Hide();
                        currentWindow = commands[e.type].Execute(e.result, viewModel);

                        currentWindow.DataContext = viewModel;
                        oldWindow.Close();
                        ShowDialog();
                    }

                }
            });
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        private void ShowDialog()
        {
            this.Dispatcher.Invoke(() =>
            {
                currentWindow.ShowDialog();
            });
        }

        /// <summary>
        /// Updates the screen.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void UpdateScreen(object sender, Recive e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.Show();
                singleplayer_button.IsEnabled = true;
                multiplayer_button.IsEnabled = true;
                settings_button.IsEnabled = true;

                if (sender is IServerModel)
                {
                    currentWindow.Close();
                    // send a command window - disconnected
                    InvalidCommand invalidCommand = new InvalidCommand(viewModel, "Partner disconnected");
                    invalidCommand.ShowDialog();
                }
               
            });
        }

    }
}
