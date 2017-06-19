using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebMazeGame
{
    public class MultiHub : Hub
    {
        public void Connect(string gameName)
        {
           bool isGameStarted = Model.AddMultiPlayer(gameName, Context.ConnectionId);

            if (isGameStarted)
            {
                // send both players to start
                string recipientId = Model.GetOpponent(gameName, Context.ConnectionId);

                Clients.Client(recipientId).gotMessage("start");

                // sent to joined (self)
                Clients.Client(Context.ConnectionId).gotMessage("start");
            }
        }

        public void SendMessage(string gameName, string keyMove)
        {
            string recipientId = Model.GetOpponent(gameName, Context.ConnectionId);
            if (recipientId == null)
                return;

            // send move
            Clients.Client(recipientId).gotMessage(keyMove);
        }
    }
}