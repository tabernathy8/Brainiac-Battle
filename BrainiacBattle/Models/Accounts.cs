using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrainiacBattle.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            AccountGameStatistics = new HashSet<AccountGameStatistics>();
            AccountsBadges = new HashSet<AccountsBadges>();
            Results = new HashSet<Results>();
        }

        public int AccountId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int BrainRating { get; set; }
        public int? CurrentGameId { get; set; }
        public int TotalPlayingTime { get; set; }
        public DateTime? StartTime { get; set; }

        public virtual Games Game { get; set; }

        public virtual ICollection<AccountGameStatistics> AccountGameStatistics { get; set; }
        public virtual ICollection<AccountCategoryStatistics> AccountCategoryStatistics { get; set; }
        public virtual ICollection<AccountsBadges> AccountsBadges { get; set; }
        public virtual ICollection<Results> Results { get; set; }
    }
}
