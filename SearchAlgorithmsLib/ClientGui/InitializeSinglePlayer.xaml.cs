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
    /// Interaction logic for SinglePlayerMenu.xaml
    /// </summary>
    public partial class InitializeSinglePlayer : Window
    {
        public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
        public event EventHandler<MazeCommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeSinglePlayer"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public InitializeSinglePlayer(ViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the OK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {

            string[] args = { game_name_text.Text, rows_text.Text, cols_text.Text };
            this.commands.Invoke(this, new MazeCommand("generate", args));
        }
    }
}
