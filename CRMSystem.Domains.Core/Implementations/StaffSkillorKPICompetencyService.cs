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

        public async Task<List<StaffSkillorKPICompetency>> GetAllOverAllSkill()
        {

            //List<Invoice> invoices;
            //DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sdate);
            //DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime edate);

            //if (sdate <= DateTime.MinValue)
            //    sdate = DateTime.Now.StartOfDay();

            //if (edate <= DateTime.MinValue)
            //    edate = DateTime.Now.EndOfDay();
            //else
            //    edate = edate.EndOfDay();

            //// filter by customerID only, if that's what was given

            //if ((startDate == "0" || endDate == "0") && customerID > 0)
            //{
            //    return invoices = await _iRepo.getProformaByCustomerIDAsync(customerID);
            //}






            //// filter by dates alone if customerID is not given

            //else if (customerID < 1 && (startDate != "0" || endDate != "0"))
            //{
            //    return invoices = await _iRepo.getAllProformaByDateAsync(sdate, edate);
            //}




            //// filter by all given parameters

            //else if (startDate != "0" && endDate != "0" && customerID > 0)
            //{
            //    return invoices = await _iRepo.getAllProformaByDateandCustomerAsync(customerID, sdate, edate);
            //}



            //// get without any parameter
            //else
            //    return invoices = await _iRepo.getAllProformaAsync();

            
            
            
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
