
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Windows.Input;

namespace ClientGui
{
    public interface IServerModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>
        /// The server ip.
        /// </value>
        string ServerIP { get; set; }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server port.
        /// </value>
        int ServerPort { get; set; }

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        int MazeRows { get; set; }

        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>
        /// The maze cols.
        /// </value>
        int MazeCols { get; set; }

        /// <summary>
        /// Gets or sets the search algorithm.
        /// </summary>
        /// <value>
        /// The search algorithm.
        /// </value>
        int SearchAlgorithm { get; set; }

        /// <summary>
        /// Gets or sets the current row.
        /// </summary>
        /// <value>
        /// The current row.
        /// </value>
        int CurrentRow { get; set; }

        /// <summary>
        /// Gets or sets the current col.
        /// </summary>
        /// <value>
        /// The current col.
        /// </value>
        int CurrentCol { get; set; }

        /// <summary>
        /// Gets or sets the other current row.
        /// </summary>
        /// <value>
        /// The other current row.
        /// </value>
        int OtherCurrentRow { get; set; }

        /// <summary>
        /// Gets or sets the other current col.
        /// </summary>
        /// <value>
        /// The other current col.
        /// </value>
        int OtherCurrentCol { get; set; }

        /// <summary>
        /// Gets or sets the end row.
        /// </summary>
        /// <value>
        /// The end row.
        /// </value>
        int EndRow { get; set; }

        /// <summary>
        /// Gets or sets the end col.
        /// </summary>
        /// <value>
        /// The end col.
        /// </value>
        int EndCol { get; set; }

        /// <summary>
        /// Games the over.
        /// </summary>
        void GameOver();

        /// <summary>
        /// Others the player won.
        /// </summary>
        void OtherPlayerWon();


        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        Maze Maze { get; set; }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>
        /// The name of the game.
        /// </value>
        string GameName { get; set; }

        /// <summary>
        /// Gets or sets the game multi player list.
        /// </summary>
        /// <value>
        /// The game multi player list.
        /// </value>
        List<string> GameMultiPlayerList { get; set; }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="isSinglePlayer">if set to <c>true</c> [is single player].</param>
        void MovePlayer(Key key, bool isSinglePlayer);

        /// <summary>
        /// Initializes the game.
        /// </summary>
        void InitializeGame();

        /// <summary>
        /// Saves the settings.
        /// </summary>
        void SaveSettings();

        /// <summary>
        /// Cancels the settings.
        /// </summary>
        void CancelSettings();

        /// <summary>
        /// Occurs when [recive from server].
        /// </summary>
        event EventHandler<Recive> reciveFromServer;

        /// <summary>
        /// Occurs when [update from server].
        /// </summary>
        event EventHandler<Recive> updateFromServer;

        /// <summary>
        /// Occurs when [connection].
        /// </summary>
        event EventHandler<Recive> connection;

        /// <summary>
        /// Occurs when [initialize event].
        /// </summary>
        event System.EventHandler<Recive> initializeEvent;

        /// <summary>
        /// Connects the client.
        /// </summary>
        void ConnectClient();

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Send(string message);

        /// <summary>
        /// Restarts the player.
        /// </summary>
        void RestartPlayer();

        /// <summary>
        /// Moves the player solve.
        /// </summary>
        /// <param name="keyCode">The key code.</param>
        void MovePlayerSolve(Key keyCode);

        /// <summary>
        /// Gets the new row and col.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="currentRowPlace">The current row place.</param>
        /// <param name="currentColPlace">The current col place.</param>
        /// <returns></returns>
        Object[] GetNewRowAndCol(Key key, int currentRowPlace, int currentColPlace);
    }
}
