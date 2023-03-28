using Demo004.ScopedServiceWorker.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Demo004.ScopedServiceWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public IServiceProvider Services { get; }

        public Worker(ILogger<Worker> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker Service is Runnig");

            await ConsumeService(stoppingToken);

            #region Trecho desativado
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //} 
            #endregion
        }

        private async Task ConsumeService(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consumindo um Scoped Service Hosted Service is working");

            using (var scope = Services.CreateScope()) 
            {
                var myService = scope
                    .ServiceProvider
                        .GetRequiredService<ISimpleService>();
                await myService.Perform(stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stoping {cancellationToken}");
            return base.StopAsync(cancellationToken);
        }
    }
}