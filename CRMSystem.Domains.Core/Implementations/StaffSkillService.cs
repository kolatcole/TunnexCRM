using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class StaffSkillService : IStaffSkillService
    {
        private readonly IRepo<Assessment> _aRepo;
        private readonly IRepo<StaffSkill> _sRepo;
        private readonly IRepo<Skill> _skRepo;
        private readonly IStaffSkillRepo _sskRepo;

        public StaffSkillService(IRepo<Assessment> aRepo,IRepo<StaffSkill> sRepo, IRepo<Skill> skRepo, IStaffSkillRepo sskRepo)
        {

            _sRepo = sRepo;
            _aRepo = aRepo;
            _skRepo = skRepo;
            _sskRepo = sskRepo;
        }
        

        public async Task<int> SaveStaffSkill(StaffSkill data)
        {
            var SID = await _sRepo.insertAsync(data);

            // get skill and increment numberOfStaff by 1

            var skill = await _skRepo.getAsync(data.SkillID);

            skill.StaffNumberWithSkill += 1;

            await _skRepo.updateAsync(skill);




            return SID;

        }

        public async Task<int> UpdateStaffSkillAsync(StaffSkill data)
        {
            var skill = await _sRepo.getAsync(data.ID);

            if (data.Assessments != null)
            {
                var assessments = new List<Assessment>();
                var comp = 0;
                foreach (var assessment in data.Assessments)
                {
                    comp += assessment.SAS;
                    assessment.StaffSkillID = data.ID;
                    await _aRepo.insertAsync(assessment);
                }
                data.CompetencyValue = (comp*2) * 10;
                data.CompetencyValue = (skill.CompetencyValue + data.CompetencyValue) / (skill.Assessments.Count);
            }

            
            var SID = await _sRepo.updateAsync(data);
            return SID;
        }

        public async Task<StaffSkill> GetStaffSkillByIDAsync(int ID) 
        {
            var skill = await _sRepo.getAsync(ID);
            return skill;
        }

        public async Task<List<StaffSkill>> GetAllStaffSkillsAsync()
        {
            var skills = await _sRepo.getAllAsync();
            return skills;
        }

        public async Task<List<StaffSkill>> getStaffSkillsByStaffIDAsync(int staffID)
        {
            var skills = await _sskRepo.getStaffSkillsByStaffID(staffID);
            return skills;
        }
    }
}
