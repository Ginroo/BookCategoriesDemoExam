using BookCategories.ApplicationService.Interface;
using BookCategories.ApplicationService.Interface.Service;
using BookCategories.ApplicationService.Service;
using BookCategories.Domain.Inerface.Repository;
using BookCategories.Infrastructure.Persistence;
using BookCategories.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCategories.ApplicationService.Configurations
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void Repository(IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
        }
    }
}
