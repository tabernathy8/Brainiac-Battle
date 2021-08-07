using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainiacBattle.DTOs
{
    public class Statistics
    {
        public int Rating { get; set; }
        public int Rank { get; set; }
        public int Percentile { get; set; }
    }

    public class AccountStatistics : Statistics
    {
        public string BestSkill { get; set; }
        public string NeedsImprovement { get; set; }
        public int TotalPlaytime { get; set; }
    }
}
