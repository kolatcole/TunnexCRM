using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IWaybillRepo
    {
        Task<int> insertAsync(Waybill data);
        
        Task<List<Waybill>> getAllByDatesAsync(DateTime startdate, DateTime endDate);

        Task<List<Waybill>> getAllAsync();
    }
}
