using BookCategories.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCategories.ApplicationService.Configurations
{
    public class ConnectionConfiguration
    {
       // public static void ConfigureConnection(IServiceCollection services)
       // {
       //     services.AddDbContext<ApplicationDbContext>(options =>
       //options.UseSqlite(Configuration.GetConnectionString("YourConnectionName")));
       //     // Add services to the container.
          
       // }
    }
}
