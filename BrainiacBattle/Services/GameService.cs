using BrainiacBattle.Data;
using BrainiacBattle.DTOs;
using BrainiacBattle.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainiacBattle.Services
{
    public class GameService
    {
        private readonly BrainiacBattleContext _context;
        private readonly AccountService _accountService;

        public GameService(BrainiacBattleContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<List<Games>> GetAllGamesAsync()
        {
            return await  _context.Games.ToListAsync();
        }

        public async Task<Games> GetGameByIdAsync(int gameId)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);
            return game;
        }

        public async Task<Dictionary<string, int>> GetCurrentPlayersAsync(int gameId, int limit)
        {
            return await _context.Accounts
                .Where(a => a.CurrentGameId == gameId)
                .OrderByDescending(a => a.BrainRating)
                .Take(limit)
                .ToDictionaryAsync(a => a.Username, a => a.BrainRating);
        }

        public async Task SubmitResult(ResultDto result)
        {
            var currentGameStatistic = _context.AccountGameStatistics.Single(ags => ags.AccountId == result.AccountId && ags.GameId == result.GameId);
            var currentCategoryStatistic = _context.AccountCategoryStatistics.Single(acs => acs.AccountId == result.AccountId && acs.CategoryId == result.CategoryId);
            var currentAccount = await _accountService.GetAccountByIdAsync(result.AccountId);

            if (result.Score > currentGameStatistic.GameRating)
            {
                currentGameStatistic.GameRating = result.Score;
                _context.AccountGameStatistics.Update(currentGameStatistic);
                await _context.SaveChangesAsync();

                var gameStatisticsByCategory = await _context.AccountGameStatistics
                    .Include(ags => ags.Game)
                    .Where(ags => ags.AccountId == result.AccountId && ags.Game.CategoryId == result.CategoryId).ToListAsync();
                currentCategoryStatistic.CategoryRating = (int)gameStatisticsByCategory.Average(gsbc => gsbc.GameRating);
                _context.AccountCategoryStatistics.Update(currentCategoryStatistic);
                await _context.SaveChangesAsync();

                var categoryStatisticsByAccount = await _context.AccountCategoryStatistics.Where(ags => ags.AccountId == result.AccountId).ToListAsync();
                currentAccount.BrainRating = (int)categoryStatisticsByAccount.Average(csba => csba.CategoryRating);
                _context.Accounts.Update(currentAccount);
                await _context.SaveChangesAsync();
            }
        }

        public bool GameExists(int gameId)
        {
            return _context.Games.Any(g => g.GameId == gameId);
        }

        public bool GameIsInCategory(int gameId, int categoryId)
        {
            return _context.Games.Any(g => g.GameId == gameId && g.CategoryId == categoryId);
        }
    }
}
