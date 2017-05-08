using System;
using System.Collections.Generic;
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
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        string mazeJson;
        public SinglePlayerWindow(string mazeJson)
        {
            this.mazeJson = mazeJson;
            InitializeComponent();
            grid.DrawMaze(mazeJson);
        }

        private void Main_Menu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Solve_Maze_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Restart_Game_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnClickEvent(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    break;

                case Key.Down:
                    break;

                case Key.Left:
                    break;

                case Key.Right:
                    break;
            }
        }
    }
}
