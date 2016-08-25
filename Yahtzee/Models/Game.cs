using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Yahtzee.DAL;

namespace Yahtzee.Models
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string GameName { get; set; }
        public DateTime TimeStamp { get; set; }

        public GameStatus Status { get; set; }

        public string PlayerAId { get; set; }
        public virtual User PlayerA { get; set; }

        public string PlayerBId { get; set; }
        public virtual User PlayerB { get; set; }
        
        public int? GameResultAId { get; set; }
        public virtual GameResult GameResultA { get; set; }

        public int? GameResultBId { get; set; }
        public virtual GameResult GameResultB { get; set; }

        public int? ScoreA { get; set; }

        public int? ScoreB { get; set; }

        public string WinnerId { get; set; }
        public virtual  ICollection<ChatMessage> ChatMessages { get; set; }

       

        
    }

   
}