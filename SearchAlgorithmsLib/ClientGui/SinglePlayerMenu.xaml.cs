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
using System.Windows.Shapes;

namespace ClientGui
{
    /// <summary>
    /// Interaction logic for InitializeSinglePlayer.xaml
    /// </summary>
    public partial class InitializeSinglePlayer : Window
    {
        MainWindow mainWindow;
        public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
        public event EventHandler<MazeCommand> commands;

        public InitializeSinglePlayer(MainWindow win)
        {
            this.mainWindow = win;
            InitializeComponent();
            
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
         
            string[] args = { game_name_text.Text , rows_text.Text, cols_text.Text};
            this.commands.Invoke(this, new MazeCommand("generate", args));
            //this.mainWindow.commands.Invoke(this, new MazeCommand("generate"));
        }

       
    }
}
