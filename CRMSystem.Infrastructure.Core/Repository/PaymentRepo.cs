using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class PaymentRepo : IRepo<Payment>, IPaymentRepo
    {
        private readonly TContext _context;
        public PaymentRepo(TContext context)
        {
            _context = context;
        }

        public Task deleteAllAsync(List<Payment> data)
        {
            throw new NotImplementedException();
        }

        public Task  deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Payment>> getAllAsync()
        {

            try
            {
                var payment = await _context.Payments.ToListAsync();
                return payment;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<Payment> getAsync(int ID)
        {
            try
            {
                var payment = await _context.Payments.Where(x => x.ID == ID).FirstOrDefaultAsync();
                return payment;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public Task<List<Payment>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Payment>> getPaymentByInvoiceNo(string invNo)
        {
            var payments = await _context.Payments.Where(x => x.InvoiceNo == invNo).ToListAsync();
            return payments;
        }

        public async Task<int> insertAsync(Payment data)
        {
            var payment = new Payment();
            try
            {
                if (data != null)
                {

                    payment = new Payment
                    {
                       
                        UserCreated = data.UserCreated,
                        Amount=data.Amount,
                        DatePaid= DateTime.Now,
                        InvoiceNo=data.InvoiceNo,
                        Method=data.Method,
                        Reference=data.Reference,
                        CustomerID=data.CustomerID
                    };
                    await _context.Payments.AddAsync(payment);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return payment.ID;
        }

        public Task<int> insertListAsync(List<Payment> data)
        {
            throw new NotImplementedException();
        }

        public Task<int> updateAsync(Payment data)
        {
            throw new NotImplementedException();
           
        }
       
    }
}
