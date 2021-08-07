using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrainiacBattle.Models
{
    public partial class Badges
    {
        public Badges()
        {
            AccountsBadges = new HashSet<AccountsBadges>();
        }

        public int BadgeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgSrc { get; set; }
        public int GameId { get; set; }

        public virtual Games Game { get; set; }
        public virtual ICollection<AccountsBadges> AccountsBadges { get; set; }
    }
}
