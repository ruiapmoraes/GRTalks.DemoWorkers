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
                _logger.LogInformation("Worker Calculadora rodando às: {time}", DateTimeOffset.Now);
                await ConsomeServico(stoppingToken);               
                await Task.Delay(3000, stoppingToken);
            }
        }

        private Task ConsomeServico(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var meuService = scope
                    .ServiceProvider
                        .GetRequiredService<ICalculadoraService>();

                Random xnd = new Random();
                Random ynd = new Random();

                var x = xnd.Next(10);
                var y = xnd.Next(10);
                
                _logger.LogInformation($"O resultado da soma de {x} + {y} = {meuService.Soma(x, y).Result}");
                _logger.LogInformation($"O resultado da multiplicação de {x} x {y} = {meuService.Multiplica(x, y).Result}");
             
            }

            return Task.CompletedTask;
        }
    }
}
