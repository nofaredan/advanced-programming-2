using MazeLib;
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
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;
using System.Timers;

namespace ClientGui
{
    /// <summary>
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        private string mazeJson;
        private ViewModel viewModel;
        int tick;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerWindow"/> class.
        /// </summary>
        /// <param name="mazeJson">The json.</param>
        /// <param name="viewModel">The view model.</param>
        public SinglePlayerWindow(string mazeJson, ViewModel viewModel)
        {
            InitializeComponent();
            this.mazeJson = mazeJson;
            this.viewModel = viewModel;
            grid.Focus();
            grid.DrawMaze(mazeJson, viewModel);
        }

        /// <summary>
        /// Handles the Click event of the Main_Menu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Main_Menu_Click(object sender, RoutedEventArgs e)
        {
            viewModel.InitializeGame();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the Solve_Maze control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Solve_Maze_Click(object sender, RoutedEventArgs e)
        {
            tick = 0;
            string message = "solve " + Maze.FromJSON(mazeJson).Name + " " + viewModel.VM_SearchAlgorithm;
            solve_button.IsEnabled = false;
            restart_game_button.IsEnabled = false;
            main_manu_button.IsEnabled = false;
            viewModel.Send(message);
        }

        /// <summary>
        /// Handles the Click event of the Restart_Game control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Restart_Game_Click(object sender, RoutedEventArgs e)
        {
            IsRestart window = new IsRestart(this);
            window.ShowDialog();
        }

        /// <summary>
        /// Answers the restart.
        /// </summary>
        /// <param name="command">The command.</param>
        public void AnswerRestart(string command)
        {
            if (command.Equals("yes"))
            {
                grid.RestartPlayer();
                solve_button.IsEnabled = true;
            }
            this.Focus();
            this.Focusable = true;
            grid.Focusable = true;
            grid.Focus();
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="resultJson">The result json.</param>
        public void SolveMaze(string resultJson)
        {
            Task task = new Task(() =>
            {
                JObject json = JObject.Parse(resultJson);
                string solution = (string)json["Solution"];
                char[] letters = solution.ToCharArray();
                Timer timer = new Timer(100);

                timer.Elapsed += (sender, e) => MyElapsedMethod(sender, e, letters);
                timer.AutoReset = true;
                timer.Enabled = true;
            });
            task.Start();
        }

        /// <summary>
        /// Mies the elapsed method.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        /// <param name="letters">The letters.</param>
        private void MyElapsedMethod(Object source, ElapsedEventArgs e, char[] letters)
        {
            // if not over:
            if (tick < letters.Length)
            {
                // move to one of the directions:
                switch (letters[tick])
                {
                    case '0':
                        grid.MovePlayerSolve(Key.Left);
                        break;
                    case '1':
                        grid.MovePlayerSolve(Key.Right);
                        break;
                    case '2':
                        grid.MovePlayerSolve(Key.Up);
                        break;
                    case '3':
                        grid.MovePlayerSolve(Key.Down);
                        break;
                }
                tick++;
            }
            else
            {
                ((Timer)source).Dispose();
                this.Dispatcher.Invoke(() =>
                {
                    restart_game_button.IsEnabled = true;
                    main_manu_button.IsEnabled = true;
                });
            }
        }
    }
}
