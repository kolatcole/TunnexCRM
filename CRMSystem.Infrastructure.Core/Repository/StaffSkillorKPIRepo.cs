using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class StaffSkillorKPIRepo : IRepo<StaffSkillorKPI>, IStaffSkillorKPIRepo
    {
        public readonly TContext _context;
        public StaffSkillorKPIRepo(TContext context)
        {
            _context = context;

        }

        public Task deleteAllAsync(List<StaffSkillorKPI> data)
        {
            throw new NotImplementedException();
        }

        public Task deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StaffSkillorKPI>> getAllStaffSkillAsync()
        {
            try
            {
                var skill = await _context.StaffSkillorKPIs.Include(x => x.Assessments).ToListAsync();
                return skill;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<List<StaffSkillorKPI>> getAllStaffKPIAsync()
        //{
        //    try
        //    {
        //        var skill = await _context.StaffSkillorKPIs.Include(x => x.Assessments).ToListAsync();
        //        return skill;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<StaffSkillorKPI> getAsync(int ID)
        {
            try
            {
                var StaffSkill = await _context.StaffSkillorKPIs.Include(x => x.Assessments).Where(x => x.ID == ID).FirstOrDefaultAsync();
                return StaffSkill;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<StaffSkillorKPI>> getStaffSkillsByStaffID(int staffID)
        {
            try
            {
                var StaffSkills = await _context.StaffSkillorKPIs.Include(x => x.Assessments).Where(x => x.StaffID == staffID).ToListAsync();
                return StaffSkills;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<List<StaffSkillorKPI>> getStaffKPIByStaffID(int staffID)
        //{
        //    try
        //    {
        //        var StaffSkills = await _context.StaffSkillorKPIs.Include(x => x.Assessments).Where(x => x.StaffID == staffID && x.t).ToListAsync();
        //        return StaffSkills;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public async Task<int> insertAsync(StaffSkillorKPI data)
        {
            var StaffSkill = new StaffSkillorKPI();
            try
            {
                if (data != null)
                {
                    StaffSkill = new StaffSkillorKPI
                    {
                        SkillorKPIID = data.SkillorKPIID,
                        StaffID = data.StaffID,
                        SupervisorID = data.SupervisorID,
                        CompetencyValue = data.CompetencyValue


                    };
                    await _context.StaffSkillorKPIs.AddAsync(StaffSkill);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StaffSkill.ID;
        }

        public Task<int> insertListAsync(List<StaffSkillorKPI> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> updateAsync(StaffSkillorKPI data)
        {
            var StaffSkill = await _context.StaffSkillorKPIs.FindAsync(data.ID);
            try
            {
                if (StaffSkill != null)
                {

                    StaffSkill.SkillorKPIID = data.SkillorKPIID;
                    StaffSkill.StaffID = data.StaffID;
                    StaffSkill.SupervisorID = data.SupervisorID;
                    StaffSkill.CompetencyValue = data.CompetencyValue;


                    _context.StaffSkillorKPIs.Update(StaffSkill);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StaffSkill.ID;
        }

        public async Task<List<StaffSkillorKPI>> getAllAsync()
        {
            try
            {
                var skill = await _context.StaffSkillorKPIs.Include(x => x.Assessments).ToListAsync();
                return skill;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
