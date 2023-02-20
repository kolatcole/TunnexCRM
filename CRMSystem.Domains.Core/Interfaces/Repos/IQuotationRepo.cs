using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IQuotationRepo
    {

         Task<List<Quotation>> getAllByCustomerandDateAsync(int customerID,DateTime startdate,DateTime endDate);


        Task<List<Quotation>> getAllByCustomerAsync(int customerID);

        Task<List<Quotation>> getAllQuotationsAsync();

       Task<int> insertAsync(Quotation data);

        Task<List<Quotation>> getAllByDatesAsync(DateTime startdate, DateTime endDate);



    }
}
