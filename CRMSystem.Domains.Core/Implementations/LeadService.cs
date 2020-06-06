using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace CRMSystem.Domains
{
    public class LeadService : ILeadService
    {
        private readonly IRepo<Lead> _lRepo;
        private readonly IRepo<Message> _mRepo;
        private readonly IRepo<Customer> _cRepo;
        public LeadService(IRepo<Lead> lRepo,IRepo<Message> mRepo, IRepo<Customer> cRepo)
        {
            _lRepo = lRepo;
            _mRepo = mRepo;
            _cRepo = cRepo;
        }

        public async Task<int> ConvertLeadtoCustomerAsync(int ID)
        {
            var lead = await _lRepo.getAsync(ID);
            var customer = new Customer
            {
                TotalSales=0,
                Address= lead.Address,
                DateCreated=DateTime.Now,
                Email= lead.Email,
                FirstName= lead.FirstName,
                LastName= lead.LastName,
                Gender= lead.Gender,
                Image= lead.Image,
                Phone= lead.Phone,
                UserCreated= lead.UserCreated
            };

            lead.isCustomer = true;
            await _lRepo.updateAsync(lead);
            var result=await _cRepo.insertAsync(customer);
            return result;

        }

        public async Task<List<Lead>> getAllLeads()
        {
            var result = await _lRepo.getAllAsync();
            return result;
        }

        public async Task<Lead> getLeadByID(int ID)
        {
            var result = await _lRepo.getAsync(ID);
            return result;
        }

        public async Task<int> SaveLeadAsync(Lead data)
        {
            int LID = await _lRepo.insertAsync(data);

            List<Message> messages = new List<Message>();
            if (data.Message!=null)
            {
                
                foreach (var message in data.Message)
                {
                    message.LeadID = LID;
                    messages.Add(message);
                }
            }

            await _mRepo.insertListAsync(messages);
            return LID;

        }

        public Task<int> UpdateLeadAsync(Lead data)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteLeadAsync(int ID)
        {
            await _mRepo.deleteAsync(ID);
            await _lRepo.deleteAsync(ID);
        }
    }
}
