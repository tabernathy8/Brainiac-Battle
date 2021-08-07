using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrainiacBattle.Models
{
    public partial class AccountGameStatistics
    {
        public int AccountGameStatisticId { get; set; }
        public int? HighestScore { get; set; }
        public int? HighestLevel { get; set; }
        public int? Wins { get; set; }
        public int? Draws { get; set; }
        public int? Losses { get; set; }
        public int AccountId { get; set; }
        public int GameId { get; set; }
        public int GameRating { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Games Game { get; set; }
    }
}
