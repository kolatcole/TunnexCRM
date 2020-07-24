using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IStaffSkillRepo
    {
        Task<List<StaffSkill>> getStaffSkillsByStaffID(int staffID);
    }
}
