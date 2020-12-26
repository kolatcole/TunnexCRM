using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains.Core.Implementations
{
    public class StaffSkillorKPICompetencyService: IStaffSkillorKPICompetencyService
    {

        private readonly IStaffSkillService _service;

        public StaffSkillorKPICompetencyService(IStaffSkillService service)
        {
            _service = service;
        }

        public async Task<List<StaffSkillorKPICompetency>> GetAllOverAllSkill()
        {
            var skills = await _service.GetAllStaffSkillsAsync();

            var overAll = new List<StaffSkillorKPICompetency>();



            foreach (var skill in skills)
            {
                if (!overAll.Exists(x => x.StaffId == skill.StaffID))
                {
                    var oneStaff = await _service.getStaffSkillsByStaffIDAsync(skill.StaffID);
                    


                  //  oneStaff.OverallCompetence = oneStaff.AllSkillsOrKpis.FindAll(x => x.CompetencyValue);
                    overAll.Add(oneStaff);
                }


            }


            //foreach(var skill in skills)
            //{
            //    if (!overAll.Exists(x => x.StaffId==skill.StaffID))
            //    {
            //        var oneStaff = new StaffSkillorKPICompetency
            //        {
            //            StaffId = skill.StaffID,
            //            AllSkillsOrKpis = await _service.getStaffSkillsByStaffIDAsync(skill.StaffID)

            //        };


            //        oneStaff.OverallCompetence=oneStaff.AllSkillsOrKpis.FindAll()

            //      //  oneStaff.OverallCompetence = oneStaff.AllSkillsOrKpis.FindAll(x => x.CompetencyValue);
            //        overAll.Add(oneStaff);
            //    }


            //}
            return overAll;
        }

        public async Task<List<StaffSkillorKPICompetency>> GetAllOverAllKpi()
        {
            var skills = await _service.GetAllStaffSkillsAsync();

            var overAll = new List<StaffSkillorKPICompetency>();



            foreach (var skill in skills)
            {
                if (!overAll.Exists(x => x.StaffId == skill.StaffID))
                {
                    var oneStaff = await _service.getStaffKpisByStaffIDAsync(skill.StaffID);


                    overAll.Add(oneStaff);
                }


            }

            return overAll;
        }
    }
}
