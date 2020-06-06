using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRMSystem.Infrastructure
{
    public class QualificationRepo : IRepo<Qualification>
    {
        private readonly TContext _context;
        public QualificationRepo(TContext context)
        {

            _context = context;
        }

        public Task deleteAllAsync(List<Qualification> data)
        {
            throw new NotImplementedException();
        }

        public async Task  deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Qualification>> getAllAsync()
        {
            try
            {
                var Qualification = await _context.Qualifications.ToListAsync();
                return Qualification;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        
        public async Task<Qualification> getAsync(int ID)
        {
            try
            {
                var Qualification = await _context.Qualifications.Where(x => x.ID == ID).FirstOrDefaultAsync();
                return Qualification;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }



        public async Task<int> insertAsync(Qualification data)
        {
            var Qualification = new Qualification();
            try
            {
                if (data != null)
                {
                    Qualification = new Qualification
                    {
                        EndDate=data.EndDate,
                        StaffID=data.StaffID,
                        StartDate=data.StartDate,
                        Name=data.Name,
                        Status=data.Status
                    };
                    await _context.Qualifications.AddAsync(Qualification);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Qualification.ID;
        }

        public Task<int> insertListAsync(List<Qualification> data)
        {
            throw new NotImplementedException();
        }



        public async Task<int> updateAsync(Qualification data)
        {
            var qual = await _context.Qualifications.FindAsync(data.ID);
            try
            {
                if (qual != null)
                {


                    qual.EndDate = data.EndDate;
                    qual.StaffID = data.StaffID;
                    qual.StartDate = data.StartDate;
                    qual.Name = data.Name;
                    qual.Status = data.Status;

                    _context.Qualifications.Update(qual);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return qual.ID;
        }
    }
}
