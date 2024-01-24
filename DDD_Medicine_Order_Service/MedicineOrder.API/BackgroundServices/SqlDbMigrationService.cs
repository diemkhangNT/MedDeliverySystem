using MedicineOrder.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MedicineOrder.API.BackgroundServices
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
            var medicineOrderContextOptions = scope.ServiceProvider.GetRequiredService<DbContextOptionsBuilder<MedicineOrderDbContext>>();
            medicineOrderContextOptions.UseSqlServer(option => option.CommandTimeout(600));
            var medicineOrderContext = new MedicineOrderDbContext(medicineOrderContextOptions);
            await medicineOrderContext.Database.MigrateAsync(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
