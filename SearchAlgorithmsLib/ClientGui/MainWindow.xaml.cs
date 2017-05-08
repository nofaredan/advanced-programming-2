using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public MainWindow()
        {
            viewModel = new ViewModel(new Model());
            commands = new Dictionary<string, IViewCommand>();
            commands.Add("generate", new ViewGenerateMode());
            InitializeComponent();
            viewModel.result += ReciveInfo;
            
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            currentWindow = new SettingsWindow(viewModel);
            this.Hide();
            ShowDialog();
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            currentWindow = new InitializeSinglePlayer(this);
            ((InitializeSinglePlayer)currentWindow).commands += commandGame;
            this.Hide();
            ShowDialog();

            //this.Close();
        }

        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void commandGame(object sender, MazeCommand e)
        {
            string message = e.CommandType;
            foreach(string arg in e.args){
                message += " " + arg;
            }
            viewModel.Send(message);
        }

        private void ReciveInfo(object sender, Recive e)
        {
            this.Dispatcher.Invoke(() => {
            Window oldWindow = currentWindow;
            if (commands.ContainsKey(e.type))
            {
                    oldWindow.Hide();
                    currentWindow = commands[e.type].Execute(e.result);
                    ShowDialog();
                    oldWindow.Close();
                      
            }
            });
        }

        private void ShowDialog()
        {
            this.Dispatcher.Invoke(() => { 
                currentWindow.ShowDialog();
            });
           

        }

    }
}
