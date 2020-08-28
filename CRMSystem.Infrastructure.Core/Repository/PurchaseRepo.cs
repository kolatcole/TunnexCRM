using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class PurchaseRepo:IPurchaseRepo
    {
        private readonly TContext _context;
        public PurchaseRepo(TContext context) => _context = context;

        

        public async Task<List<Purchase>> getAllAsync()
        {

            try
            {
                var Purchase = await _context.Purchases.Include(y => y.Cart).ThenInclude(a => a.Items).ToListAsync();

                return Purchase;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<List<Purchase>> getPurchaseHistoryByDate(DateTime startdate, DateTime enddate)
        {
            try
            {
                var Purchases = await _context.Purchases.Include(y => y.Cart).ThenInclude(a => a.Items).Where(x => x.DateCreated >= startdate && x.DateCreated < enddate).ToListAsync();
                return Purchases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Purchase> getAsync(string invNo)
        {
            try
            {
                var Purchase = await _context.Purchases.Include(y => y.Cart).ThenInclude(a => a.Items).Where(x => x.InvoiceNo == invNo).FirstOrDefaultAsync();

                return Purchase;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<List<Purchase>> getByCustomerIDAsync(int supplierID)
        {
            try
            {
                var Purchases = await _context.Purchases.Include(y => y.Cart).ThenInclude(a => a.Items).Where(x => x.SupplierID == supplierID).ToListAsync();
                return Purchases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<int> insertAsync(Purchase data)
        {

            //  Purchase obj = new Purchase();
            try
            {

                Purchase obj = new Purchase
                {
                    CartID = data.CartID,
                    InvoiceNo = data.InvoiceNo,
                    TotalAmountForeign=data.TotalAmountForeign,
                    ExchangeCurrency=data.ExchangeCurrency,
                    NairaEquivalent=data.NairaEquivalent,
                    TotalAmountNaira=data.TotalAmountNaira,
                    DateCreated = DateTime.Now,
                    UserCreated = data.UserModified,
                    SupplierID = data.SupplierID

                };
                await _context.Purchases.AddAsync(obj);
                await _context.SaveChangesAsync();
                return obj.ID;



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        public async Task<List<Purchase>> GetSingleDayPurchases(DateTime date)
        {
            try
            {
                var Purchases = await _context.Purchases.Include(y => y.Cart).ThenInclude(a => a.Items).Where(x => x.DateCreated.Date == date.Date).ToListAsync();
                return Purchases;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<List<Purchase>> GetPurchasesByDate(DateTime startdate, DateTime enddate)
        {
            try
            {
                var Purchases = await _context.Purchases.Include(y => y.Cart).ThenInclude(a => a.Items).
                            Where(x => x.DateCreated.Date >= startdate.Date && x.DateCreated <= enddate.Date).ToListAsync();
                return Purchases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Purchase>> getBySupplierIDandDateAsync(int supplierID, DateTime startdate, DateTime enddate)
        {
            try
            {
                var Purchases = await _context.Purchases.Include(y => y.Cart).
                                    ThenInclude(a => a.Items).Where(x => x.SupplierID == supplierID
                                    && x.DateCreated >= startdate && x.DateCreated <= enddate).ToListAsync();
                return Purchases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Purchase>> getAllPurchasesAsync()
        {
            try
            {
                var Purchases = await _context.Purchases.Include(y => y.Cart).
                                    ThenInclude(a => a.Items).ToListAsync();
                return Purchases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<Purchase>> getBySupplierIDAsync(int supplierID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Purchase>> getSaleHistoryByDate(DateTime startdate, DateTime enddate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Purchase>> GetSingleDaySales(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
