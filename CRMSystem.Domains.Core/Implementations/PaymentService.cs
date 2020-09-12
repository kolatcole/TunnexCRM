using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepo<Payment> _pRepo;
        private readonly IPaymentRepo _repo;
        private readonly IInvoiceService _iService;
        public PaymentService(IRepo<Payment> pRepo, IInvoiceService iService, IPaymentRepo repo)
        {
            _pRepo = pRepo;
            _iService = iService;
            _repo = repo;
        }

        public async Task<List<Payment>> GetFreePayments(int customerID = 0, string startDate = "0", string endDate = "0")
        {

            List<Payment> payments;
            DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sDate);
            DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime eDate);

            if (sDate <= DateTime.MinValue)
                sDate = DateTime.Now.StartOfDay();

            if (eDate <= DateTime.MinValue)
                eDate = DateTime.Now.EndOfDay();
            else
                eDate = eDate.EndOfDay();

            // filter by customerID only, if that's what was given

            if ((startDate == "0" || endDate == "0") && customerID > 0)
            {
                return payments = await _repo.getFreePaymentsByCustomerAsync(customerID);
            }






            // filter by dates alone if customerID is not given

            else if (customerID < 1 && (startDate != "0" || endDate != "0"))
            {
                return payments = await _repo.getFreePaymentsByDatesAsync(sDate, eDate);
            }




            // filter by all given parameters

            else if (startDate != "0" && endDate != "0" && customerID > 0)
            {
                return payments = await _repo.getFreePaymentsByCustomerIDandDateAsync(customerID, sDate, eDate);
            }



            // get without any parameter
            else
                return payments = await _repo.getAllFreePaymentsAsync();

        }

        public async Task<int> PayAsync(Payment data)
        {
            // get invoice to make payment on
            var invoice = await _iService.GetInvoiceByNumber(data.InvoiceNo, data.CustomerID);

            var payment = new Payment
            {
                DatePaid = DateTime.Now,
                CustomerID=data.CustomerID,
                Amount=data.Amount,
                InvoiceNo=data.InvoiceNo,
                Method=data.Method,
                Reference=data.Reference,
                UserCreated=data.UserCreated
                
            };

            //save payment

            var PID = await _pRepo.insertAsync(payment);

            // update invoice with latest payment record

            invoice.AmountPaid += data.Amount;
            invoice.Balance = invoice.Amount - invoice.AmountPaid;
            if (invoice.Balance == 0)
                invoice.IsPaid = true;

            await _iService.updateAsync(invoice);

            return PID;


        }

        public async Task<bool> DeleteFOCPayment(string invNo)
        {
            var result = await _repo.DeleteFOCPaymentAsync(invNo);
            return result;
        
        }


    }
}
