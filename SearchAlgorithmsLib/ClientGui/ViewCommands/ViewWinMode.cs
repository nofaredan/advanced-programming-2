﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientGui
{
    class ViewWinMode : IViewCommand
    {
        /// <summary>
        /// Executes the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public Window Execute(string e, ViewModel viewModel)
        {
            Window window = new WinSinglePlayer();
            return window;
        }
    }
}
