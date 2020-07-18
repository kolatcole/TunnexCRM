using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class UserService : IUserService
    {

        private readonly IRepo<User> _repo;
        private readonly IUserRepo _uRepo;
        private readonly IRoleService _roleService;

        public UserService(IRepo<User> repo, IUserRepo uRepo, IRoleService roleService)
        {
            _repo = repo;
            _uRepo = uRepo;
            _roleService = roleService;
        }

        public async Task deleteUserAsync(int ID)
        {
           await _repo.deleteAsync(ID);
        }

        public async Task<List<User>> getAllUsersAsync()
        {
            var users = await _repo.getAllAsync();
            return users;
        }

        public async Task<User> getUserAsync(int ID)
        {
            var user = await _repo.getAsync(ID);
            return user;
        }

        public Task<List<User>> getUserByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByNameandPasswordAsync(string username, string password)
        {
            var user = await _uRepo.GetUserByNameandPassword(username, password);
            return user;
        }

        public Task<int> insertUsersListAsync(List<User> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveUserAsync(User data)
        {
            // save role

            int RID= await _roleService.SaveRoleWithPrivileges(data.Role);
            data.RoleID = RID;




            int ID = await _repo.insertAsync(data);
            return ID;
        }

        public async Task<int> updateUserAsync(User data)
        {
            int ID = await _repo.updateAsync(data);
            return ID;
        }
    }
}
