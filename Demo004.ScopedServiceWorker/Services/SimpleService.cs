using Demo004.ScopedServiceWorker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo004.ScopedServiceWorker.Services
{
    public class SimpleService : ISimpleService
    {
        private readonly ILogger<SimpleService> _logger;        

        public SimpleService(ILogger<SimpleService> logger)
        {
            _logger = logger;
        }

        public async Task Perform(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"SimpleService running at: {DateTimeOffset.Now}");
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
