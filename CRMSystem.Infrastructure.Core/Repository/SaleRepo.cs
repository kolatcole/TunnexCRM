﻿using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class SaleRepo : IRepo<Sale>,ISaleRepo
    {
        private readonly TContext _context;
        public SaleRepo(TContext context)
        {
            _context = context;
        }
        public async Task  deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sale>> getAllAsync()
        {

            try
            {
                var sale = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items).ToListAsync();

                return sale;
            }
            catch (Exception ex)
            {
                throw ex;
            } 


        }

        public async Task<List<Sale>> getSaleHistoryByDate(DateTime startdate, DateTime enddate) 
        {
            try
            {
                var sales = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items).Where(x => x.DateCreated >= startdate && x.DateCreated < enddate).ToListAsync();
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sale> getAsync(int ID)
        {
            try
            {
                var sale = await _context.Sales.Include(y=>y.Invoice).Include(y=>y.Cart).ThenInclude(a=>a.Items).Where(x => x.ID == ID).FirstOrDefaultAsync();
                return sale;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<List<Sale>> getByCustomerIDAsync(int customerID)
        {
            try
            {
                var sales = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items).Where(x => x.CustomerID == customerID).ToListAsync();
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<int> insertAsync(Sale data)
        {

          //  Sale obj = new Sale();
            try
            {
                
                   Sale obj = new Sale
                    {
                       CartID=data.CartID,
                       InvoiceID=data.InvoiceID,
                      
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserModified,
                        CustomerID = data.CustomerID
                        
                    };
                    await _context.Sales.AddAsync(obj);
                    await _context.SaveChangesAsync();
                    return obj.ID;
                
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public Task<int> insertListAsync(List<Sale> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> updateAsync(Sale data)
        {
            int ID = 0;
            var sale = await _context.Sales.FindAsync(data.ID);
            try
            {
                if (sale != null)
                {
                    sale.InvoiceID = data.InvoiceID;
                    sale.DateModified = data.DateModified;
                    sale.UserModified = data.UserModified;


                    _context.Sales.Update(sale);
                    ID = await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sale.ID;
        }


        public async Task<List<Sale>> GetSingleDaySales(DateTime date)
        {
            try
            {
                var sales = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items).Where(x => x.DateCreated.Date ==date.Date).ToListAsync();
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task deleteAllAsync(List<Sale> data)
        {
            throw new NotImplementedException();
        }
        //public async Task<List<Sale>> getByCustomerIDAsync(int customerID)
        //{
        //    try
        //    {
        //        var sales = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items).Where(x => x.CustomerID == customerID).ToListAsync();
        //        return sales;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //} b4 deployemnt

    }
}
