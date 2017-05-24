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
        private Rectangle playerImg;
        private Rectangle exitImg;
        private ViewModel viewModel;
        private bool isMainPlayer;
        public DependencyProperty CurrentPosition { get; set; }
        private bool isSinglePlayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridSinglePlayer"/> class.
        /// </summary>
        public GridSinglePlayer()
        {
            InitializeComponent();
            grid = new Grid();
            gridHeight = this.Height;
            gridWidth = this.Width;
            Content = grid;
        }

        /// <summary>
        /// Draws the maze.
        /// </summary>
        /// <param name="jsonMaze">The json maze.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="isSinglePlayer">if set to <c>true</c> [is single player].</param>
        /// <param name="isMainPlayer">if set to <c>true</c> [is main player].</param>
        public void DrawMaze(string jsonMaze, ViewModel viewModel, bool isSinglePlayer = true, bool isMainPlayer = true)
        {
            this.isSinglePlayer = isSinglePlayer;
            this.isMainPlayer = isMainPlayer;
            this.viewModel = viewModel;
            maze = Maze.FromJSON(jsonMaze);

            CreateRows();
            CreateColumns();

            // draw maze:
            for (int row = 0; row < maze.Rows; row++)
            {
                for (int col = 0; col < maze.Cols - 1; col++)
                {

                    Rectangle rectangle = DrawRectangle(row, col);
                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, col);
                    grid.Children.Add(rectangle);
                }
            }

            BindPlayer();
        }

        /// <summary>
        /// Binds the player.
        /// </summary>
        private void BindPlayer()
        {
            string path;
            playerImg = new Rectangle();

            if (isMainPlayer)
            {
                path = "pack://application:,,,/ClientGui;component/pics/bob.jpg";
                Bind(path, "VM_CurrentRow", "VM_CurrentCol");
            }
            else
            {
                path = "pack://application:,,,/ClientGui;component/pics/purple.jpg";
                Bind(path, "VM_OtherCurrentRow", "VM_OtherCurrentCol");
            }

            BitmapImage exitBitMap = new BitmapImage(new Uri(path));
            ImageBrush brush = new ImageBrush(exitBitMap);
            playerImg.Fill = brush;

            this.grid.Children.Add(playerImg);
        }

        /// <summary>
        /// Binds the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="rowPropertyName">Name of the row property.</param>
        /// <param name="colPropertyName">Name of the col property.</param>
        private void Bind(string path, string rowPropertyName, string colPropertyName)
        {
            Binding bindingRow = new Binding();
            bindingRow.Path = new PropertyPath(rowPropertyName);
            BindingOperations.SetBinding(playerImg, Grid.RowProperty, bindingRow);

            Binding bindingCol = new Binding();
            bindingCol.Path = new PropertyPath(colPropertyName);
            BindingOperations.SetBinding(playerImg, Grid.ColumnProperty, bindingCol);
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns></returns>
        private Rectangle DrawRectangle(int row, int col)
        {
            double heightRec = gridHeight / maze.Rows;
            double widthRec = gridWidth / maze.Cols;

            Rectangle rectangle = new Rectangle();
            rectangle.Height = heightRec;
            rectangle.Width = widthRec;

            if (col == maze.GoalPos.Col && row == maze.GoalPos.Row)
            {
                BitmapImage exitBitMap = new BitmapImage(new Uri("pack://application:,,,/ClientGui;component/pics/exit.jpg"));
                ImageBrush brush = new ImageBrush(exitBitMap);
                rectangle.Fill = brush;
                exitImg = rectangle;

            }
            else if (maze[row, col] == MazeLib.CellType.Wall)
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

        /// <summary>
        /// Creates the columns.
        /// </summary>
        private void CreateColumns()
        {
            // Create Columns
            for (int i = 0; i < maze.Cols; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                grid.ColumnDefinitions.Add(col);
            }
        }

        /// <summary>
        /// Creates the rows.
        /// </summary>
        private void CreateRows()
        {
            // Create rows
            for (int i = 0; i < maze.Rows; i++)
            {
                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
            }
        }

        /// <summary>
        /// Called when [click event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void OnClickEvent(object sender, KeyEventArgs e)
        {
            MovePlayer(e.Key);
            e.Handled = true;
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="key">The key.</param>
        public void MovePlayer(Key key)
        {
            this.Dispatcher.Invoke(() =>
            {
                viewModel.MovePlayer(key, isSinglePlayer);
                this.grid.Children.Remove(playerImg);
                this.grid.Children.Add(playerImg);
            });
        }

        /// <summary>
        /// Moves the player solve.
        /// </summary>
        /// <param name="key">The key.</param>
        public void MovePlayerSolve(Key key)
        {
            this.Dispatcher.Invoke(() =>
            {
                viewModel.MovePlayerSolve(key);
                this.grid.Children.Remove(playerImg);
                this.grid.Children.Add(playerImg);
            });
        }

        /// <summary>
        /// Restarts the player.
        /// </summary>
        public void RestartPlayer()
        {
            viewModel.RestartPlayer();
            this.grid.Children.Remove(playerImg);
            this.grid.Children.Add(playerImg);
        }


    }
}