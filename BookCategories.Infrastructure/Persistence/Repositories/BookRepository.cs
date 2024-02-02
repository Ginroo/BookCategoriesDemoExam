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
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
           await _context.Books.AddAsync(book);
        }

        public IQueryable<Book> GetBooks()
        {
           return _context.Books.Include(b => b.Category);
        }
    }
}
