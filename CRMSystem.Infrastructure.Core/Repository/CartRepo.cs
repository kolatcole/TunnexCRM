using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class CartRepo : IRepo<Cart>
    {
        private readonly TContext _context;
        public CartRepo(TContext context)
        {
            _context = context;
        }

        public async Task deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }
        public Task deleteAllAsync(List<Cart> data)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Cart>> getAllAsync()
        {
            var carts = await _context.Carts.ToListAsync();
            return carts;
        }

        public Task<List<Cart>> getAllByIDAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> getAsync(int ID)
        {
            var cart = await _context.Carts.Where(x => x.ID == ID).FirstOrDefaultAsync();
            return cart;
        }

        public Task<List<Cart>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(Cart data)
        {
            var cart = new Cart();
            try
            {
                if (data != null)
                {
                    cart = new Cart
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserModified,
                        Code = data.Code
                    };
                    await _context.Carts.AddAsync(cart);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cart.ID;
        }

        public Task<int> insertListAsync(List<Cart> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> updateAsync(Cart data)
        {
            var cart = await _context.Carts.Where(x => x.ID == data.ID).SingleOrDefaultAsync();
            try
            {
                if (cart != null)
                {
                    cart.Code = data.Code;
                    cart.DateModified = data.DateModified;
                    cart.UserModified = data.UserModified;
                    cart.Amount = data.Amount;



                    _context.Carts.Update(cart);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cart.ID;
        }
    }

}
