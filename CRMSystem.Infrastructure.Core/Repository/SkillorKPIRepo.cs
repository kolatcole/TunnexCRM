using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{  // create ISkillRepo to form getKpiByType
    public class SkillorKPIRepo : IRepo<SkillorKPI>,ISkillorKPIRepo
    {
        private readonly TContext _context;
        public SkillorKPIRepo(TContext context)
        {

            _context = context;
        }

        public Task deleteAllAsync(List<SkillorKPI> data)
        {
            throw new NotImplementedException();
        }

        public async Task deleteAsync(int ID)
        {
            try
            {
                var skill = await _context.SkillorKPIs.FindAsync(ID);
                _context.SkillorKPIs.Remove(skill);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SkillorKPI>> getAllAsync()
        {
            try
            {
                var skill = await _context.SkillorKPIs.Where(x=>x.Type=="skill").ToListAsync();
                return skill;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }
        public async Task<List<SkillorKPI>> getAllKPIs()
        {
            try
            {
                var skill = await _context.SkillorKPIs.Where(x => x.Type == "kpi").ToListAsync();
                return skill;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<SkillorKPI> getAsync(int ID)
        {
            try
            {
                var skill = await _context.SkillorKPIs.Where(x => x.ID == ID).FirstOrDefaultAsync();
                return skill;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }



        public async Task<int> insertAsync(SkillorKPI data)
        {
            var Skill = new SkillorKPI();
            try
            {
                if (data != null)
                {
                    Skill = new SkillorKPI
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserCreated,
                        Description = data.Description,
                        Name = data.Name,
                        Type=data.Type

                    };
                    await _context.SkillorKPIs.AddAsync(Skill);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Skill.ID;
        }

        public Task<int> insertListAsync(List<SkillorKPI> data)
        {
            throw new NotImplementedException();
        }



        public async Task<int> updateAsync(SkillorKPI data)
        {
            var skill = await _context.SkillorKPIs.FindAsync(data.ID);
            try
            {
                if (skill != null)
                {


                    skill.DateCreated = DateTime.Now;
                    skill.Description = data.Description;
                    skill.Name = data.Name;
                    skill.DateModified = DateTime.Now;
                    skill.UserModified = data.UserCreated;
                    skill.StaffNumberWithSkillorKPI = data.StaffNumberWithSkillorKPI;

                    _context.SkillorKPIs.Update(skill);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return skill.ID;
        }
    }
}
