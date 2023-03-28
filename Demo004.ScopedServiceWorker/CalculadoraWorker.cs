using Demo004.ScopedServiceWorker.Interfaces;
using Demo004.ScopedServiceWorker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo004.ScopedServiceWorker
{
    public class CalculadoraWorker : BackgroundService
    {
        private readonly ILogger<CalculadoraWorker> _logger;
        
        public IServiceProvider Services { get; }

        public CalculadoraWorker(ILogger<CalculadoraWorker> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ConsumeService(stoppingToken);               
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task ConsumeService(CancellationToken stoppingToken)
        {      
            using (var scope = Services.CreateScope())
            {
                var myService = scope
                    .ServiceProvider
                        .GetRequiredService<ICalculadoraService>();

                Random xnd = new Random();
                Random ynd = new Random();

                var x = xnd.Next(10);
                var y = xnd.Next(10);

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation($"A soma de {x} e {y} é {myService.Soma(x, y).Result}");
                _logger.LogInformation($"A multiplicação de {x} e {y} é {myService.Multiplica(x, y).Result}");

                //Task.CompletedTask.Wait();
            }
        }
    }
}
