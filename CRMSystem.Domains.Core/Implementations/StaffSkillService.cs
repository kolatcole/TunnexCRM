using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class StaffSkillService : IStaffSkillService
    {
        private readonly IRepo<Assessment> _aRepo;
        private readonly IRepo<StaffSkillorKPI> _sRepo;
        private readonly IRepo<SkillorKPI> _skRepo;
        private readonly IStaffSkillorKPIRepo _sskRepo;

        public StaffSkillService(IRepo<Assessment> aRepo,IRepo<StaffSkillorKPI> sRepo, IRepo<SkillorKPI> skRepo, IStaffSkillorKPIRepo sskRepo)
        {

            _sRepo = sRepo;
            _aRepo = aRepo;
            _skRepo = skRepo;
            _sskRepo = sskRepo;
        }
        

        public async Task<int> SaveStaffSkill(StaffSkillorKPI data)
        {
            var SID = await _sRepo.insertAsync(data);

            // get skill and increment numberOfStaff by 1

            var skill = await _skRepo.getAsync(data.SkillorKPIID);

            skill.StaffNumberWithSkillorKPI += 1;

            await _skRepo.updateAsync(skill);




            return SID;

        }

        public async Task<int> UpdateStaffSkillAsync(StaffSkillorKPI data)
        {
            var skill = await _sRepo.getAsync(data.ID);

            if (data.Assessments != null)
            {
                var assessments = new List<Assessment>();
                var comp = 0;
                foreach (var assessment in data.Assessments)
                {
                    comp += assessment.SAS;
                    assessment.StaffSkillorKPIID = data.ID;
                    await _aRepo.insertAsync(assessment);
                }
                data.CompetencyValue = (comp*2) * 10;
                data.CompetencyValue = (skill.CompetencyValue + data.CompetencyValue) / (skill.Assessments.Count);
            }

            
            var SID = await _sRepo.updateAsync(data);
            return SID;
        }

        public async Task<StaffSkillorKPI> GetStaffSkillByIDAsync(int ID) 
        {
            var skill = await _sRepo.getAsync(ID);
            return skill;
        }

        public async Task<List<StaffSkillorKPI>> GetAllStaffSkillsAsync()
        {
            
            var staffskillorKpis = await _sRepo.getAllAsync();

            var newStaffskillorKpis = new List<StaffSkillorKPI>();

            foreach (var staffskillorKpi in staffskillorKpis)
            {
                var skill = await _skRepo.getAsync(staffskillorKpi.SkillorKPIID);

                if (skill.Type == "skill")
                    newStaffskillorKpis.Add(staffskillorKpi);

            }

            return newStaffskillorKpis;
        }
        public async Task<List<StaffSkillorKPI>> GetAllStaffKpisAsync()
        {

            var staffskillorKpis = await _sRepo.getAllAsync();

            var newStaffskillorKpis = new List<StaffSkillorKPI>();

            foreach (var staffskillorKpi in staffskillorKpis)
            {
                var skill = await _skRepo.getAsync(staffskillorKpi.SkillorKPIID);

                if (skill.Type == "kpi")
                    newStaffskillorKpis.Add(staffskillorKpi);

            }

            return newStaffskillorKpis;
        }
        public async Task<StaffSkillorKPICompetency> getStaffSkillsByStaffIDAsync(int staffID)
        {
            //var skills = await _sskRepo.getStaffSkillsByStaffID(staffID);
            //return skills;

            var staffskillorKpis = await _sskRepo.getStaffSkillsByStaffID(staffID);
            var overAll = new StaffSkillorKPICompetency();

            var newStaffskillorKpis = new List<StaffSkillorKPI>();
            int competenceCount = 0;
            decimal Average = 0.00M;
            decimal TempCompetency = 0.00M;
            foreach (var staffskillorKpi in staffskillorKpis)
            {
                var skill = await _skRepo.getAsync(staffskillorKpi.SkillorKPIID);

                if (skill.Type == "skill")
                {
                    competenceCount++;
                    TempCompetency += staffskillorKpi.CompetencyValue;
                    Average /= competenceCount;
                    newStaffskillorKpis.Add(staffskillorKpi);

                    overAll.StaffId = staffID;
                    overAll.AllSkillsOrKpis = newStaffskillorKpis;
                    

                }
                

            }

            if (TempCompetency > 0)
                overAll.OverallCompetence = TempCompetency/competenceCount;

            return overAll;











            //var staffskillorKpis = await _sskRepo.getStaffSkillsByStaffID(staffID);

            //var newStaffskillorKpis = new List<StaffSkillorKPI>();
            //int competenceCount = 0;
            //foreach (var staffskillorKpi in staffskillorKpis)
            //{
            //    var skill = await _skRepo.getAsync(staffskillorKpi.SkillorKPIID);

            //    if (skill.Type == "skill")
            //    {
            //        competenceCount++;
            //        staffskillorKpi.Average += staffskillorKpi.CompetencyValue;
            //        staffskillorKpi.Average /= competenceCount;
            //        newStaffskillorKpis.Add(staffskillorKpi);
            //    }


            //}

            //return newStaffskillorKpis;
        }
        public async Task<StaffSkillorKPICompetency> getStaffKpisByStaffIDAsync(int staffID)
        {


            //var staffskillorKpis = await _sskRepo.getStaffSkillsByStaffID(staffID);

            //var newStaffskillorKpis = new List<StaffSkillorKPI>();

            //foreach (var staffskillorKpi in staffskillorKpis)
            //{
            //    var skill = await _skRepo.getAsync(staffskillorKpi.SkillorKPIID);

            //    if (skill.Type == "kpi")
            //        newStaffskillorKpis.Add(staffskillorKpi);

            //}

            //return newStaffskillorKpis;



            var staffskillorKpis = await _sskRepo.getStaffSkillsByStaffID(staffID);

            var overAll = new StaffSkillorKPICompetency();

            var newStaffskillorKpis = new List<StaffSkillorKPI>();
            int competenceCount = 0;
            decimal Average = 0.00M;
            decimal TempCompetency = 0.00M;
            foreach (var staffskillorKpi in staffskillorKpis)
            {
                var skill = await _skRepo.getAsync(staffskillorKpi.SkillorKPIID);

                if (skill.Type == "kpi")
                {
                    competenceCount++;
                    TempCompetency += staffskillorKpi.CompetencyValue;
                    Average /= competenceCount;
                    newStaffskillorKpis.Add(staffskillorKpi);

                    overAll.StaffId = staffID;
                    overAll.AllSkillsOrKpis = newStaffskillorKpis;


                }


            }

            if(TempCompetency>0)
                overAll.OverallCompetence = TempCompetency / competenceCount;

            return overAll;
        }

        public async Task<List<StaffSkillorKPI>> getStaffKpiorSkillByNameAsync(string name)
        {


            var staffskillorKpis = await _sRepo.getAllAsync();

            var newStaffskillorKpis = new List<StaffSkillorKPI>();

            foreach (var staffskillorKpi in staffskillorKpis)
            {
                var skill = await _skRepo.getAsync(staffskillorKpi.SkillorKPIID);

                if (skill.Name == name)
                    newStaffskillorKpis.Add(staffskillorKpi);

            }

            return newStaffskillorKpis;
        }
    }
}
