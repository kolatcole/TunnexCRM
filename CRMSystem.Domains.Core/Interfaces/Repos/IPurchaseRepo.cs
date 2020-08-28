using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IPurchaseRepo
    {

        Task<int> insertAsync(Purchase data);
        Task<Purchase> getAsync(string invoiceNo);
        Task<List<Purchase>> getAllAsync();
        Task<List<Purchase>> getBySupplierIDAsync(int supplierID);

        Task<List<Purchase>> getSaleHistoryByDate(DateTime startdate,DateTime enddate);
        Task<List<Purchase>> GetSingleDaySales(DateTime date);
        Task<List<Purchase>> getBySupplierIDandDateAsync(int supplierID, DateTime startdate, DateTime enddate);
        Task<List<Purchase>> getAllPurchasesAsync();
    }
}
