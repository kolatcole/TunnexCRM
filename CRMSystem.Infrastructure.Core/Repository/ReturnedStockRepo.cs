using CRMSystem.Domains;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class ReturnedStockRepo : IRepo<ReturnedStock>
    {
        private readonly TContext _context;

        public ReturnedStockRepo(TContext context) => _context = context;


        public Task deleteAllAsync(List<ReturnedStock> data)
        {
            throw new NotImplementedException();
        }

        public Task deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReturnedStock>> getAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReturnedStock> getAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(ReturnedStock data)
        {
            try
            {
                var stock = new ReturnedStock
                {
                    DateCreated = DateTime.Now,
                    RefundAmount = data.RefundAmount,
                    InvoiceNo = data.InvoiceNo,
                    CartID=data.CartID
                };

                await _context.ReturnedStocks.AddAsync(stock);
                await _context.SaveChangesAsync();
                return stock.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task<int> insertListAsync(List<ReturnedStock> data)
        {
            throw new NotImplementedException();
        }

        public Task<int> updateAsync(ReturnedStock data)
        {
            throw new NotImplementedException();
        }
    }
}
