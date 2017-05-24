using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using MazeLib;

namespace ClientGui
{
    public class GenerateCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseConnection"/> class.
        /// </summary>
        public GenerateCommand()
        {
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public RecieveInfo Execute(string[] args, IServerModel model, TcpClient client = null)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            string result = SendAndRecieve.RecieveInfo(reader);
            client.GetStream().Flush();
            client.GetStream().Close();
            client.Close();

            if (!result.Equals("invalid command"))
            {
                // save start and end point
                Maze maze = Maze.FromJSON(result);
                model.CurrentRow = maze.InitialPos.Row;
                model.CurrentCol = maze.InitialPos.Col;
                model.Maze = maze;
                model.GameName = maze.Name;

                model.EndRow = maze.GoalPos.Row;
                model.EndCol = maze.GoalPos.Col;
            }
            return new RecieveInfo(result, false, "generate");
        }
    }
}

