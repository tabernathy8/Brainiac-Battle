using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrainiacBattle.Data;
using BrainiacBattle.Models;
using BrainiacBattle.Services;
using BrainiacBattle.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace BrainiacBattle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly GameService _gameService;
        private readonly CategoryService _categoryService;
        private readonly LeaderBoardsService _leaderboardService;


        public GameController(GameService gameService, AccountService accountService, CategoryService categoryService, LeaderBoardsService leaderBoardService)
        {
            _gameService = gameService;
            _accountService = accountService;
            _categoryService = categoryService;
            _leaderboardService = leaderBoardService;
        }

        // GET: api/Game/Get/All
        [HttpGet("Get/All")]
        public async Task<ActionResult<List<GameDto>>> GetGames()
        {
            var games = await _gameService.GetAllGamesAsync();

            if (games == null)
                return NotFound();

            return games.Select(g => ConvertToGameDto(g)).ToList();
        }

        // GET: api/Game/Get/{gameId}
        [HttpGet("Get/{gameId}")]
        public async Task<ActionResult<GameDto>> GetGame(int gameId)
        {
            var game = await _gameService.GetGameByIdAsync(gameId);

            if (game == null)
                return NotFound();

            return ConvertToGameDto(game);
        }

        // GET: api/Game/Get/{gameId}/CurrentPlayers/{limit}
        [HttpGet("Get/{gameId}/CurrentPlayers/{limit}")]
        public async Task<ActionResult<Dictionary<string, int>>> GetCurrentPlayers(int gameId, int limit)
        {
            if (!_gameService.GameExists(gameId))
                return NotFound();

            var currentPlayers = await _gameService.GetCurrentPlayersAsync(gameId, limit);

            if (currentPlayers == null)
                return NotFound();

            return currentPlayers;
        }

        // POST: api/Game/Add/Result
        [HttpPost("Add/Result")]
        public async Task<ActionResult> AddGameResult([FromBody] ResultDto result)
        {
            if (!_gameService.GameExists(result.GameId) || !_accountService.AccountExists(result.AccountId) || !_categoryService.CategoryExists(result.CategoryId))
                return NotFound();

            if (!_gameService.GameIsInCategory(result.GameId, result.CategoryId))
                return NotFound("Game is not part of Category");

            await _gameService.SubmitResult(result);

            return Ok();
        }

        // GET: api/Game/Get/Leaderboard/{gameId}/{limit}
        [HttpGet("Get/Leaderboard/{gameId}/{limit}")]
        public async Task<ActionResult<List<LeaderBoardDto>>> GetGameLeaderboard(int gameId, int limit)
        {
            if (!_gameService.GameExists(gameId))
                return NotFound();

            var leaderboard = await _leaderboardService.GetGameLeaderboard(gameId, limit);
            var leaderboardData = new List<LeaderBoardDto>();
            var i = 1;
            foreach (var entry in leaderboard)
            {
                leaderboardData.Add(new LeaderBoardDto
                {
                    Rank = i,
                    Name = entry.Key,
                    Score = entry.Value
                });
                i++;
            }

            return Ok(leaderboardData);
        }

        private GameDto ConvertToGameDto(Games game)
        {
            return new GameDto
            {
                GameId = game.GameId,
                Name = game.Name,
                ImgSrc = game.ImgSrc,
                Description = game.Description,
                CategoryId = game.CategoryId
            };
        }
    }
}
