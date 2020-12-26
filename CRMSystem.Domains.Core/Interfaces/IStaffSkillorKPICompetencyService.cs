using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IStaffSkillorKPICompetencyService
    {
        Task<List<StaffSkillorKPICompetency>> GetAllOverAllSkill();

        Task<List<StaffSkillorKPICompetency>> GetAllOverAllKpi();
    }
}
