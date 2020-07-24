using CRMSystem.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Infrastructure
{
    public class RoleRepo : IRepo<Role>
    {
        public readonly TContext _context;
        public RoleRepo(TContext context)
        {
            _context = context;
        }

        public Task deleteAllAsync(List<Role> data)
        {
            throw new NotImplementedException();
        }

        public async Task  deleteAsync(int ID)
        {
            try
            {
                var role = await _context.Roles.FindAsync(ID);
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Role>> getAllAsync()
        {
            try
            {
                var role = await _context.Roles.Include(y => y.Privileges).ToListAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<Role> getAsync(int ID)
        {
            try
            {
                var role = await _context.Roles.Include(y => y.Privileges).Where(x => x.ID == ID).FirstOrDefaultAsync<Role>();
                return role;
            }
            catch(Exception ex)
            {
                throw ex;

            }
            
               
        }

        public Task<List<Role>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(Role data)
        {
            var role = new Role();
            try
            {
                role = new Role
                {
                    Code = data.Code,
                    DateCreated=DateTime.Now,
                    UserCreated=data.UserCreated,
                    Name=data.Name
                };

                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return role.ID;
        }

        public Task<int> insertListAsync(List<Role> data)
        {
            throw new NotImplementedException();
        }

        public Task<int> updateAsync(Role data)
        {
            throw new NotImplementedException();
        }
    }
}
