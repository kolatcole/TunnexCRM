using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class QuotationRepo : IQuotationRepo
    {
        private readonly TContext _context;
        public QuotationRepo(TContext context)
        {

            _context = context;
        }

        public async Task<List<Quotation>> getAllByCustomerandDateAsync(int customerID, DateTime startdate, DateTime endDate)
        {
            try
            {
                try
                {
                    var quotations = await _context.Quotations.Include(y=>y.QuotProducts).Where(x => x.CustomerID == customerID && x.DateCreated >= startdate &&
                                            x.DateCreated <= endDate).OrderByDescending(x => x.DateCreated).ToListAsync();
                    return quotations;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Quotation>> getAllByDatesAsync( DateTime startdate, DateTime endDate)
        {
            try
            {
                try
                {
                    var quotations = await _context.Quotations.Include(y=>y.QuotProducts).Where(x => x.DateCreated >= startdate &&
                                            x.DateCreated <= endDate).OrderByDescending(x => x.DateCreated).ToListAsync();
                    return quotations;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Quotation>> getAllByCustomerAsync(int customerID)
        {
            try
            {
                var quotations = await _context.Quotations.Include(y=>y.QuotProducts).Where(x => x.CustomerID == customerID)
                                                            .OrderByDescending(x => x.DateCreated).ToListAsync();
                return quotations;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> insertAsync(Quotation data)
        {
            try
            {
                var quotation = new Quotation
                {
                    DateCreated = DateTime.Now,
                    CustomerID = data.CustomerID,
                    UserCreated = data.UserCreated
                };

                await _context.Quotations.AddAsync(quotation);
                await _context.SaveChangesAsync();

                return quotation.ID;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Quotation>> getAllQuotationsAsync()
        {
            try
            {
                var quotations = await _context.Quotations.Include(y => y.QuotProducts).OrderByDescending(x=>x.DateCreated).ToListAsync();
                return quotations;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
