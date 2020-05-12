﻿using CRMSystem.Domains;
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
        public Task<int> deleteAsync(Invoice data)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Invoice>> getAllAsync()
        {

            try
            {
                var invoices = await _context.Invoices.ToListAsync();
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
                var invoice = await _context.Invoices.Where(x => x.ID == ID).FirstOrDefaultAsync();
                return invoice;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<Invoice> getByNumberAsync(string invNumber)
        {
            // PENDING
            //try
            //{
            //    var invoice = await _context.Invoices.Include(y=>y.Cart).Where(x => x.InvoiceNo == invNumber).FirstOrDefaultAsync();
            //    return invoice;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            throw new NotImplementedException();
        }

        public async Task<List<Invoice>> getByCustomerIDAsync(int customerID)
        {
            // PENDING
            //try
            //{
            //    var invoices = await _context.Invoices.Include(y => y.Cart).Where(x => x.CustomerID == customerID).ToListAsync();
            //    return invoices;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            throw new NotImplementedException();

        }

        public async Task<int> getLastAsync()
        {
            try
            {
                var invoice = await _context.Invoices.LastAsync();
                return invoice.ID;
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
                        Amount=data.Amount,
                        Discount=data.Discount,
                        CustomerID=data.CustomerID,
                        ExtData=data.ExtData,
                        InvoiceDate=DateTime.Now,
                        InvoiceNo=data.InvoiceNo,
                        CartID=data.CartID,
                        AmountPaid=data.AmountPaid,
                        Balance=data.Balance,
                        IsPaid=data.IsPaid

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

        public Task<int> updateAsync(Invoice data)
        {
            throw new NotImplementedException();
            //int ID = 0;
            //var invoice = await _context.Invoices.FindAsync(data.ID);
            //try
            //{
            //    if (invoice != null)
            //    {
            //        invoice.UserModified = data.UserModified;
            //        invoice. = data.UserCreated;
            //            Amount = data.Amount;
            //            Discount = data.Discount;
            //            CustomerID = data.CustomerID;
            //            ExtData = data.ExtData;
            //            InvoiceDate = DateTime.Now;

            //        _context.Invoices.Update(newInvoice);
            //        ID = await _context.SaveChangesAsync();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //return ID;
        }
    }
}
