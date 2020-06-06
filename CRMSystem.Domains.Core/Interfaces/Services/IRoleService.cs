using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IRoleService
    {
        Task<int> SaveRoleWithPrivileges(Role data);
        Task<Role> GetRoleWithPrivileges(int ID);
        Task<List<Role>> GetAllRolesAsync();
        Task DeleteRoleAsync(int ID);
    }
}
