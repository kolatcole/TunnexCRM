using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class StaffSkillRepo:IRepo<StaffSkill>, IStaffSkillRepo
    {
        public readonly TContext _context;
        public StaffSkillRepo(TContext context)
        {
            _context = context;

        }

        public Task deleteAllAsync(List<StaffSkill> data)
        {
            throw new NotImplementedException();
        }

        public Task  deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StaffSkill>> getAllAsync()
        {
            try
            {
                var skill = await _context.StaffSkills.Include(x => x.Assessments).ToListAsync();
                return skill;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StaffSkill> getAsync(int ID)
        {
            try
            {
                var StaffSkill = await _context.StaffSkills.Include(x=>x.Assessments).Where(x => x.ID == ID).FirstOrDefaultAsync();
                return StaffSkill;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<StaffSkill>> getStaffSkillsByStaffID(int staffID)
        {
            try
            {
                var StaffSkills = await _context.StaffSkills.Include(x => x.Assessments).Where(x => x.StaffID == staffID).ToListAsync();
                return StaffSkills;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> insertAsync(StaffSkill data)
        {
            var StaffSkill = new StaffSkill();
            try
            {
                if (data != null)
                {
                    StaffSkill = new StaffSkill
                    {
                        SkillID=data.SkillID,
                        StaffID=data.StaffID,
                        SupervisorID=data.SupervisorID,
                        CompetencyValue=data.CompetencyValue
           

                    };
                    await _context.StaffSkills.AddAsync(StaffSkill);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StaffSkill.ID;
        }

        public Task<int> insertListAsync(List<StaffSkill> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> updateAsync(StaffSkill data)
        {
            var StaffSkill = await _context.StaffSkills.FindAsync(data.ID);
            try
            {
                if (StaffSkill != null)
                {

                    StaffSkill.SkillID = data.SkillID;
                    StaffSkill.StaffID = data.StaffID;
                    StaffSkill.SupervisorID = data.SupervisorID;
                    StaffSkill.CompetencyValue = data.CompetencyValue;


                    _context.StaffSkills.Update(StaffSkill);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StaffSkill.ID;
        }
    }
}
