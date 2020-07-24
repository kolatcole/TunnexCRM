using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class MessageRepo : IRepo<Message>
    {
        private readonly TContext _context;
        public MessageRepo(TContext context)
        {
            _context = context;
        }

        public Task deleteAllAsync(List<Message> data)
        {
            throw new NotImplementedException();
        }

        public async Task  deleteAsync(int ID)
        {
            try
            {
                var messages = await _context.Messages.Where(x=>x.LeadID==ID).ToListAsync();
                _context.Messages.RemoveRange(messages);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Message>> getAllAsync()
        {
            var messages = await _context.Messages.ToListAsync();
            return messages;
        }

        public Task<List<Message>> getAllByIDAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Message> getAsync(int ID)
        {
            var message = await _context.Messages.Where(x => x.ID == ID).FirstOrDefaultAsync();
            return message;
        }

        public Task<List<Message>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> insertAsync(Message data)
        {
            var message = new Message();
            try
            {
                if (data != null)
                {
                    message = new Message
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserCreated,
                        Summary=data.Summary,
                        Type=data.Type,
                        LeadID=data.LeadID
                    };
                    await _context.Messages.AddAsync(message);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return message.ID;
        }

        public async Task<int> insertListAsync(List<Message> data)
        {
            int ID = 0;   
            try 
            {
                await _context.Messages.AddRangeAsync(data);
                ID=await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ID;
        }

        public async Task<int> updateAsync(Message data)
        {
            var message = await _context.Messages.Where(x => x.ID == data.ID).SingleOrDefaultAsync();
            try
            {
                if (message != null)
                {
                    message.Type = data.Type;
                    message.Summary = data.Summary;
                    message.UserModified = data.UserModified;
                    message.DateModified = data.DateModified;


                    _context.Messages.Update(message);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return message.ID;
        }
    }

}
