using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yahtzee.Models
{
    public class ChatMessage
    {

        public int Id { get; set; }
        public string ScreenName { get; set; }

        public string Message { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}