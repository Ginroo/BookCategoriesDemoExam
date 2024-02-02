using BookCategories.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCategories.ApplicationService.Interface.Service
{
    public interface IBookService
    {
        Task<(List<BooksViewModel> books, double totalRecords)> GetBooksAsync(BookQueryParametersDTO queryParameters);
        Task<Guid> AddOrUpdateBookAsync(BooksAddQueryBodyDTO bookDto);
    }
}
