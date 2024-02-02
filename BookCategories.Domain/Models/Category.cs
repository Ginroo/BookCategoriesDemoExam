using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCategories.Domain.Models
{
    public class Category
    {
        [Key]
        [Required]
        public Guid CategoryId { get; set; }

        [Required()]
        [MaxLength(128)]
        public string CategoryName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
