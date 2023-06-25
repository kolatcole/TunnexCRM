using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics.Contracts;

namespace CRMSystem.Infrastructure
{
    public class UserRepo : IRepo<User>,IUserRepo
    {
        private readonly TContext _context;
        public UserRepo(TContext context)
        {
            _context = context;
        }

        public Task deleteAllAsync(List<User> data)
        {
            throw new NotImplementedException();
        }

        public async Task  deleteAsync(int ID)
        {
            try
            {
                var user = await _context.AppUsers.FindAsync(ID);
                _context.AppUsers.Remove(user);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<User>> getAllAsync()
        {
            try
            {
                var users = await _context.AppUsers.ToListAsync();

                foreach(var user in users)
                {
                    var role = await _context.Roles.Where(x => x.ID == user.RoleID).SingleOrDefaultAsync();
                    user.Role = role;

                    var privileges = await _context.Privileges.Where(x => x.RoleID == role.ID).ToListAsync();
                    role.Privileges = privileges;

                }



                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<User> getAsync(int ID)
        {
            try
            {
                var user =  await _context.AppUsers.Where(x => x.ID == ID).FirstOrDefaultAsync();
                
                var role = await _context.Roles.Where(x => x.ID == user.RoleID).SingleOrDefaultAsync();
                user.Role = role;

                var privileges = await _context.Privileges.Where(x => x.RoleID == role.ID).ToListAsync();
                role.Privileges = privileges;
                


                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public Task<List<User>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByNameandPassword(string username, string password)
        {
            User user = null;   
            try 
            {
               user = await _context.AppUsers.Where(x => x.Username == username && x.Password == password).FirstAsync();
            }
            catch(Exception ex)
            {
                throw ex;

            }
            return user;
        }

        public async Task<int> insertAsync(User data)
        {
            var user = new User();
            try
            {
                if (data != null)
                {
                    user = new User
                    {
                        Post=data.Post,
                        Gender=data.Gender,
                        Image=data.Image,
                        Name=data.Name,
                        Phone=data.Phone,
                        DateCreated=DateTime.Now,
                        Email=data.Email,
                        Password=data.Password,
                        Username=data.Username,
                        RoleID=data.RoleID
                    };
                    await _context.AppUsers.AddAsync(user);
                    await _context.SaveChangesAsync();
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user.ID;
        }

        public Task<int> insertListAsync(List<User> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> updateAsync(User data)
        {
            var newUser = await _context.AppUsers.FindAsync(data.ID);
            try
            {
                
                if(data.Image!=null || data.Image!=String.Empty) newUser.Image = data.Image;
                if (data.Name != null || data.Name != String.Empty) newUser.Name = data.Name;
                if (data.Phone != null || data.Phone != String.Empty) newUser.Phone = data.Phone;
                if (data.Post != null || data.Post != String.Empty) newUser.Post = data.Post;
                newUser.DateModified = DateTime.Now;
                if (data.Gender != null || data.Gender != String.Empty) newUser.Gender = data.Gender;
                if (data.Username != null || data.Username != String.Empty) newUser.Username = data.Username;
                if (data.Email != null || data.Email != String.Empty) newUser.Email = data.Email;
                if (data.Image != null || data.Image != String.Empty) newUser.RoleID = data.RoleID;

                _context.Update(newUser);
                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newUser.ID;
        }
    }
}
