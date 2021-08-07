using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrainiacBattle.Models
{
    public partial class MultiplayerResult
    {
        public MultiplayerResult()
        {
            Results = new HashSet<Results>();
        }

        public int MultiplayerResultId { get; set; }
        public bool IsDraw { get; set; }
        public bool IsP1win { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual ICollection<Results> Results { get; set; }
    }
}
