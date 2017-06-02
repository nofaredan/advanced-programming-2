﻿using MazeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    class StartCommand : ICommand
    {
        private TcpClient client;
        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="model"></param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public RecieveInfo Execute(string[] args, IServerModel model, TcpClient client = null)
        {
            this.client = client;
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            string result = SendAndRecieve.RecieveInfo(reader);
            client.GetStream().Flush();

            // the first finished before the other joined
            if (result.Equals("aborted"))
            {
                return new RecieveInfo("close", false, "close");
            }
            // if the message is invalid
            else if (!result.Equals("invalid command"))
            {
                // save start and end point
                Maze maze = Maze.FromJSON(result);
                model.CurrentRow = maze.InitialPos.Row;
                model.CurrentCol = maze.InitialPos.Col;
                model.OtherCurrentRow = maze.InitialPos.Row;
                model.OtherCurrentCol = maze.InitialPos.Col;
                model.Maze = maze;
                model.GameName = maze.Name;

                model.EndRow = maze.GoalPos.Row;
                model.EndCol = maze.GoalPos.Col;

                Game.server = client;
                Game.StartGame(model);
            }
            return new RecieveInfo(result, true, "start");
        }

        public void Abort(string gameName)
        {
            SendAndRecieve.Send(new StreamWriter(client.GetStream()),"close "+gameName);
        }
    }
}
