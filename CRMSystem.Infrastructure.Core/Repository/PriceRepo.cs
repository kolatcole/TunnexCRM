using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class PriceRepo:IRepo<Price>
    {
        private readonly TContext _context;
        public PriceRepo(TContext context)
        {
            _context = context;
        }

        public Task deleteAllAsync(List<Price> data)
        {
            throw new NotImplementedException();
        }

        public Task  deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Price>> getAllAsync()
        {
            var price = await _context.Prices.ToListAsync();
            return price;

        }

        public async Task<Price> getAsync(int ID)
        {
            var price = await _context.Prices.Where(x => x.ID == ID).FirstOrDefaultAsync();
            return price;

        }

        public Task<List<Price>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(Price data)
        {


            var price = new Price();
            try
            {
                    price = new Price
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserCreated,
                        SalePrice=data.SalePrice,
                        CostPrice=data.CostPrice
                    };
                    
                    await _context.Prices.AddAsync(price);
                    await _context.SaveChangesAsync();
                return price.ID;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public Task<int> insertListAsync(List<Price> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> updateAsync(Price data)
        {
            int ID = 0;
            var newPrice = await _context.Prices.FindAsync(data.ID);
            try
            {
                if (newPrice != null)
                {
                    
                    newPrice.SalePrice = data.SalePrice;
                    newPrice.CostPrice = data.CostPrice;
                    newPrice.DateModified = DateTime.Now;
                    newPrice.UserModified = data.UserModified;

                    _context.Prices.Update(newPrice);
                    ID=await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ID;
        }
    }
}
