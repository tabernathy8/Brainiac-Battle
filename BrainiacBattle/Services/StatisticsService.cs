using BrainiacBattle.Data;
using BrainiacBattle.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainiacBattle.Services
{
    public class StatisticsService
    {
        private readonly BrainiacBattleContext _context;
        private readonly CategoryService _categoryService;

        public StatisticsService(BrainiacBattleContext context, CategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        public async Task<AccountStatistics> GetAccountStatisticsAsync(int accountId)
        {
            var accounts = await _context.Accounts
                .Include(a => a.AccountCategoryStatistics)
                .OrderByDescending(a => a.BrainRating).ToListAsync();

            var account = accounts.SingleOrDefault(a => a.AccountId == accountId);

            if (account == null)
                return null;

            var rank = accounts.IndexOf(account) + 1;
            var totalAccounts = accounts.Count;

            var maxCategoryRating = account.AccountCategoryStatistics.Max(acs => acs.CategoryRating);
            var maxCategoryStatistic = account.AccountCategoryStatistics.First(acs => acs.CategoryRating == maxCategoryRating);
            var maxCategory = await _categoryService.GetCategoryByIdAsync(maxCategoryStatistic.CategoryId);

            var minCategoryRating = account.AccountCategoryStatistics.Min(acs => acs.CategoryRating);
            var minCategoryStatistic = account.AccountCategoryStatistics.First(acs => acs.CategoryRating == maxCategoryRating);
            var minCategory = await _categoryService.GetCategoryByIdAsync(maxCategoryStatistic.CategoryId);

            return new AccountStatistics
            {
                Rating = account.BrainRating,
                Rank = rank,
                Percentile = totalAccounts / rank,
                TotalPlaytime = account.TotalPlayingTime,
                BestSkill = maxCategory.Name,
                NeedsImprovement = minCategory.Name
            };
        }

        public async Task<Statistics> GetCategoryStatisticsAsync(int accountId, int categoryId)
        {
            var categoryStatistic = await _context.AccountCategoryStatistics
                .SingleOrDefaultAsync(acs => acs.AccountId == accountId && acs.CategoryId == categoryId);

            if (categoryStatistic == null)
                return null;

            var specificCategoryStatistics = await _context.AccountCategoryStatistics
                .Where(acs => acs.CategoryId == categoryId)
                .OrderByDescending(acs => acs.CategoryRating).ToListAsync();

            var rank = specificCategoryStatistics.IndexOf(categoryStatistic) + 1;
            var totalAccounts = specificCategoryStatistics.Count;

            return new Statistics
            {
                Rating = categoryStatistic.CategoryRating,
                Rank = rank,
                Percentile = (int)rank / totalAccounts * 100
            };
        }

        public async Task<Statistics> GetGameStatisticsAsync(int accountId, int gameId)
        {
            var gameStatistic = await _context.AccountGameStatistics
                .SingleOrDefaultAsync(acs => acs.AccountId == accountId && acs.GameId == gameId);

            if (gameStatistic == null)
                return null;

            var specificGameStatistics = await _context.AccountGameStatistics
                .Where(acs => acs.GameId == gameId)
                .OrderByDescending(acs => acs.GameRating).ToListAsync();

            var rank = specificGameStatistics.IndexOf(gameStatistic) + 1;
            var totalAccounts = specificGameStatistics.Count;

            return new Statistics
            {
                Rating = gameStatistic.GameRating,
                Rank = rank,
                Percentile = totalAccounts / rank
            };
        }
    }
}
