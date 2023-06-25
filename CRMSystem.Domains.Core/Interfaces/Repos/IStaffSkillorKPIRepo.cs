using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IStaffSkillorKPIRepo
    {
        Task<List<StaffSkillorKPI>> getStaffSkillsByStaffID(int staffID, string startDate, string endDate);

        Task<StaffSkillorKPI> getStaffSkillorKpiByStaffIDandSkillorKpi(int staffID,int skillOrKpi);

        Task<List<StaffSkillorKPI>> getAllAsync(string startDate, string endDate);
    }
}
