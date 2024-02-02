using BookCategories.Domain.Inerface.Repository;
using BookCategories.Domain.Models;
using BookCategories.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCategories.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> GetByNameAsync(string categoryName)
        {
            return await _context.Categories.Where(c => c.CategoryName.ToLower() == categoryName.ToLower()).FirstOrDefaultAsync();
        }
    }
}
