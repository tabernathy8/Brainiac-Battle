using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrainiacBattle.Models
{
    public partial class Benefits
    {
        public int BenefitId { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public int CategoryId { get; set; }

        public virtual Categories Category { get; set; }
    }
}
