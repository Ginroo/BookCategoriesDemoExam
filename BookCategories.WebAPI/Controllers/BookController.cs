using BookCategories.ApplicationService;
using BookCategories.ApplicationService.DTOs;
using BookCategories.ApplicationService.Interface.Service;
using BookCategories.ApplicationService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookCategories.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks([FromQuery] BookQueryParametersDTO queryParameters)
        {
            var (books, totalRecords) = await _bookService.GetBooksAsync(queryParameters);
            
            _logger.LogInformation($"Books Count Result :'{books.Count()}' does not exist.");
            var pageMetadata = new
            {
                queryParameters.PageNumber,
                queryParameters.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / queryParameters.PageSize)
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pageMetadata));

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody]BooksAddQueryBodyDTO bookDto)
        {
            try
            {
                var bookId = await _bookService.AddOrUpdateBookAsync(bookDto);
                return CreatedAtAction("GetBook", new { id = bookId });
            }
            catch (ArgumentException ex)
            {
                // Log the error
                _logger.LogError(ex.Message);

                // Return a BadRequest response with the error message
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }
    }
}
