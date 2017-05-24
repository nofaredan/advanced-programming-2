using System;
using System.Windows;

namespace ClientGui
{
    public class ViewJoinMode : IViewCommand
    {
        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public Window Execute(string result, ViewModel viewModel)
        {
            MultiPlayerWindow window = new MultiPlayerWindow(result, viewModel);

            return window;
        }
    }
}