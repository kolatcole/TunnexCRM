using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class StaffSkillorKPIRepo : IRepo<StaffSkillorKPI>, IStaffSkillorKPIRepo
    {
        public readonly TContext _context;
        public StaffSkillorKPIRepo(TContext context)
        {
            _context = context;

        }

        public Task deleteAllAsync(List<StaffSkillorKPI> data)
        {
            throw new NotImplementedException();
        }

        public Task deleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StaffSkillorKPI>> getAllStaffSkillAsync()
        {
            try
            {
                var skill = await _context.StaffSkillorKPIs.Include(x => x.Assessments).ToListAsync();
                return skill;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<List<StaffSkillorKPI>> getAllStaffKPIAsync()
        //{
        //    try
        //    {
        //        var skill = await _context.StaffSkillorKPIs.Include(x => x.Assessments).ToListAsync();
        //        return skill;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        //public async Task<StaffSkillorKPI> getAsync(int ID, string startDate, string endDate)
        //{

        //    DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sdate);
        //    DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime edate);

        //    if (sdate <= DateTime.MinValue)
        //        sdate = DateTime.Now.StartOfDay();

        //    if (edate <= DateTime.MinValue)
        //        edate = DateTime.Now.EndOfDay();
        //    else
        //        edate = edate.EndOfDay();

            




        //    try
        //    {
        //        //var StaffSkill = await _context.StaffSkillorKPIs.Include(x => x.Assessments).Where(x => x.ID == ID).FirstOrDefaultAsync();


        //        // filter by STAFFID only, if that's what was given

        //        if ((startDate == "0" || endDate == "0") && ID > 0)
        //        {
        //            var StaffSkill = await _context.StaffSkillorKPIs.Where(x => x.ID == ID).FirstOrDefaultAsync();

        //            var assessment = await _context.Assessments.Where(x=>x.StaffSkillorKPIID == ID).ToListAsync();

        //             StaffSkill.Assessments = assessment;
        //        }






        //        // filter by dates alone if customerID is not given

        //        else if (startDate != "0" || endDate != "0")
        //        {
        //            var StaffSkill = await _context.StaffSkillorKPIs.Where(x => x.ID == ID).FirstOrDefaultAsync();

        //            var assessment = await _context.Assessments.Where(x => x.DateCreated > sdate &&
        //                                        x.DateCreated <= edate && x.StaffSkillorKPIID == ID).ToListAsync();
        //            StaffSkill.Assessments = assessment;
        //        }




        //        // filter by all given parameters

        //        else if (startDate != "0" && endDate != "0" && ID > 0)
        //        {
        //            var StaffSkill = await _context.StaffSkillorKPIs.Where(x => x.ID == ID).FirstOrDefaultAsync();

        //            var assessment = await _context.Assessments.Where(x => x.DateCreated > sdate &&
        //                                        x.DateCreated <= edate && x.StaffSkillorKPIID == ID).ToListAsync();
        //            StaffSkill.Assessments = assessment;
        //        }



        //        // get without any parameter
        //        else
        //            return  await _context.StaffSkillorKPIs.Include(x => x.Assessments).FirstOrDefaultAsync();





                

        //        return StaffSkill;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}




        public async Task<StaffSkillorKPI> getAsync(int ID)
        {

            try
            {
                var StaffSkill = await _context.StaffSkillorKPIs.Include(x => x.Assessments).Where(x => x.ID == ID).FirstOrDefaultAsync();



                return StaffSkill;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StaffSkillorKPI> getStaffSkillorKpiByStaffIDandSkillorKpi(int staffID, int skillOrKpiId)
        {
            try
            {
                var StaffSkill = await _context.StaffSkillorKPIs.Include(x => x.Assessments).Where
                                                (x => x.StaffID == staffID && x.SkillorKPIID==skillOrKpiId).FirstOrDefaultAsync();
                return StaffSkill;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<StaffSkillorKPI>> getStaffSkillsByStaffID(int staffID, string startDate, string endDate)
        {
            DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sdate);
            DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime edate);

            if (sdate <= DateTime.MinValue)
                sdate = DateTime.Now.StartOfDay();

            if (edate <= DateTime.MinValue)
                edate = DateTime.Now.EndOfDay();
            else
                edate = edate.EndOfDay();

            try
            {
                var staffskills = new List<StaffSkillorKPI>();

                if(startDate!="0" && endDate!="0")
                {
                    staffskills = await _context.StaffSkillorKPIs.Where(x => x.StaffID == staffID).ToListAsync();

                    foreach (var stk in staffskills)
                    {
                        var assessments = await _context.Assessments.Where(x => x.StaffSkillorKPIID == stk.ID &&
                                                (x.DateCreated >= sdate && x.DateCreated <= edate)).ToListAsync();

                        if (assessments != null)
                            stk.Assessments = assessments;

                    }
                }
                else
                {
                    staffskills = await _context.StaffSkillorKPIs.Include(x=>x.Assessments).Where(x => x.StaffID == staffID).ToListAsync();
                }


                return staffskills;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<List<StaffSkillorKPI>> getStaffKPIByStaffID(int staffID)
        //{
        //    try
        //    {
        //        var StaffSkills = await _context.StaffSkillorKPIs.Include(x => x.Assessments).Where(x => x.StaffID == staffID && x.t).ToListAsync();
        //        return StaffSkills;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public async Task<int> insertAsync(StaffSkillorKPI data)
        {

            // CHHECK IF STAFF HAS BEEN ENROLLED FOR THIS SKILL

            var skill = await _context.StaffSkillorKPIs.Where(x => x.SkillorKPIID == data.SkillorKPIID && x.StaffID == data.StaffID)
                                                       .FirstOrDefaultAsync();

            if (skill != null)
                return 0;

            var StaffSkill = new StaffSkillorKPI();
            try
            {
                if (data != null)
                {
                    StaffSkill = new StaffSkillorKPI
                    {
                        SkillorKPIID = data.SkillorKPIID,
                        StaffID = data.StaffID,
                        SupervisorID = data.SupervisorID,
                        CompetencyValue = data.CompetencyValue


                    };
                    await _context.StaffSkillorKPIs.AddAsync(StaffSkill);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StaffSkill.ID;
        }

        public Task<int> insertListAsync(List<StaffSkillorKPI> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> updateAsync(StaffSkillorKPI data)
        {
            var StaffSkill = await _context.StaffSkillorKPIs.FindAsync(data.ID);
            try
            {
                if (StaffSkill != null)
                {

                    StaffSkill.SkillorKPIID = data.SkillorKPIID;
                    StaffSkill.StaffID = data.StaffID;
                    StaffSkill.SupervisorID = data.SupervisorID;
                    StaffSkill.CompetencyValue = data.CompetencyValue;


                    _context.StaffSkillorKPIs.Update(StaffSkill);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StaffSkill.ID;
        }

        public async Task<List<StaffSkillorKPI>> getAllAsync(string startDate,string endDate)
        {
            DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sdate);
            DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime edate);

            if (sdate <= DateTime.MinValue)
                sdate = DateTime.Now.StartOfDay();

            if (edate <= DateTime.MinValue)
                edate = DateTime.Now.EndOfDay();
            else
                edate = edate.EndOfDay();




            var staffSkills = new List<StaffSkillorKPI>();

            try
            {
                //var StaffSkill = await _context.StaffSkillorKPIs.Include(x => x.Assessments).Where(x => x.ID == ID).FirstOrDefaultAsync();


                // filter by dates, if that's what was given

                if (startDate != "0" || endDate != "0")
                {
                    staffSkills = await _context.StaffSkillorKPIs.ToListAsync();

                    foreach (var stk in staffSkills)
                    {


                        var assessment = await _context.Assessments.Where(x => x.DateCreated >= sdate &&
                                                x.DateCreated <= edate && x.StaffSkillorKPIID == stk.ID).ToListAsync();

                        if (assessment != null)
                            stk.Assessments = assessment;
                    }

                }


                // get without any parameter
                else
                    staffSkills = await _context.StaffSkillorKPIs.Include(x => x.Assessments).ToListAsync();


                return staffSkills;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task<List<StaffSkillorKPI>> getAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
