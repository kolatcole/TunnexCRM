using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface ISaleRepo
    {
        Task<List<Sale>> getByCustomerIDAsync(int customerID);

        Task<Sale> getByInvIDAsync(int invID);


        Task<List<Sale>> getSaleHistoryByDate(DateTime startdate,DateTime enddate);
        Task<List<Sale>> GetSingleDaySales(DateTime date);
        Task<List<Sale>> getByCustomerIDandDateAsync(int customerID, DateTime startdate, DateTime enddate);
        Task<List<Sale>> getAllSalesAsync();
    }
}
