using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrainiacBattle.Models
{
    public partial class AccountsBadges
    {
        public int AccountBadgeId { get; set; }
        public int AccountId { get; set; }
        public int BadgeId { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Badges Badge { get; set; }
    }
}
