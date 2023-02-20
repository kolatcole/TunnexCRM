using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class SaleRepo : IRepo<Sale>, ISaleRepo
    {
        private readonly TContext _context;
        public SaleRepo(TContext context)
        {
            _context = context;
        }
        public async Task deleteAsync(int ID)
        {
            try
            {
                // get sale by invoiceID
                var sale = await _context.Sales.Where(x => x.IsDeleted != true && x.InvoiceID == ID).FirstOrDefaultAsync();

                // soft delete the sale object and update the daatabase

                sale.IsDeleted = true;
                sale.DateModified = DateTime.Now;


                _context.Sales.Update(sale);
                ID = await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<Sale>> getAllAsync()
        {

            try
            {
                var sale = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items)
                                                .OrderByDescending(x => x.DateCreated ).Where(x=>x.IsDeleted==false).ToListAsync();

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
                var sales = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items)
                                                .Where(x => x.DateCreated >= startdate && x.DateCreated <= enddate && x.IsDeleted == false)
                                                .OrderByDescending(x => x.DateCreated).ToListAsync();
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // this ID is invoiceID used like this becuase of generic interface
        public async Task<Sale> getAsync(int ID)
        {
            try
            {
                var sale = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items)
                                                .Where(x => x.InvoiceID == ID && x.IsDeleted==false).FirstOrDefaultAsync();
                return sale;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<Sale> getByInvIDAsync(int invID)
        {
            try
            {
                var sale = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items).
                    Where(x => x.InvoiceID == invID && x.IsDeleted == false).FirstOrDefaultAsync();
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
                var sales = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items)
                                                .Where(x => x.CustomerID == customerID && x.IsDeleted==false)
                                                .OrderByDescending(x => x.DateCreated).ToListAsync();
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
                    CartID = data.CartID,
                    InvoiceID = data.InvoiceID,
                    LPO = data.LPO,
                    DateCreated = DateTime.Now,
                    UserCreated = data.UserCreated,
                    CustomerID = data.CustomerID,
                    ToDeliver=data.ToDeliver,
                    DeliveryFee=data.DeliveryFee

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
                var sales = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items)
                                                .Where(x => x.DateCreated.Date == date.Date && x.IsDeleted==false)
                                                .OrderByDescending(x => x.DateCreated).ToListAsync();
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

        public async Task<List<Sale>> GetSalesByDate(DateTime startdate, DateTime enddate)
        {
            try
            {
                var sales = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).ThenInclude(a => a.Items).
                            Where(x => x.DateCreated.Date >= startdate.Date && x.DateCreated <= enddate.Date && x.IsDeleted == false)
                            .OrderByDescending(x => x.DateCreated).ToListAsync();
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Sale>> getByCustomerIDandDateAsync(int customerID, DateTime startdate, DateTime enddate)
        {
            try
            {
                var sales = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).
                                    ThenInclude(a => a.Items).Where(x => x.CustomerID == customerID
                                    && x.DateCreated >= startdate && x.DateCreated <= enddate && x.IsDeleted == false)
                                    .OrderByDescending(x => x.DateCreated).ToListAsync();
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Sale>> getAllSalesAsync()
        {
            try
            {
                var sales = await _context.Sales.Include(y => y.Invoice).Include(y => y.Cart).
                                    ThenInclude(a => a.Items).Where(x =>x.IsDeleted == false)
                                    .OrderByDescending(x => x.DateCreated).ToListAsync();
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
