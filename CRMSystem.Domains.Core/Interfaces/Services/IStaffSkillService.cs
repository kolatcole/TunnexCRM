using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IStaffSkillService
    {
        Task<int> SaveStaffSkill(StaffSkillorKPI data);
        Task<int> UpdateStaffSkillAsync(StaffSkillorKPI data);
        Task<StaffSkillorKPI> GetStaffSkillByIDAsync(int ID);
        Task<List<StaffSkillorKPI>> GetAllStaffSkillsAsync();
        Task<List<StaffSkillorKPI>> GetAllStaffKpisAsync();
        Task<StaffSkillorKPICompetency> getStaffSkillsByStaffIDAsync(int staffID);
        Task<StaffSkillorKPICompetency> getStaffKpisByStaffIDAsync(int staffID);
        Task<List<StaffSkillorKPI>> getStaffKpiorSkillByNameAsync(string name);
    }
}
