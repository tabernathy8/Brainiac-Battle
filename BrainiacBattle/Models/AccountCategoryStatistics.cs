using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainiacBattle.Models
{
    public partial class AccountCategoryStatistics
    {
        public int AccountCategoryStatisticId { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public int CategoryRating { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Categories Category { get; set; }
    }
}
