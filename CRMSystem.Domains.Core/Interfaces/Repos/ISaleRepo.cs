using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface ISaleRepo
    {
        Task<List<Sale>> getByCustomerIDAsync(int customerID);

        Task<List<Sale>> getSaleHistoryByDate(DateTime startdate,DateTime enddate);
        Task<List<Sale>> GetSingleDaySales(DateTime date);
    }
}
