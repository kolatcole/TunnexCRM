using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IPaymentService
    {
        Task<int> PayAsync(Payment data);

        Task<List<Payment>> GetFreePayments(int customer = 0, string startDate = "0", string endDate = "0");
    }
}
