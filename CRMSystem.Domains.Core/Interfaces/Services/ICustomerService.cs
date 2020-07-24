using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface ICustomerService
    {
        Task<int> SaveCustomerAsync(Customer data);
        Task<int> SaveMultipleCustomersAsync(List<Customer> data);
        Task DeleteCustomerAsync(int ID);
        Task<int> UpdateCustomerAsync(Customer data);
        Task<Customer> getCustomerByID(int ID);
        Task<List<Customer>> getAllCustomers();
        Task<List<Customer>> MostFrequentCustomerAsync();
    }
}
