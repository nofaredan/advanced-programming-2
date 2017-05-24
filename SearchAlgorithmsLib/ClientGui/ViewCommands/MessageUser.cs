using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientGui
{
    class MessageUser : IViewCommand
    {
        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public Window Execute(string result, ViewModel viewModel)
        {
            InvalidCommand windowInvalid = new InvalidCommand(viewModel, result);
            windowInvalid.ShowDialog();
            return windowInvalid;
        }
    }
}
