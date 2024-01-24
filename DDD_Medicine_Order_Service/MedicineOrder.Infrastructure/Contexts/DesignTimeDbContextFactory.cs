using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Infrastructure.Contexts
{
    public class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        public TContext CreateDbContext(string[] arg)
        {

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{env}.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TContext>();
            var connectionstring = _configurationRoot.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionstring);
            builder.EnableSensitiveDataLogging();
            var dbContext = (TContext)Activator.CreateInstance(typeof(TContext), builder);
            return dbContext;
        }
    }
}
