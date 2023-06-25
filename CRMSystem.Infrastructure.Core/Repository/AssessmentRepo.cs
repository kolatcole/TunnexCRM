using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class AssessmentRepo : IRepo<Assessment>
    {
        private readonly TContext _context;
        public AssessmentRepo(TContext context)
        {

            _context = context;
        }
        public async Task deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }
        public Task deleteAllAsync(List<Assessment> data)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Assessment>> getAllAsync()
        {
            try
            {
                var assessment = await _context.Assessments.ToListAsync();
                return assessment;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }


        public async Task<Assessment> getAsync(int ID)
        {
            try
            {
                var Assessment = await _context.Assessments.Where(x => x.ID == ID).FirstOrDefaultAsync();
                return Assessment;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }



        public async Task<int> insertAsync(Assessment data)
        {
            var Assessment = new Assessment();
            try
            {
                if (data != null)
                {
                    Assessment = new Assessment
                    {
                        DateCreated = DateTime.Now,
                        SAS = data.SAS,
                        StaffSkillorKPIID = data.StaffSkillorKPIID
                    };
                    await _context.Assessments.AddAsync(Assessment);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Assessment.ID;
        }

        public Task<int> insertListAsync(List<Assessment> data)
        {
            throw new NotImplementedException();
        }



        public Task<int> updateAsync(Assessment data)
        {
            throw new NotImplementedException();
        }

    }
}
