using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrainiacBattle.Data;
using BrainiacBattle.Models;
using BrainiacBattle.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using BrainiacBattle.DTOs;
using System.Collections.Generic;

namespace BrainiacBattle.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly LeaderBoardsService _leaderboardService;

        public AccountController(AccountService accountService, LeaderBoardsService leaderBoardService)
        {
            _accountService = accountService;
            _leaderboardService = leaderBoardService;
        }

        // GET: api/Account/Get/Id/{accountId}
        [HttpGet("Get/Id/{accountId}")]
        public async Task<ActionResult<AccountDto>> GetAccount(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);

            if (account == null)
            {
                return NotFound();
            }

            return ConvertToAccountDto(account);
        }

        // GET: api/Account/Get/Email/{email}
        [HttpGet("Get/Email/{email}")]
        public async Task<ActionResult<AccountDto>> GetAccount(string email)
        {
            var account = await _accountService.GetAccountByEmailAsync(email);

            if (account == null)
                return NotFound();

            return ConvertToAccountDto(account);
        }

        // POST: api/Account/ChangeUsername/{AccountId}/{username}
        [HttpPost("ChangeUsername/{accountId}/{username}")]
        public async Task<ActionResult> ChangeUsername(int accountId, string username)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);

            if (account == null)
                return NotFound();

            await _accountService.ChangeUsername(account, username);

            return Ok();
        }

        // POST: api/Account/SetCurrentGame/{accountId}/{gameId}
        [HttpPost("SetCurrentGame/{accountId}/{gameId}")]
        public async Task<ActionResult> SetCurrentGame(int accountId, int gameId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);

            if (account == null)
                return NotFound();

            var success = await _accountService.SetCurrentGame(account, gameId);

            if (!success)
                return NotFound();

            return Ok();
        }

        // POST: api/Account/RemoveCurrentGame/{accountId}
        [HttpPost("RemoveCurrentGame/{accountId}")]
        public async Task<ActionResult> RemoveCurrentGame(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);

            if (account == null)
                return NotFound();

            await _accountService.RemoveCurrentGame(account);

            return Ok();
        }

        // POST: api/Account/SetStartTime/{accountId}
        [HttpPost("SetStartTime/{accountId}")]
        public async Task<ActionResult> SetStartTime([FromBody] DateTime startTime, int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);

            if (account == null)
                return NotFound();

            await _accountService.SetStartTime(account, startTime);

            return Ok();
        }

        // POST: api/Account/UpdateTotalPlayingTime/{accountId}
        [HttpPost("UpdateTotalPlayingTime/{accountId}")]
        public async Task<ActionResult> UpdateTotalPlayingTime([FromBody] DateTime endTime, int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);

            if (account == null || account.StartTime == null)
                return NotFound();

            await _accountService.UpdateTotalPlayingTime(account, endTime);

            return Ok();
        }

        // POST: api/Account/Get/Leaderboard/{numEntries}
        [HttpGet("Get/Leaderboard/{numEntries}")]
        public async Task<ActionResult<List<LeaderBoardDto>>> GetLeaderboard(int numEntries)
        {
            var leaderBoardData = new List<LeaderBoardDto>();
            var leaderBoard = (await _leaderboardService.GetOverallLeaderBoard()).Select();
            if (numEntries > leaderBoard.Length)
                numEntries = leaderBoard.Length;
            for (var i = 0; i < numEntries; i++)
            {
                var entry = new LeaderBoardDto()
                {
                    Rank = (int)leaderBoard[i]["Rank"],
                    Name = (string)leaderBoard[i]["Name"],
                    Score = (int)leaderBoard[i]["Score"]
                };
                leaderBoardData.Add(entry);
            }
            
            return Ok(leaderBoardData);
        }

        private AccountDto ConvertToAccountDto(Accounts account)
        {
            return new AccountDto
            {
                AccountId = account.AccountId,
                Username = account.Username,
                Email = account.Email,
                BrainRating = account.BrainRating,
                CurrentGameId = account.CurrentGameId,
                StartTime = account.StartTime,
                TotalPlayingTime = account.TotalPlayingTime
            };
        }
    }
}
