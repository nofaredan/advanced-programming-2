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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MazeLib;
//using Newtonsoft.Json;

namespace ClientGui
{
    /// <summary>
    /// Interaction logic for GridSinglePlayer.xaml
    /// </summary>
    public partial class GridSinglePlayer : UserControl
    {
        private Maze maze;
        private Grid grid;
        private double gridHeight;
        private double gridWidth;
        private Image playerImg;
        private Image exitImg;

        public GridSinglePlayer()
        {
            InitializeComponent();

            grid = new Grid();
            gridHeight = this.Height;
            gridWidth = this.Width;

            Content = grid;

         //   ImageSource imgSource = new BitmapImage(new Uri("/pics/bob.jpg", UriKind.Relative));
            
            playerImg = new Image();
            playerImg.Source = new BitmapImage(new Uri("/ClientGui;pics/bob.jpg", UriKind.Relative));

            exitImg = new Image();
            playerImg.Source = new BitmapImage(new Uri("/ClientGui;pics/exit.jpg", UriKind.Relative));
        }

        public void DrawMaze(string jsonMaze)
        {
            maze = Maze.FromJSON(jsonMaze);

            CreateRows();
            CreateColumns();

            // draw maze:
            for (int row = 0; row < maze.Rows; row++)
            {
                for (int col = 0; col < maze.Cols; col++)
                {
                    Rectangle rectangle = DrawRectangle(row, col);
                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, col);
                    grid.Children.Add(rectangle);
                }
            }

            //DrawPlayer();
            DrawGoal();
        }

        private void DrawGoal()
        {
            double heightRec = gridHeight / maze.Rows;
            double widthRec = gridWidth / maze.Cols;

            Rectangle exitRec = new Rectangle();
            BitmapImage exitBitMap = new BitmapImage(new Uri("pack://application:,,,/ClientGui;component/pics/bob.jpg"));

            ImageBrush brush = new ImageBrush(exitBitMap);
            exitRec.Fill = brush;

            //exitRec.Height = heightRec;
            //exitRec.Width = widthRec;
            exitRec.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(exitRec, maze.GoalPos.Row);
            Grid.SetColumn(exitRec, maze.GoalPos.Col);

            grid.Children.Add(exitRec);

            
        }

        private void DrawPlayer()
        {
            double heightRec = gridHeight / maze.Rows;
            double widthRec = gridWidth / maze.Cols;

            playerImg.Height = heightRec;
            playerImg.Width = widthRec;
            playerImg.VerticalAlignment = VerticalAlignment.Center;

            // ----------> SET PLAYER PLACE BY DATA BINDING
            /*Binding bindingCurrImgRow = new Binding();
            bindingCurrImgRow.Path = new PropertyPath("VM_CurrRow");
            BindingOperations.SetBinding(playerImg, Grid.RowProperty, bindingCurrImgRow);
            Binding bindingCurrImgCol = new Binding();
            bindingCurrImgCol.Path = new PropertyPath("VM_CurrCol");
            BindingOperations.SetBinding(playerImg,Grid.ColumnProperty, bindingCurrImgCol);
            */

            // add to screen
            //  grid.Children.Add(playerImg);
        }

        private Rectangle DrawRectangle(int row, int col)
        {
            double heightRec = gridHeight / maze.Rows;
            double widthRec = gridWidth / maze.Cols;

            Rectangle rectangle = new Rectangle();
            rectangle.Height = heightRec;
            rectangle.Width = widthRec;

            if (maze[row, col] == MazeLib.CellType.Wall)
            {
                // wall
                rectangle.Fill = new SolidColorBrush(Colors.Black);
            }
            else
            {
                // free
                rectangle.Fill = new SolidColorBrush(Colors.LightGray);
            }

            return rectangle;
        }

        private void CreateColumns()
        {
            // Create Columns
            for (int i = 0; i < maze.Cols; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                grid.ColumnDefinitions.Add(col);
            }
        }

        private void CreateRows()
        {
            // Create rows
            for (int i = 0; i < maze.Rows; i++)
            {
                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
            }
        }
    }
}
