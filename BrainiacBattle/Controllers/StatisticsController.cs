using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrainiacBattle.Data;
using BrainiacBattle.Models;
using Microsoft.AspNetCore.Authorization;
using BrainiacBattle.Services;
using BrainiacBattle.DTOs;

namespace BrainiacBattle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsController(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        // GET: api/Statistics/Get/Account/{accountId}
        [HttpGet("Get/Account/{accountId}")]
        public async Task<ActionResult<AccountStatistics>> GetAccountStatistics(int accountId)
        {
            var statstics = await _statisticsService.GetAccountStatisticsAsync(accountId);

            if (statstics == null)
            {
                return NotFound();
            }

            return statstics;
        }

        // GET: api/Statistics/Get/Category/{accountId}/{categoryId}
        [HttpGet("Get/Category/{accountId}/{categoryId}")]
        public async Task<ActionResult<Statistics>> GetAccountStatistics(int accountId, int categoryId)
        {
            var statstics = await _statisticsService.GetCategoryStatisticsAsync(accountId, categoryId);

            if (statstics == null)
            {
                return NotFound();
            }

            return statstics;
        }

        // GET: api/Statistics/Get/Game/{accountId}/{gameId}
        [HttpGet("Get/Game/{accountId}/{gameId}")]
        public async Task<ActionResult<Statistics>> GetGameStatistics(int accountId, int gameId)
        {
            var statstics = await _statisticsService.GetGameStatisticsAsync(accountId, gameId);

            if (statstics == null)
            {
                return NotFound();
            }

            return statstics;
        }
    }
}
