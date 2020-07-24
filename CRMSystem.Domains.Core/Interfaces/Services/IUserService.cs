using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IUserService
    {
        Task<int> SaveUserAsync(User data);

        Task deleteUserAsync(int ID);

        Task<List<User>> getAllUsersAsync();
        
        Task<User> getUserAsync(int ID);
        
        Task<List<User>> getUserByCustomerIDAsync(int customerID);
        
        Task<User> GetUserByNameandPasswordAsync(string username, string password);
        
        
        Task<int> insertUsersListAsync(List<User> data);
        
        Task<int> updateUserAsync(User data);
    }
}
