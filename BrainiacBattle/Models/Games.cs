using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrainiacBattle.Models
{
    public partial class Games
    {
        public Games()
        {
            AccountGameStatistics = new HashSet<AccountGameStatistics>();
            Badges = new HashSet<Badges>();
            Results = new HashSet<Results>();
        }

        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgSrc { get; set; }
        public int CategoryId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<AccountGameStatistics> AccountGameStatistics { get; set; }
        public virtual ICollection<Badges> Badges { get; set; }
        public virtual ICollection<Results> Results { get; set; }
    }
}
