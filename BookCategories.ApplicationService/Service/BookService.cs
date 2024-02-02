using BookCategories.ApplicationService.DTOs;
using BookCategories.ApplicationService.Interface;
using BookCategories.ApplicationService.Interface.Service;
using BookCategories.Domain.Inerface.Repository;
using BookCategories.Domain.Models;
using BookCategories.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookCategories.ApplicationService.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<BookService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<(List<BooksViewModel> books, double totalRecords)> GetBooksAsync(BookQueryParametersDTO queryParameters)
        {
            IQueryable<Book> booksQuery = _bookRepository.GetBooks();

            if (!string.IsNullOrEmpty(queryParameters.Category))
            {
                booksQuery = booksQuery.Where(b => b.Category.CategoryName == queryParameters.Category);
            }

            if (!string.IsNullOrEmpty(queryParameters.Title))
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(queryParameters.Title));
            }

            int totalRecords = await booksQuery.CountAsync();

            List<BooksViewModel>? books = await booksQuery
                .Select(b => new BooksViewModel
                {
                    BookId = b.BookId,
                    Category = b.Category.CategoryName,
                    Title = b.Title,
                    Description = b.Description,
                    PublishDateUtc = b.PublishDateUtc
                })
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return (books, totalRecords);
        }

        public async Task<Guid> AddOrUpdateBookAsync(BooksAddQueryBodyDTO bookDto)
        {
            var existingBook = await _bookRepository.GetBooks().FirstOrDefaultAsync(b => b.BookId == bookDto.BookId);

            if (existingBook == null)
            {
                // Book with specified Id does not exist, add a new book
                return await AddBookAsync(bookDto);
            }
            else
            {
                // Book with specified Id exists, update the existing book
                return await UpdateBookAsync(existingBook, bookDto);
            }
        }

        private async Task<Guid> AddBookAsync(BooksAddQueryBodyDTO bookDto)
        {
            // Check if the category exists
            var category = await _categoryRepository.GetByNameAsync(bookDto.Category);
            if (category == null)
            {
                // Log an error and throw an exception
                _logger.LogError($"Category '{bookDto.Category}' does not exist.");
                throw new ArgumentException($"Category '{bookDto.Category}' does not exist.");
            }

            var book = new Book
            {
                BookId = Guid.NewGuid(),
                Title = bookDto.Title,
                Description = bookDto.Description,
                CategoryId = category.CategoryId,
                PublishDateUtc = bookDto.PublishedDate ?? DateTime.UtcNow,
            };

            await _bookRepository.AddAsync(book);

            await _unitOfWork.SaveChangesAsync();

            return book.BookId;
        }

        private async Task<Guid> UpdateBookAsync(Book existingBook, BooksAddQueryBodyDTO bookDto)
        {
            // Check if the new category exists
            var newCategory = await _categoryRepository.GetByNameAsync(bookDto.Category);
            if (newCategory == null)
            {
                _logger.LogError($"Category '{bookDto.Category}' does not exist.");
                throw new ArgumentException($"Category '{bookDto.Category}' does not exist.");
            }

            existingBook.Title = bookDto.Title;
            existingBook.Description = bookDto.Description;
            existingBook.PublishDateUtc = bookDto.PublishedDate ?? DateTime.UtcNow;
            existingBook.CategoryId = newCategory.CategoryId;

            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return existingBook.BookId;
        }


    }
}
