namespace MedicineOrder.API.BackgroundServices
{
    public abstract class WorkerBase : BackgroundService
    {
        protected string WorkerName;
        public ILogger<WorkerBase> _logger { get; }

        protected WorkerBase(ILogger<WorkerBase> logger)
        {
            WorkerName = GetType().Name;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await RunWorkerAsync(stoppingToken);
            }catch (Exception ex) when (stoppingToken.IsCancellationRequested)
            {
                _logger.LogWarning(ex, $"{WorkerName} Execution Cancelled");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unhandled exception in {WorkerName}. Execution Stopping");
            }
        }

        public abstract Task RunWorkerAsync(CancellationToken stoppingToken);  
    }
}
