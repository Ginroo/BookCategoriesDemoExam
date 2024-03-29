﻿using BookCategories.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCategories.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {

            }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
