﻿using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CRMSystem.Infrastructure
{
    public class UserRepo : IRepo<User>,IUserRepo
    {
        private readonly TContext _context;
        public UserRepo(TContext context)
        {
            _context = context;
        }
        public Task<int> deleteAsync(User data)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> getAllAsync()
        {
            try
            {
                var user = await _context.AppUsers.ToListAsync();
                return user;
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
                        Username=data.Username
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
                if (newUser!=null)
                {
                    newUser.Image = data.Image;
                    newUser.Name = data.Name;
                    newUser.Phone = data.Phone;
                    newUser.Post = data.Post;
                    newUser.DateModified = DateTime.Now;
                    newUser.Gender = data.Gender;
                    newUser.Username = data.Username;
                    newUser.Password = data.Password;
                    newUser.Email = data.Email;
                    newUser.DateModified = DateTime.Now;

                    _context.Update(newUser);
                     await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newUser.ID;
        }
    }
}
