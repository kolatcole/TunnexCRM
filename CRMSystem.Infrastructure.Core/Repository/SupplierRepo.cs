using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CRMSystem.Infrastructure
{
    public class SupplierRepo : IRepo<Supplier>
    {
        private readonly TContext _context;
        public SupplierRepo(TContext context)
        {
            _context = context;
        }
        public async Task deleteAsync(int ID)
        {
            try
            {
                var Supplier = await _context.Suppliers.FindAsync(ID);
                _context.Suppliers.Remove(Supplier);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task deleteAllAsync(List<Supplier> data)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Supplier>> getAllAsync()
        {
            try
            {
                return await _context.Suppliers.ToListAsync();
               
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        //public async Task<List<Supplier>> getAllByIDAsync(int ID)
        //{
        //    var Supplier = await _context.Suppliers.Where(x=>x.UserCreated==ID).ToListAsync();
        //    return Supplier;
        //}

        public async Task<Supplier> getAsync(int ID)
        {
            try
            {
                return await _context.Suppliers.Where(x => x.ID == ID).FirstOrDefaultAsync();
                
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }



        public Task<List<Supplier>> getBySupplierIDAsync(int SupplierID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(Supplier data)
        {
            var Supplier = new Supplier();
            try
            {
                if (data != null)
                {
                    Supplier = new Supplier
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserModified,
                        Name = data.Name,
                        Address = data.Address,
                        Phone = data.Phone,
                        Email=data.Email
                    };
                    await _context.Suppliers.AddAsync(Supplier);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Supplier.ID;
        }

        public async Task<int> insertListAsync(List<Supplier> data)
        {
            try
            {
                await _context.Suppliers.AddRangeAsync(data);
                await _context.SaveChangesAsync();
                return data.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public async Task<int> updateAsync(Supplier data)
        {
            var newSupplier = await _context.Suppliers.FindAsync(data.ID);
            try
            {


                if (data.Email != null) newSupplier.Email = data.Email;


                if (data.Name != null) newSupplier.Name = data.Name;

                if (data.Phone != null) newSupplier.Phone = data.Phone;

                data.DateModified = DateTime.Now;

                if (data.UserModified != 0) newSupplier.UserModified = data.UserModified;

                if (data.Address != null) newSupplier.Address = data.Address;


                _context.Suppliers.Update(newSupplier);
                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newSupplier.ID;
        }
    }
}
