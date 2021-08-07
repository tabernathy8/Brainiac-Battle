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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly LeaderBoardsService _leaderboardService;


        public CategoryController(CategoryService categoryService, LeaderBoardsService leaderBoardService)
        {
            _categoryService = categoryService;
            _leaderboardService = leaderBoardService;
        }

        // GET: Api/Category/Get/All
        [HttpGet("Get/All")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null)
                return NotFound();

            return categories.Select(c => ConvertToCategoryDto(c)).ToList();
        }

        // GET: Api/Category/Get/{categoryId}
        [HttpGet("Get/{categoryId}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);

            if (category == null)
                return NotFound();

            return ConvertToCategoryDto(category);
        }

        // GET: Api/Category/Get/Leaderboard/{categoryId}/{numEntries}
        [HttpGet("Get/Leaderboard/{categoryId}/{numEntries}")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategory(int categoryId, int numEntries)
        {
            if (!_categoryService.CategoryExists(categoryId))
                return NotFound();

            var leaderboard = await _leaderboardService.GetCategoryLeaderboard(categoryId, numEntries);

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

        private CategoryDto ConvertToCategoryDto(Categories Category)
        {
            return new CategoryDto
            {
                CategoryId = Category.CategoryId,
                Name = Category.Name,
                Description = Category.Description,
            };
        }
    }
}
