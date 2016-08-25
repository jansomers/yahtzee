using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yahtzee.Models
{
    public class GameCreationDTO
    {
        public string Id { get; set; }
        public string PlayerA { get; set; }
        public string PlayerB { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsCreated { get; set; }
    }
}