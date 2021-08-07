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
    public class CategoryService
    {
        private readonly BrainiacBattleContext _context;
        
        public CategoryService(BrainiacBattleContext context)
        {
            _context = context;
        }

        public async Task<List<Categories>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Categories> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(g => g.CategoryId == categoryId);
        }

        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.CategoryId == categoryId);
        }
    }
}
