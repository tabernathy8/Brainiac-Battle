using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrainiacBattle.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Benefits = new HashSet<Benefits>();
            Games = new HashSet<Games>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AccountCategoryStatistics> AccountCategoryStatistics { get; set; }
        public virtual ICollection<Benefits> Benefits { get; set; }
        public virtual ICollection<Games> Games { get; set; }
    }
}
