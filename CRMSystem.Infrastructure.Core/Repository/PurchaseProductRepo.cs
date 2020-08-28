using CRMSystem.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class PurchaseProductRepo : IRepo<PurchaseProduct>
    {
        private readonly TContext _context;
        public PurchaseProductRepo(TContext context) => _context = context;


        public Task deleteAllAsync(List<PurchaseProduct> data)
        {
            throw new NotImplementedException();
        }

        public Task deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<List<PurchaseProduct>> getAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseProduct> getAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(PurchaseProduct data)
        {
            try
            {

                PurchaseProduct obj = new PurchaseProduct
                {
                    cartID = data.cartID,
                    productID = data.productID,
                    Amount=data.Amount

                };
                await _context.PurchaseProducts.AddAsync(obj);
                await _context.SaveChangesAsync();
                return obj.ID;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> insertListAsync(List<PurchaseProduct> data)
        {
            throw new NotImplementedException();
        }

        public Task<int> updateAsync(PurchaseProduct data)
        {
            throw new NotImplementedException();
        }
    }
}
