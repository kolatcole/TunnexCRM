using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IReturnedStockRepo
    {
        Task<ReturnedStock> getAsync(string invNo);
        
        Task<int> insertAsync(ReturnedStock data);
    }
}
