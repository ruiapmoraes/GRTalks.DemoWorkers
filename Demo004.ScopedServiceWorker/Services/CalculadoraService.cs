using Demo004.ScopedServiceWorker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo004.ScopedServiceWorker.Services
{
    public class CalculadoraService : ICalculadoraService
    {
        public async Task<int> Multiplica(int x, int y)
        {
            return x * y;
        }

        public async Task<int> Soma(int x, int y)
        {
            return x + y;
        }
    }
}
