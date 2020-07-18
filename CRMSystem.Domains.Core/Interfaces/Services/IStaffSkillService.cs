using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IStaffSkillService
    {
        Task<int> SaveStaffSkill(StaffSkill data);
        Task<int> UpdateStaffSkillAsync(StaffSkill data);
        Task<StaffSkill> GetStaffSkillByIDAsync(int ID);
        Task<List<StaffSkill>> GetAllStaffSkillsAsync();
        Task<List<StaffSkill>> getStaffSkillsByStaffIDAsync(int staffID);
    }
}
