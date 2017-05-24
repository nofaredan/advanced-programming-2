using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MultiPlayerMenu.xaml
    /// </summary>
    public partial class MultiPlayerMenu : Window
    {
        public event EventHandler<MazeCommand> commands;
        private ViewModel viewModel;
        private InvalidCommand waitWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerMenu"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public MultiPlayerMenu(ViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.Send("list");
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Handles the Click event of the Start control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            string[] args = { name_text.Text, rows_text.Text, maze_columns_txt.Text };
            this.commands.Invoke(this, new MazeCommand("start", args));

            start_game.IsEnabled = false;
            waitWindow = new InvalidCommand(viewModel, "waiting for partner");
            waitWindow.ShowDialog();
        }

        /// <summary>
        /// Closings the menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        public void ClosingMenu(object sender, CancelEventArgs e)
        {
            if (waitWindow != null)
            {
                waitWindow.Close();
            }
        }

        /// <summary>
        /// Hides the waiting.
        /// </summary>
        public void HideWaiting()
        {
            start_game.IsEnabled = true;
            waitWindow.message.Content = "invalid command";
        }

        /// <summary>
        /// Shows the menu.
        /// </summary>
        public void ShowMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the Join control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Join_Click(object sender, RoutedEventArgs e)
        {
            string[] args = { multiplayer_list.SelectedItem.ToString() };
            this.commands.Invoke(this, new MazeCommand("join", args));
        }
    }
}
