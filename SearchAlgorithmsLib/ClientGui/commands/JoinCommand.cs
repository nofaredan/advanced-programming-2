using MazeLib;
using System;
using System.IO;
using System.Net.Sockets;

namespace ClientGui
{
    internal class JoinCommand : ICommand
    {
        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="model"></param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public RecieveInfo Execute(string[] args, IServerModel model, TcpClient client = null)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            string result = SendAndRecieve.RecieveInfo(reader);
            client.GetStream().Flush();

            if (!result.Equals("invalid command"))
            {
                // save start and end point
                Maze maze = Maze.FromJSON(result);
                model.CurrentRow = maze.InitialPos.Row;
                model.CurrentCol = maze.InitialPos.Col;
                model.OtherCurrentRow = maze.InitialPos.Row;
                model.OtherCurrentCol = maze.InitialPos.Col;
                model.Maze = maze;
                model.GameName = maze.Name;

                // update end point
                model.EndRow = maze.GoalPos.Row;
                model.EndCol = maze.GoalPos.Col;
            }
            Game.server = client;
            Game.StartGame(model);
            return new RecieveInfo(result, true, "join");
        }
    }
}