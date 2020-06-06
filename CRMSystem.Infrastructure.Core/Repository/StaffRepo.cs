using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class StaffRepo : IRepo<Staff>
    {
        public readonly TContext _context;
        public StaffRepo(TContext context)
        {
            _context = context;

        }

        public Task deleteAllAsync(List<Staff> data)
        {
            throw new NotImplementedException();
        }

        public async Task  deleteAsync(int ID)
        {
            try
            {
                var staff = await _context.Staffs.FindAsync(ID);
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Staff>> getAllAsync()
        {
            try
            {
                var staff = await _context.Staffs.Include(x=>x.Qualifications).ToListAsync();
                return staff;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Staff> getAsync(int ID)
        {
            try
            {
                var staff = await _context.Staffs.Include(x => x.Qualifications).Where(x => x.ID == ID).FirstOrDefaultAsync();
                return staff;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<int> insertAsync(Staff data)
        {
            var staff = new Staff();
            try
            {
                if (data != null)
                {
                    staff = new Staff
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserModified,
                        FirstName = data.FirstName,
                        Address = data.Address,
                        Email = data.Email,
                        Gender = data.Gender,
                        HEL = data.HEL,
                        DateofBirth = data.DateofBirth,
                         Image = data.Image,
                        LastName = data.LastName,
                         Phone = data.Phone
                        
                    };
                    await _context.Staffs.AddAsync(staff);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return staff.ID;
        }

        public Task<int> insertListAsync(List<Staff> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> updateAsync(Staff data)
        {
            var staff = await _context.Staffs.FindAsync(data.ID);
            try
            {
                if (staff != null)
                {


                    staff.Image = data.Image;
                    staff.FirstName = data.FirstName;
                    staff.Phone = data.Phone;
                    staff.LastName = data.LastName;
                    staff.DateModified = DateTime.Now;
                    staff.UserModified = data.UserModified;
                    staff.Gender = data.Gender;
                    staff.Email = data.Email;
                    staff.Address = data.Address;
                    staff.HEL = data.HEL;
                    

                    _context.Staffs.Update(staff);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return staff.ID;
        }
    }
}
