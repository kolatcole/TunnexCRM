using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class QuotProductRepo : IRepo<QuotProduct>
    {
        private readonly TContext _context;
        public QuotProductRepo(TContext context)
        {

            _context = context;
        }

        public Task deleteAllAsync(List<QuotProduct> data)
        {
            throw new NotImplementedException();
        }

        public Task deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuotProduct>> getAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<QuotProduct> getAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(QuotProduct data)
        {
            try
            {
                var product = new QuotProduct
                {
                   ProductID=data.ProductID,
                   QuotationID=data.QuotationID,
                    UnitPrice=data.UnitPrice
                };

                await _context.QuotProducts.AddAsync(product);
                await _context.SaveChangesAsync();

                return product.ID;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> insertListAsync(List<QuotProduct> data)
        {
            try
            {
                

                await _context.QuotProducts.AddRangeAsync(data);
                await _context.SaveChangesAsync();

                return data.Count;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> updateAsync(QuotProduct data)
        {
            throw new NotImplementedException();
        }
    }
}