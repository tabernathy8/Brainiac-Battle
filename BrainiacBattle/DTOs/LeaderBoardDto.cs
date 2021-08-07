using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainiacBattle.DTOs
{
    public class LeaderBoardDto
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
