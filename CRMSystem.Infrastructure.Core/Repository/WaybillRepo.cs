using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class WaybillRepo : IWaybillRepo
    {
        private readonly TContext _context;
        public WaybillRepo(TContext context)
        {

            _context = context;
        }



        public async Task<int> insertAsync(Waybill data)
        {
            try
            {
                var waybill = new Waybill
                {
                    DateCreated = DateTime.Now,
                    UserCreated = data.UserCreated,
                    InvoiceNo = data.InvoiceNo,
                    customerID=data.customerID
                };

                await _context.Waybills.AddAsync(waybill);
                await _context.SaveChangesAsync();

                return waybill.ID;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Waybill>> getAllByDatesAsync(DateTime startdate, DateTime endDate)
        {
            try
            {
                try
                {
                    var waybills = await _context.Waybills.Include(y => y.WaybillProducts).Where(x => x.DateCreated >= startdate &&
                                              x.DateCreated <= endDate).OrderByDescending(x=>x.DateCreated).ToListAsync();
                    return waybills;
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

        public async Task<List<Waybill>> getAllAsync()
        {
            try
            {
                try
                {
                    var waybills = await _context.Waybills.Include(y => y.WaybillProducts).OrderByDescending(x => x.DateCreated).ToListAsync();
                    return waybills;
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
    }


}
