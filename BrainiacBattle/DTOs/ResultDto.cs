using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainiacBattle.DTOs
{
    public class ResultDto
    {
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public int GameId { get; set; }
        public int Score { get; set; }
    }
}
