using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IStaffSkillorKPICompetencyService
    {
        Task<List<StaffSkillorKPICompetency>> GetAllOverAllSkill(string startdate,string enddate);

        Task<List<StaffSkillorKPICompetency>> GetAllOverAllKpi(string startdate, string enddate);
    }
}
