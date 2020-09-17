using CRMSystem.Domains;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class ReturnedStockRepo : IReturnedStockRepo
    {
        private readonly TContext _context;

        public ReturnedStockRepo(TContext context) => _context = context;


        
        public async Task<ReturnedStock> getAsync(string invNo)
        {
            try
            {
                var stock = await _context.ReturnedStocks.Where(x => x.InvoiceNo == invNo)
                    .Include(y=>y.Cart).ThenInclude(y=>y.Items).FirstOrDefaultAsync();
                return stock;
            }
            catch(Exception ex)
            {
                return null;
            }
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

      
    }
}
