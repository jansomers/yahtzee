using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Yahtzee.Hubs
{
    public class AppHub : Hub
    {

        public void GameChange(int id)
        {
            Clients.All.GameChange(id);
        }
      
    }
}