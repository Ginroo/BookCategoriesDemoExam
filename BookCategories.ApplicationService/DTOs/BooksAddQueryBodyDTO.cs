using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCategories.ApplicationService.DTOs
{
    public class BooksAddQueryBodyDTO
    {
        public Guid? BookId { get; set; }
        public string Title { get; set; }
        public string Description  { get; set; }
        public string Category { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
