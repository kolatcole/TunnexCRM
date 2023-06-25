using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IInvoiceRepo
    {

        Task<int> getLastAsync();

        Task<Invoice> getByNumberAsync(string invNumber,int customerID);

        Task<Invoice> getByinvNumberAsync(string invNumber);

        Task<List<Invoice>> getByCustomerIDAsync(int customerID);

        Task<List<Invoice>> getProformaByCustomerIDAsync(int customerID);

        Task<List<Invoice>> getAllDebtorsAsync(DateTime startdate, DateTime enddate);

        Task<List<Invoice>> getAllProformaByDateAsync(DateTime startdate, DateTime enddate);

        Task<List<Invoice>> getAllProformaByDateandCustomerAsync(int customerID, DateTime startdate, DateTime enddate);

        Task<List<Invoice>> getAllProformaAsync();
    }
}
