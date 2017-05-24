using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientGui
{
    class ViewSendMultiPlayerList : IUpdateCommand
    {
        /// <summary>
        /// Executes the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="window">The window.</param>
        public void Execute(string result, Window window)
        {
            ((MultiPlayerMenu)(window)).ShowMenu();
        }
    }
}
