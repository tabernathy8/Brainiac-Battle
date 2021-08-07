using BrainiacBattle.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BrainiacBattle.Services
{
    public class LeaderBoardsService
    {
        private readonly BrainiacBattleContext _context;

        public LeaderBoardsService(BrainiacBattleContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, int>> GetCategoryLeaderboard(int categoryId, int limit)
        {
            return await _context.AccountCategoryStatistics
                .Include(acs => acs.Account)
                .Where(acs => acs.CategoryId == categoryId)
                .OrderByDescending(acs => acs.CategoryRating)
                .Take(limit)
                .ToDictionaryAsync(acs => acs.Account.Username, acs => acs.Account.BrainRating);
        }

        public async Task<Dictionary<string, int>> GetGameLeaderboard(int gameId, int limit)
        {
            return await _context.AccountGameStatistics
                .Include(acs => acs.Account)
                .Where(acs => acs.GameId == gameId)
                .OrderByDescending(acs => acs.GameRating)
                .Take(limit)
                .ToDictionaryAsync(acs => acs.Account.Username, acs => acs.Account.BrainRating);
        }

        public async Task<List<string>> GetAllUserNames()
        {
            var userNames = new List<String>();
            var query = from account in _context.Accounts orderby account.BrainRating descending select account.Username;
            var userName = await query.ToArrayAsync();
            var count = 0;
            while (count < userName.Length)
            {
                userNames.Add(userName[count]);
                count++;
            }
            return userNames;
        }

        public async Task<List<int>> GetAllBrainRatings()
        {
            var bRatings = new List<int>();
            var query = from account in _context.Accounts orderby account.BrainRating descending select account.BrainRating;
            var rating = await query.ToArrayAsync();
            var count = 0;
            while (count < rating.Length)
            {
                bRatings.Add(rating[count]);
                count++;
            }
            return bRatings;
        }

        public async Task<DataTable> GetOverallLeaderBoard()
        {
            int count = 1;
            int iterator = 0;
            List<string> userNames = await GetAllUserNames();
            List<int> brainRatings = await GetAllBrainRatings();

            DataTable lBoard = new DataTable();
            lBoard.Columns.Add("Rank", typeof(int));
            lBoard.Columns.Add("Name", typeof(string));
            lBoard.Columns.Add("Score", typeof(int));

            while (iterator < userNames.Count())
            {
                DataRow row = lBoard.NewRow();
                row["Rank"] = count;
                row["Name"] = userNames[iterator].ToString();
                row["Score"] = brainRatings[iterator].ToString();
                lBoard.Rows.Add(row);
                count++;
                iterator++;
            }
            return lBoard;
        }

        public async Task<string> GetUserRank(string userName)
        {
            DataRow[] uRow = (await GetOverallLeaderBoard()).Select("Name = " + userName);
            string userRank = uRow[0].ToString();
            return userRank;
        }
    }
}
