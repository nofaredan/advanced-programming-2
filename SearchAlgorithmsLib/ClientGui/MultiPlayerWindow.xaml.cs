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
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {
        private ViewModel viewModel;
        private string jsonMaze;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerWindow"/> class.
        /// </summary>
        /// <param name="jsonMaze">The json maze.</param>
        /// <param name="viewModel">The view model.</param>
        public MultiPlayerWindow(string jsonMaze, ViewModel viewModel)
        {
            InitializeComponent();
            this.jsonMaze = jsonMaze;
            this.viewModel = viewModel;
            grid.DataContext = viewModel;
            other_grid.DataContext = viewModel;
            grid.DrawMaze(jsonMaze, viewModel, false, true);
            grid.Focus();
            other_grid.DrawMaze(jsonMaze, viewModel, true, false);
        }

        /// <summary>
        /// Called when [closing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private void OnClosing(object sender, CancelEventArgs e)
        {
            viewModel.Send("close " + viewModel.VM_GameName);
            viewModel.InitializeGame();
        }

        /// <summary>
        /// Backs to main window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BackToMainWindow(object sender, RoutedEventArgs e)
        {
            viewModel.Send("close " + viewModel.VM_GameName);
            this.Close();
            viewModel.InitializeGame();
        }
    }
}
