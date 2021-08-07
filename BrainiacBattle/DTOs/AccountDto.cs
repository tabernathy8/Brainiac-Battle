using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainiacBattle.DTOs
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int BrainRating { get; set; }
        public int? CurrentGameId { get; set; }
        public int TotalPlayingTime { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
