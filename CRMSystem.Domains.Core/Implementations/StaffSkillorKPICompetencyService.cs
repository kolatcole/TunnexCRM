using System;
using System.Collections.Generic;
using System.Globalization;
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

        //working
        //public async Task<List<StaffSkillorKPICompetency>> GetAllOverAllSkill()
        //{

            

        //    var skills = await _service.GetAllStaffSkillsAsync();

        //    var overAll = new List<StaffSkillorKPICompetency>();



        //    foreach (var skill in skills)
        //    {
        //        if (!overAll.Exists(x => x.StaffId == skill.StaffID))
        //        {
        //            var oneStaff = await _service.getStaffSkillsByStaffIDAsync(skill.StaffID);



        //            //  oneStaff.OverallCompetence = oneStaff.AllSkillsOrKpis.FindAll(x => x.CompetencyValue);
        //            overAll.Add(oneStaff);
        //        }


        //    }


        //    //foreach(var skill in skills)
        //    //{
        //    //    if (!overAll.Exists(x => x.StaffId==skill.StaffID))
        //    //    {
        //    //        var oneStaff = new StaffSkillorKPICompetency
        //    //        {
        //    //            StaffId = skill.StaffID,
        //    //            AllSkillsOrKpis = await _service.getStaffSkillsByStaffIDAsync(skill.StaffID)

        //    //        };


        //    //        oneStaff.OverallCompetence=oneStaff.AllSkillsOrKpis.FindAll()

        //    //      //  oneStaff.OverallCompetence = oneStaff.AllSkillsOrKpis.FindAll(x => x.CompetencyValue);
        //    //        overAll.Add(oneStaff);
        //    //    }


        //    //}
        //    return overAll;
        //}

        public async Task<List<StaffSkillorKPICompetency>> GetAllOverAllSkill(string startdate,string enddate)
        {


            var overAll = new List<StaffSkillorKPICompetency>();


            var skills = await _service.GetAllStaffSkillsAsync(startdate, enddate);
            // filter by date, if that's what was given

            foreach (var skill in skills)
            {
                if (!overAll.Exists(x => x.StaffId == skill.StaffID))
                {
                    var oneStaff = await _service.getStaffSkillsByStaffIDAsync(skill.StaffID, startdate, enddate);



                    //  oneStaff.OverallCompetence = oneStaff.AllSkillsOrKpis.FindAll(x => x.CompetencyValue);
                    overAll.Add(oneStaff);
                }


            }


            return overAll;




            //pend
            //var skills = await _service.GetAllStaffSkillsAsync();
            // var skills = new List<StaffSkillorKPI>();

            //var overAll = new List<StaffSkillorKPICompetency>();



            //foreach (var skill in skills)
            //{
            //    if (!overAll.Exists(x => x.StaffId == skill.StaffID))
            //    {
            //        var oneStaff = await _service.getStaffSkillsByStaffIDAsync(skill.StaffID);



            //      //  oneStaff.OverallCompetence = oneStaff.AllSkillsOrKpis.FindAll(x => x.CompetencyValue);
            //        overAll.Add(oneStaff);
            //    }


            //}


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
        }




        //public async Task<List<StaffSkillorKPICompetency>> GetAllOverAllSkill(string startdate, string enddate)
        //{


        //    var overAll = new List<StaffSkillorKPICompetency>();


        //    var skills = await _service.GetAllStaffSkillsAsync(startdate, enddate);
        //    // filter by date, if that's what was given

        //    foreach (var skill in skills)
        //    {
        //        if (!overAll.Exists(x => x.StaffId == skill.StaffID))
        //        {
        //            var oneStaff = await _service.getStaffSkillsByStaffIDAsync(skill.StaffID, startdate, enddate);



        //            //  oneStaff.OverallCompetence = oneStaff.AllSkillsOrKpis.FindAll(x => x.CompetencyValue);
        //            overAll.Add(oneStaff);
        //        }


        //    }


        //    return overAll;

        //}
        public async Task<List<StaffSkillorKPICompetency>> GetAllOverAllKpi(string startdate, string enddate)
        {
            ////pend
            ////var skills = await _service.GetAllStaffSkillsAsync();
            //var skills = new List<StaffSkillorKPI>();
            //var overAll = new List<StaffSkillorKPICompetency>();



            //foreach (var skill in skills)
            //{
            //    if (!overAll.Exists(x => x.StaffId == skill.StaffID))
            //    {
            //        var oneStaff = await _service.getStaffKpisByStaffIDAsync(skill.StaffID);


            //        overAll.Add(oneStaff);
            //    }


            //}

            //return overAll;

            var overAll = new List<StaffSkillorKPICompetency>();


            var skills = await _service.GetAllStaffKpisAsync(startdate, enddate);
            // filter by date, if that's what was given

            foreach (var skill in skills)
            {
                if (!overAll.Exists(x => x.StaffId == skill.StaffID))
                {
                    var oneStaff = await _service.getStaffKpisByStaffIDAsync(skill.StaffID, startdate, enddate);



                    //  oneStaff.OverallCompetence = oneStaff.AllSkillsOrKpis.FindAll(x => x.CompetencyValue);
                    overAll.Add(oneStaff);
                }


            }


            return overAll;
        }
    }
}
