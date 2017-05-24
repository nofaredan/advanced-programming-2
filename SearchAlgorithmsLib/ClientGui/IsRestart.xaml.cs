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
    /// Interaction logic for IsRestart.xaml
    /// </summary>
    public partial class IsRestart : Window
    {

        private SinglePlayerWindow parentWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsRestart"/> class.
        /// </summary>
        /// <param name="parentWindow">The parent window.</param>
        public IsRestart(SinglePlayerWindow parentWindow)
        {
            InitializeComponent();
            this.parentWindow = parentWindow;
        }

        /// <summary>
        /// Handles the Click event of the Yes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            parentWindow.AnswerRestart("yes");
        }

        /// <summary>
        /// Handles the Click event of the No control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            parentWindow.AnswerRestart("no");
        }
    }
}
