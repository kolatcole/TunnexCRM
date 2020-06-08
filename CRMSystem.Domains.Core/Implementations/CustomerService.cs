using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepo<Customer> _cRepo;
        private readonly IRepo<CustomerMessage> _cmRepo;
        private readonly ICustomerRepo _c1Repo;
        public CustomerService(IRepo<Customer> cRepo,IRepo<CustomerMessage> cmRepo, ICustomerRepo c1Repo)
        {
            _cmRepo = cmRepo;
            _cRepo = cRepo;
            _c1Repo = c1Repo;
        }
        public async Task DeleteCustomerAsync(int ID)
        {
            await _cmRepo.deleteAsync(ID);
            await _cRepo.deleteAsync(ID);
        }

        public async Task<List<Customer>> getAllCustomers()
        {
            var result = await _cRepo.getAllAsync();
            return result;
            
        }

        public async Task<Customer> getCustomerByID(int ID)
        {
            var result = await _cRepo.getAsync(ID);
            return result;
        }

        public async Task<int> SaveCustomerAsync(Customer data)
        {
            var result = await _cRepo.insertAsync(data);
            return result;
        }

        public async Task<int> UpdateCustomerAsync(Customer data)
        {
            var result = await _cRepo.updateAsync(data);
            return result;
        }
        public async Task<List<Customer>> MostFrequentCustomerAsync()
        {
            var result = await _c1Repo.MostFrequentCustomer();
            return result;
        }
    }
}
