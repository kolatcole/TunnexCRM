using CRMSystem.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Infrastructure
{
    public class ItemRepo : IRepo<Item>
    {
        TContext _context;
        public ItemRepo(TContext context)
        {
            _context = context;
        }

        public async Task deleteAsync(int ID)
        {
            try
            {
                var item = await _context.Items.FindAsync(ID);
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task deleteAllAsync(List<Item> data)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Item>> getAllAsync()
        {
            try
            {
                var item = await _context.Items.ToListAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Item> getAsync(int ID)
        {
            try
            {
                var item = await _context.Items.Where(x => x.ID == ID).FirstOrDefaultAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public Task<List<Item>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(Item data)
        {
            var item = new Item();
            try
            {
                if (data != null)
                {
                    item = new Item
                    {
                        Amount = data.Amount,
                        CartID = data.CartID,
                        Code = data.Code,
                        Name = data.Name,
                        //Price = data.Price,
                        ProductID = data.ProductID,
                        Quantity = data.Quantity
                    };
                    await _context.Items.AddAsync(item);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item.ID;
        }

        public async Task<int> insertListAsync(List<Item> data)
        {
            int ID = 0;
            try
            {
                await _context.Items.AddRangeAsync(data);
                ID = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ID;
        }

        public async Task<int> updateAsync(Item data)
        {
            int ID = 0;
            var item = await _context.Items.FindAsync(data.ID);
            try
            {
                if (item != null)
                {
                    item.Amount = data.Amount;
                    item.CartID = data.CartID;
                    item.Code = data.Code;
                    item.Name = data.Name;
                    item.Quantity = data.Quantity;

                    _context.Update(item);
                    ID = await _context.SaveChangesAsync();
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
