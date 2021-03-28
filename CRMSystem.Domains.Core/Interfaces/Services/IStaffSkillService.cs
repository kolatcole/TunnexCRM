using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IStaffSkillService
    {
        Task<int> EnrolAllStaffAsync(int id);
        Task<int> SaveStaffSkill(StaffSkillorKPI data);
        Task<int> UpdateStaffSkillAsync(StaffSkillorKPI data);
        Task<StaffSkillorKPI> GetStaffSkillByIDAsync(int ID);
        Task<List<StaffSkillorKPI>> GetAllStaffSkillsAsync(string sdate, string edate);
        Task<List<StaffSkillorKPI>> GetAllStaffKpisAsync(string sdate, string edate);
        Task<StaffSkillorKPICompetency> getStaffSkillsByStaffIDAsync(int staffID,string sdate="0",string edate="0");
        Task<StaffSkillorKPICompetency> getStaffKpisByStaffIDAsync(int staffID, string sdate="0", string edate="0");
        Task<List<StaffSkillorKPI>> getStaffKpiorSkillByNameAsync(string name);
    }
}
