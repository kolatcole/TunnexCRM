using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class PrivilegeRepo:IRepo<Privilege>
    {
        public readonly TContext _context;
        public PrivilegeRepo(TContext context)
        {
            _context = context;
        }

        public async Task  deleteAsync(int ID)
        {
            try
            {
                var privileges = await _context.Privileges.Where(x => x.RoleID == ID).ToListAsync();
                _context.Privileges.RemoveRange(privileges);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task deleteAllAsync(List<Privilege> data)
        {
            try
            {
                _context.Privileges.RemoveRange(data);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<Privilege>> getAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Privilege> getAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Privilege>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(Privilege data)
        {
            var privilege = new Privilege();
            try
            {
                privilege = new Privilege
                {
                    Code = data.Code,
                    DateCreated = DateTime.Now,
                    UserCreated = data.UserCreated,
                    Name = data.Name,
                    Read=data.Read,
                    Write=data.Write,
                    RoleID=data.RoleID
                };

                await _context.Privileges.AddAsync(privilege);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return privilege.ID;
        }

        public async Task<int> insertListAsync(List<Privilege> data)
        {
            int ID = 0;
            
            try
            {


                await _context.Privileges.AddRangeAsync(data);
               ID= await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ID;
        }

        public Task<int> updateAsync(Privilege data)
        {
            throw new NotImplementedException();
        }
    }
}
