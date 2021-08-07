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
    public class AccountService
    {
        private readonly BrainiacBattleContext _context;

        public AccountService(BrainiacBattleContext context)
        {
            _context = context;
        }

        public async Task<Accounts> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts
                .SingleOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task<Accounts> GetAccountByEmailAsync(string email)
        {
            return await _context.Accounts.SingleOrDefaultAsync(a => a.Email == email);

        }

        public async Task AddAccountWithEmail(string email)
        {
            var account = new Accounts
            {
                Email = email,
                Username = email,
                TotalPlayingTime = 0, // in minutes
                BrainRating = 0,
            };

            var result = await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            var newAccount = await GetAccountByEmailAsync(email);

            foreach(var category in _context.Categories)
                await _context.AccountCategoryStatistics.AddAsync(new AccountCategoryStatistics() { AccountId = newAccount.AccountId, CategoryId = category.CategoryId, CategoryRating = 0,  });

            foreach (var game in _context.Games)
                await _context.AccountGameStatistics.AddAsync(new AccountGameStatistics() { AccountId = newAccount.AccountId, GameId = game.GameId, GameRating = 0,  });

            await _context.SaveChangesAsync();
        }

        public async Task ChangeUsername(Accounts account, string username)
        {
            account.Username = username;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SetCurrentGame(Accounts account, int gameId)
        {
            if (!_context.Games.Any(g => g.GameId == gameId))
                return false;

            account.CurrentGameId = gameId;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task RemoveCurrentGame(Accounts account)
        {
            account.CurrentGameId = null;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task SetStartTime(Accounts account, DateTime startTime)
        {
            account.StartTime = startTime;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTotalPlayingTime(Accounts account, DateTime endTime)
        {
            var minutesToAdd = (int)endTime.Subtract(account.StartTime.Value).TotalMinutes;
            account.TotalPlayingTime += minutesToAdd;
            account.StartTime = null;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public bool UsernameTaken(string username)
        {
            return _context.Accounts.Any(a => a.Username == username);
        }

        public bool AccountExists(int accountId)
        {
            return _context.Accounts.Any(a => a.AccountId == accountId);
        }
    }
}
