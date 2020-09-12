using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IPaymentRepo
    {
        Task<List<Payment>> getPaymentByInvoiceNo(string invNo);

        Task<List<Payment>> getFreePaymentsByCustomerAsync(int customerID);

        Task<List<Payment>> getFreePaymentsByDatesAsync(DateTime startDate,DateTime endDate);

        Task<List<Payment>> getAllFreePaymentsAsync();

        Task<List<Payment>> getFreePaymentsByCustomerIDandDateAsync(int customerID, DateTime startDate, DateTime endDate);

        Task<List<Payment>> getAllByInvAsync(string invNo);

        Task<bool> DeleteFOCPaymentAsync(string invNo);
    }
}
