using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class RoleService : IRoleService
    {
        public readonly IRepo<Role> _Rrepo;
        public readonly IRepo<Privilege> _Prepo;

        public RoleService(IRepo<Role> Rrepo, IRepo<Privilege> Prepo) 
        {
            _Rrepo = Rrepo;
            _Prepo = Prepo;
        
        }
        public async Task<int> SaveRoleWithPrivileges(Role data)
        {
            int RID = await _Rrepo.insertAsync(data);

            List<Privilege> privileges = new List<Privilege>();
            foreach (var privilege in data.Privileges)
            {
                privilege.RoleID = RID;
                privileges.Add(privilege);

            }

            await _Prepo.insertListAsync(privileges);
            return RID;
        }
        public async Task<Role> GetRoleWithPrivileges(int ID)
        {

            var result = await _Rrepo.getAsync(ID);
            return result;
        }
        public async Task<List<Role>> GetAllRolesAsync()
        {

            var result = await _Rrepo.getAllAsync();
            return result;
        }
        public async Task DeleteRoleAsync(int ID)
        {

            await _Prepo.deleteAsync(ID);

            await _Rrepo.deleteAsync(ID);

            
        }

        public async Task UpdateRoleAsync(Role data)
        {
            var RID=await _Rrepo.updateAsync(data);

            if (data.Privileges != null)
            {
                foreach (var priv in data.Privileges)
                {
                    await _Prepo.updateAsync(priv);
                }

            }
        }
    }
}
