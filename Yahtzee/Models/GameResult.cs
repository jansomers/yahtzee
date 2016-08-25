using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yahtzee.Models
{
    public class GameResult
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameResultId { get; set; }

        public char Player { get; set; }

        public int Ones { get; set; }
        public int Twos { get; set; }
        public int Threes { get; set; }
        public int Fours { get; set; }
        public int Fives { get; set; }
        public int Sixes { get; set; }
        public int Tok { get; set; }
        public int Fok { get; set; }
        public int Fh { get; set; }
        public int SmStr { get; set; }
        public int LaStr { get; set; }
        public int Yahtzee { get; set; }
        public int Chance { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        
        
    }
}