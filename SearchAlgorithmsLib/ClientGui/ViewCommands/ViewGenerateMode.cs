using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientGui
{
    class ViewGenerateMode : IViewCommand
    {
        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public Window Execute(string result, ViewModel viewModel)
        {
            SinglePlayerWindow window = new SinglePlayerWindow(result, viewModel);
            return window;
        }
    }
}
