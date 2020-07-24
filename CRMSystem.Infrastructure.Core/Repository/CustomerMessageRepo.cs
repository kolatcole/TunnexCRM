using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class CustomerMessageRepo : IRepo<CustomerMessage>
    {
        private readonly TContext _context;
        public CustomerMessageRepo(TContext context)
        {
            _context = context;
        }

        public Task deleteAllAsync(List<CustomerMessage> data)
        {
            throw new NotImplementedException();
        }

        public async Task deleteAsync(int ID)
        {
            try
            {
                var CustomerMessages = await _context.CustomerMessages.Where(x => x.CustomerID == ID).ToListAsync();
                _context.CustomerMessages.RemoveRange(CustomerMessages);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CustomerMessage>> getAllAsync()
        {
            var CustomerMessages = await _context.CustomerMessages.ToListAsync();
            return CustomerMessages;
        }

        public Task<List<CustomerMessage>> getAllByIDAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerMessage> getAsync(int ID)
        {
            var CustomerMessage = await _context.CustomerMessages.Where(x => x.ID == ID).FirstOrDefaultAsync();
            return CustomerMessage;
        }

        public Task<List<CustomerMessage>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(CustomerMessage data)
        {
            var CustomerMessage = new CustomerMessage();
            try
            {
                if (data != null)
                {
                    CustomerMessage = new CustomerMessage
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserCreated,
                        Summary = data.Summary,
                        Type = data.Type,
                        CustomerID = data.CustomerID
                    };
                    await _context.CustomerMessages.AddAsync(CustomerMessage);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CustomerMessage.ID;
        }

        public async Task<int> insertListAsync(List<CustomerMessage> data)
        {
            int ID = 0;
            try
            {
                await _context.CustomerMessages.AddRangeAsync(data);
                ID = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ID;
        }

        public async Task<int> updateAsync(CustomerMessage data)
        {
            var CustomerMessage = await _context.CustomerMessages.Where(x => x.ID == data.ID).SingleOrDefaultAsync();
            try
            {
                if (CustomerMessage != null)
                {
                    CustomerMessage.Type = data.Type;
                    CustomerMessage.Summary = data.Summary;
                    CustomerMessage.UserModified = data.UserModified;
                    CustomerMessage.DateModified = data.DateModified;


                    _context.CustomerMessages.Update(CustomerMessage);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CustomerMessage.ID;
        }
    }
}
