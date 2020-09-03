using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IPurchaseService
    {
        Task<int> MakePurchase(Purchase data);

        Task<Purchase> GetPurchaseByInvoiceNo(string invoiceNo);

        Task<List<Purchase>> GetPurchasesReportByDate(int customerID, string startDate, string endDate);



    }
}
