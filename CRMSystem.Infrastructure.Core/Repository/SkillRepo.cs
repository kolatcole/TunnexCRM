using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class SkillRepo : IRepo<Skill>
    {
        private readonly TContext _context;
        public SkillRepo(TContext context)
        {

            _context = context;
        }

        public Task deleteAllAsync(List<Skill> data)
        {
            throw new NotImplementedException();
        }

        public async Task  deleteAsync(int ID)
        {
            try
            {
                var skill = await _context.Skills.FindAsync(ID);
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Skill>> getAllAsync()
        {
            try
            {
                var skill = await _context.Skills.ToListAsync();
                return skill;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }


        public async Task<Skill> getAsync(int ID)
        {
            try
            {
                var skill = await _context.Skills.Where(x => x.ID == ID).FirstOrDefaultAsync();
                return skill;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }



        public async Task<int> insertAsync(Skill data)
        {
            var Skill = new Skill();
            try
            {
                if (data != null)
                {
                    Skill = new Skill
                    {
                        DateCreated = DateTime.Now,
                        UserCreated=data.UserCreated,
                        Description=data.Description,
                        Name=data.Name

                    };
                    await _context.Skills.AddAsync(Skill);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Skill.ID;
        }

        public Task<int> insertListAsync(List<Skill> data)
        {
            throw new NotImplementedException();
        }



        public async Task<int> updateAsync(Skill data)
        {
            var skill = await _context.Skills.FindAsync(data.ID);
            try
            {
                if (skill != null)
                {


                    skill.DateCreated = DateTime.Now;
                    skill.Description = data.Description;
                    skill.Name = data.Name;
                    skill.DateModified = DateTime.Now;
                    skill.UserModified = data.UserCreated;
                    skill.StaffNumberWithSkill = data.StaffNumberWithSkill;

                    _context.Skills.Update(skill);
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
