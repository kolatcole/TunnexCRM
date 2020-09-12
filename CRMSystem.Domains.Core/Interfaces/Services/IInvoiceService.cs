using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IInvoiceService
    {
        Task<int> SaveInvoice(Invoice data);
        Task<Invoice> GetInvoiceByNumber(string InvNumber, int customerID);
        Task<Invoice> GetInvoiceByinvNo(string InvNumber);
        Task<List<Invoice>> GetInvoiceByCustomerID(int customerID);
        Task<int> getLastAsync();
        Task<List<Invoice>> getProformaByDate(int customerID, string startDate, string endDate);
        Task<int> updateAsync(Invoice data);
        Task<List<Invoice>> getDebtorInvoice(DateTime startdate, DateTime enddate);
        Task<int> SaveProformaInvoice(Invoice data);
    }
}
