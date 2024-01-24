using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Application.Medication.Handlers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Contexts;
using Pharmacy.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: FunctionsStartup(typeof(MedicationHandler.Startup))]
namespace MedicationHandler
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IMessageHandler, MessageHandler>();
            builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();

            var config = new ConfigurationBuilder()
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings") ?? config.GetConnectionString("DefaultConnection"); ;
            var medicineOrderContextOptions = new DbContextOptionsBuilder<PharmacyDbContext>().UseSqlServer(connectionString);
            builder.Services.AddScoped(context => medicineOrderContextOptions);
            builder.Services.AddDbContext<PharmacyDbContext>(ServiceLifetime.Scoped);
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
