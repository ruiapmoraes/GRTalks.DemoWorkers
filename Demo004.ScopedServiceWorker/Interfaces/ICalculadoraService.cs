using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo004.ScopedServiceWorker.Interfaces
{
    public interface ICalculadoraService
    {
        Task<int> Soma(int x, int y);
        Task<int> Multiplica(int x, int y);
    }
}
