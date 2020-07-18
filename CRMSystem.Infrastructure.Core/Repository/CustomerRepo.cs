using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CRMSystem.Infrastructure
{
    public class CustomerRepo : IRepo<Customer>, ICustomerRepo
    {
        private readonly TContext _context;
        public CustomerRepo(TContext context)
        {
            _context = context;
        }
        public async Task  deleteAsync(int ID)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(ID);
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task deleteAllAsync(List<Customer> data)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Customer>> getAllAsync()
        {
            try
            {
                var customer = await _context.Customers.Include(x=>x.CustomerMessages).ToListAsync();
                return customer;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        //public async Task<List<Customer>> getAllByIDAsync(int ID)
        //{
        //    var customer = await _context.Customers.Where(x=>x.UserCreated==ID).ToListAsync();
        //    return customer;
        //}

        public async Task<Customer> getAsync(int ID)
        {
            try
            {
                var customer = await _context.Customers.Include(x => x.CustomerMessages).Where(x => x.ID == ID).FirstOrDefaultAsync();
                return customer;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        

        public Task<List<Customer>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(Customer data)
        {
            var customer = new Customer();
            try
            {
                if (data != null)
                {
                    customer = new Customer
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserModified,
                        FirstName=data.FirstName,
                        Address=data.Address,
                        Email=data.Email,
                        Gender=data.Gender,
                        Image=data.Image,
                        LastName=data.LastName,
                        Phone=data.Phone,
                        TotalSales=data.TotalSales
                    };
                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customer.ID;
        }

        public Task<int> insertListAsync(List<Customer> data)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> MostFrequentCustomer()
        {
            try
            {
                var customers = await _context.Customers.OrderByDescending(x => x.TotalSales).Take(10).ToListAsync();
                return customers;
            }
            catch (Exception ex)
            {
                throw ex;

            }
           // throw new NotImplementedException();
        }

        public async Task<int> updateAsync(Customer data)
        {
            var newCustomer = await _context.Customers.FindAsync(data.ID);
            try
            {


                if (data.Image != null ) newCustomer.Image = data.Image;


                if (data.FirstName != null ) newCustomer.FirstName = data.FirstName;

                if (data.Phone != null) newCustomer.Phone = data.Phone;

                if (data.LastName != null ) newCustomer.LastName = data.LastName;
                data.DateModified = DateTime.Now;

                if (data.UserModified != 0 ) newCustomer.UserModified = data.UserModified;

                if (data.Gender != null ) newCustomer.Gender = data.Gender;

                if (data.Email != null) newCustomer.Email = data.Email;

                if (data.Address != null) newCustomer.Address = data.Address;

                if (data.TotalSales != 0) newCustomer.TotalSales += data.TotalSales;


                    _context.Customers.Update(newCustomer);
                     await _context.SaveChangesAsync();
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newCustomer.ID;
        }
    }
}
