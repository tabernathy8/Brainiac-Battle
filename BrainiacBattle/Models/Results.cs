using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrainiacBattle.Models
{
    public partial class Results
    {
        public int ResultId { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public int OldGameRating { get; set; }
        public int NewGameRating { get; set; }
        public DateTime DateAdded { get; set; }
        public int GameId { get; set; }
        public int AccountId { get; set; }
        public int? MultiplayerResultId { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Games Game { get; set; }
        public virtual MultiplayerResult MultiplayerResult { get; set; }
    }
}
