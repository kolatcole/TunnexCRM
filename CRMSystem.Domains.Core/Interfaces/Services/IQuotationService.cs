using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IQuotationService
    {
        Task<int> SaveQuotationAndProducts(Quotation data);

        Task<List<Quotation>> GetAllByCustomerAndDates(int customerID, string startDate, string endDate);

        Task<List<Quotation>> GetAllQuotations();

        Task<int> ChangeQuoteToSale(Quotation data, string Lpo, bool IsDeliver, decimal DeliveryFee, decimal discount);

        //Task<List<Quotation>> GetAllByCustomer(int customerID);
    }
}
