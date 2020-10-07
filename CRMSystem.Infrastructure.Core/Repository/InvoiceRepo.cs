using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class InvoiceRepo : IRepo<Invoice>, IInvoiceRepo
    {
        private readonly TContext _context;
        public InvoiceRepo(TContext context)
        {
            _context = context;
        }
        public async Task deleteAsync(int ID)
        {
            try
            {
                // get sale by invoiceID
                var invoice = await _context.Invoices.Where(x => x.IsDeleted != true && x.ID == ID).FirstOrDefaultAsync();

                // soft delete the invoice object and update the daatabase

                invoice.IsDeleted = true;
                invoice.DateModified = DateTime.Now;


                _context.Invoices.Update(invoice);
                ID = await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Task deleteAllAsync(List<Invoice> data)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Invoice>> getAllAsync()
        {

            try
            {
                var invoices = await _context.Invoices.Where(x => x.Type == "sales" && x.IsDeleted != true).
                    OrderByDescending(x => x.DateCreated).ToListAsync();
                return invoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<Invoice> getAsync(int ID)
        {
            try
            {
                var invoice = await _context.Invoices.Include(x => x.Cart).ThenInclude(x => x.Items).
                                        Where(x => x.ID == ID && x.Type == "sales" && x.IsDeleted != true).
                                        OrderByDescending(x => x.DateCreated).FirstOrDefaultAsync();
                return invoice;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<Invoice> getByNumberAsync(string invNumber, int customerID)
        {
            // PENDING
            try
            {
                var invoice = await _context.Invoices.Include(x => x.Cart).ThenInclude(x => x.Items).
                    Where(x => x.InvoiceNo == invNumber && x.CustomerID == customerID && x.IsDeleted != true).FirstOrDefaultAsync();
                return invoice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Invoice> getByinvNumberAsync(string invNumber)
        {
            
            try
            {
                var invoice = await _context.Invoices.Include(x => x.Cart).ThenInclude(x => x.Items).
                                                        Where(x => x.InvoiceNo == invNumber && x.Type == "sale" && x.IsDeleted!=true)
                                                        .OrderByDescending(x => x.DateCreated).FirstOrDefaultAsync();
                return invoice;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            throw new NotImplementedException();
        }



        public async Task<List<Invoice>> getByCustomerIDAsync(int customerID)
        {

            try
            {
                var invoices = await _context.Invoices.Include(x => x.Cart).ThenInclude(x => x.Items)
                                    .Where(x => x.CustomerID == customerID && x.Type == "sales" && x.IsDeleted != true)
                                    .OrderByDescending(x => x.DateCreated).ToListAsync();
                return invoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<List<Invoice>> getProformaByCustomerIDAsync(int customerID)
        {

            try
            {
                var invoices = await _context.Invoices.Include(x => x.Cart).ThenInclude(x => x.Items)
                                    .Where(x => x.CustomerID == customerID && x.Type == "proforma")
                                    .OrderByDescending(x => x.DateCreated).ToListAsync();
                return invoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<List<Invoice>> getAllProformaByDateAsync(DateTime startdate, DateTime enddate)
        {

            try
            {
                var invoices = await _context.Invoices.Include(x => x.Cart).
                                        ThenInclude(x => x.Items).
                                        Where(x => x.Type == "proforma" && x.DateCreated >= startdate && x.DateCreated <= enddate)
                                        .OrderByDescending(x => x.DateCreated).ToListAsync();
                return invoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Invoice>> getAllProformaByDateandCustomerAsync(int customerID, DateTime startdate, DateTime enddate)
        {

            try
            {
                var invoices = await _context.Invoices.Include(x => x.Cart).
                                        ThenInclude(x => x.Items).
                                        Where(x => x.Type == "proforma" && x.CustomerID == customerID &&
                                        x.DateCreated >= startdate && x.DateCreated <= enddate)
                                        .OrderByDescending(x => x.DateCreated).ToListAsync();
                return invoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Invoice>> getAllProformaAsync()
        {
            try
            {
                var invoices = await _context.Invoices.Include(x => x.Cart).
                                        ThenInclude(x => x.Items).
                                        Where(x => x.Type == "proforma")
                                        .ToListAsync();
                return invoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> getLastAsync()
        {
            try
            {

                var invoiceID = await _context.Invoices.OrderByDescending(x => x.ID).Select(y => y.ID).FirstAsync();
                return invoiceID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> insertAsync(Invoice data)
        {
            var invoice = new Invoice();
            try
            {
                if (data != null)
                {

                    invoice = new Invoice
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserCreated,
                        Amount = data.Amount,
                        DiscountPercent = data.DiscountPercent,
                        CustomerID = data.CustomerID,
                        ExtData = data.ExtData,
                        InvoiceDate = DateTime.Now,
                        InvoiceNo = data.InvoiceNo,
                        CartID = data.CartID,
                        AmountPaid = data.AmountPaid,
                        Balance = data.Balance,
                        IsPaid = data.IsPaid,
                        Type = data.Type,
                        DeliveryFee=data.DeliveryFee

                    };
                    await _context.Invoices.AddAsync(invoice);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return invoice.ID;
        }

        public Task<int> insertListAsync(List<Invoice> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> updateAsync(Invoice data)
        {

            var invoice = await _context.Invoices.FindAsync(data.ID);
            try
            {
                if (invoice != null)
                {
                    invoice.DateModified = DateTime.Now;
                    invoice.UserModified = data.UserModified;
                    invoice.AmountPaid = data.AmountPaid;
                    invoice.Balance = data.Balance;
                    invoice.IsDeleted = data.IsDeleted;


                    _context.Invoices.Update(invoice);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return invoice.ID;
        }
        public async Task<List<Invoice>> getAllDebtorsAsync(DateTime startdate, DateTime enddate)
        {

            try
            {
                var invoices = await _context.Invoices.Include(x => x.Cart).ThenInclude(x => x.Items).
                                        Where(x => x.Type == "sale" && x.Balance != 0 && 
                                        x.DateCreated >= startdate && x.DateCreated <= enddate && x.IsDeleted != true)
                                        .OrderByDescending(x => x.DateCreated).ToListAsync();
                return invoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
