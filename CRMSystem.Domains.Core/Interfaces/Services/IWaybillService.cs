using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IWaybillService
    {
        Task<int> SaveWaybillAndProducts(Waybill data);

        Task<List<Waybill>> GetAllWaybillByDates(string startDate, string endDate);

        
    }
}
