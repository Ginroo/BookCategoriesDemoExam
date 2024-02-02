using BookCategories.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCategories.Domain.Inerface.Repository
{
    public interface ICategoryRepository
    {
        Task<Category> GetByNameAsync(string categoryName);
    }
}
