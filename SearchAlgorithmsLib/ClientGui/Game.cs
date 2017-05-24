using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Windows.Input;
using Newtonsoft.Json.Linq;

namespace ClientGui
{
    public class Game
    {
        private static bool gameAlive;
        public static TcpClient server { get; set; }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public static void StartGame(IServerModel model)
        {
            gameAlive = true;
            new Task(() =>
            {
                // wait for join
                NetworkStream stream = server.GetStream();
                StreamReader reader = new StreamReader(stream);

                while (gameAlive)
                {

                    string result = SendAndRecieve.RecieveInfo(reader);

                    if (result!="")
                    {
                        JObject jsonResult = JObject.Parse(result);

                        // if it's the end of the game:
                        if ((string)jsonResult[""] == "game closed")
                        {
                            gameAlive = false;
                            model.GameOver();
                            model.InitializeGame();
                        }
                        else
                        {
                            // play one move
                            Play(jsonResult, model);
                        }
                    }
                    else
                    {
                        gameAlive = false;
                        model.GameOver();
                    }

                }

            }).Start();
        }

        /// <summary>
        /// Plays the specified json result.
        /// </summary>
        /// <param name="jsonResult">The json result.</param>
        /// <param name="model">The model.</param>
        private static void Play(JObject jsonResult, IServerModel model)
        {
            string direction = (string)jsonResult["Direction"];
            Key key = GetKeyByString(direction);
            Object[] newRowAndCol = model.GetNewRowAndCol(key, model.OtherCurrentRow, model.OtherCurrentCol);
            int newRow = (int)newRowAndCol[0];
            int newCol = (int)newRowAndCol[1];

            if (newRow < model.MazeRows && newRow >= 0 && newCol < model.MazeRows &&
            newCol >= 0 && model.Maze[newRow, newCol] != MazeLib.CellType.Wall)
            {
                model.OtherCurrentRow = newRow;
                model.OtherCurrentCol = newCol;

                // other player won
                if (newRow == model.Maze.GoalPos.Row && newCol == model.Maze.GoalPos.Col)
                {
                    model.OtherPlayerWon();
                }
            }
        }

        /// <summary>
        /// Gets the key by string.
        /// </summary>
        /// <param name="strKey">The string key.</param>
        /// <returns></returns>
        private static Key GetKeyByString(string strKey)
        {
            Key resultKey = new Key();
            switch (strKey)
            {
                case "right":
                    resultKey = Key.Right;
                    break;
                case "left":
                    resultKey = Key.Left;
                    break;
                case "up":
                    resultKey = Key.Up;
                    break;
                case "down":
                    resultKey = Key.Down;
                    break;
            }
            return resultKey;
        }
    }
}


