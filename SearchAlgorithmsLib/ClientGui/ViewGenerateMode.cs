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
        public Window Execute(string e)
        {
            SinglePlayerWindow window = new SinglePlayerWindow(e);
            
            return window;
        }
    }
}
