using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo004.ScopedServiceWorker.Interfaces
{
    public interface ISimpleService
    {
        Task Perform(CancellationToken cancellationToken);
    }
}
