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
    /// Interaction logic for InvalidCommand.xaml
    /// </summary>
    public partial class InvalidCommand : Window
    {
        private ViewModel viewModel;
        private string result;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommand"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="result">The result.</param>
        public InvalidCommand(ViewModel viewModel, string result)
        {
            this.result = result;
            this.viewModel = viewModel;
            InitializeComponent();
            this.message.Content = result;
        }

    }
}
