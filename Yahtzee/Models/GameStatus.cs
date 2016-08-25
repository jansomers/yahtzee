using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yahtzee.Models
{
    public enum GameStatus
    {
        Making, Created, Accepted, AIsPlaying, BIsPlaying, Ended, Finished
    }
}