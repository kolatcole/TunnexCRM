using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface ISaleService
    {
        Task<int> Save(Sale data);
        Task<Sale> GetSaleByIDAsync(int ID);
        Task<List<Sale>> GetAllSalesAsync();
        Task<List<Sale>> GetSalesByCustomerIDAsync(int customerID);
        Task<List<Sale>> getSaleHistoryByDateAsync(DateTime startdate, DateTime enddate);
        Task<List<Sale>> GetSingleDaySalesAsync(DateTime date);
        Task<List<Sale>> GetSalesReportByDate(int customerID, string startDate, string endDate);
        Task<List<Waybill>> GetWaybillByDate(string startDate, string endDate);
    }
}
