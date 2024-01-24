using Microsoft.EntityFrameworkCore;
using Pharmacy.Infrastructure.Contexts;

namespace Pharmacy.API.BackgroundServices
{
    public class SqlDbMigrationService : WorkerBase
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SqlDbMigrationService(IServiceScopeFactory scopeFactory, ILogger<WorkerBase> logger) : base(logger)
        {
            _scopeFactory = scopeFactory;
        }

        public override async Task RunWorkerAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var pharmacyContextOptions = scope.ServiceProvider.GetRequiredService<DbContextOptionsBuilder<PharmacyDbContext>>();
            pharmacyContextOptions.UseSqlServer(option => option.CommandTimeout(600));
            var pharmacyContext = new PharmacyDbContext(pharmacyContextOptions);
            await pharmacyContext.Database.MigrateAsync(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
