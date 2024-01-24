using MedicineOrder.Application.Pharmacy.Handlers;
using MedicineOrder.Domain.Interfaces;
using MedicineOrder.Infrastructure.Contexts;
using MedicineOrder.Infrastructure.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: FunctionsStartup(typeof(PharmacyHandler.Startup))]
namespace PharmacyHandler
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IMessageHandler, MessageHandler>();
            builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();
            builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
            builder.Services.AddScoped<IMedicineOrderRepository, MedicineOrderRepository>();

            var config = new ConfigurationBuilder()
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings") ?? config.GetConnectionString("DefaultConnection"); ;
            var medicineOrderContextOptions = new DbContextOptionsBuilder<MedicineOrderDbContext>().UseSqlServer(connectionString);
            builder.Services.AddScoped(context => medicineOrderContextOptions);
            builder.Services.AddDbContext<MedicineOrderDbContext>(ServiceLifetime.Scoped);
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }
    }
}
