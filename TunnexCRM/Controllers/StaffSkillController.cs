using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Presentation.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffSkillController : ControllerBase
    {
        private readonly IStaffSkillService _service;
        public StaffSkillController(IStaffSkillService service)
        {
            _service = service;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns></returns>
        [HttpPost("EnrolAllStaff/{skillId}")]
        public async Task<IActionResult> EnrolAllStaff(int skillId)
        {
            var result = await _service.EnrolAllStaffAsync(skillId);
            return Ok(result);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public async Task<IActionResult> Save(StaffSkillorKPI data)
        {
            var result = await _service.SaveStaffSkill(data);
            return Ok(result);
        }


        /// <summary>
        /// Optional, can be used to add a new assessment, pass the staffskill id and the assessment score (SAS)
        /// Can also be used to update staffskill too
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<IActionResult> Update(StaffSkillorKPI data)
        {
            var result = await _service.UpdateStaffSkillAsync(data);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllStaffSkill")]
        public async Task<IActionResult> GetAllStaffSkill(string sdate="0",string edate="0")
        {

            var result = await _service.GetAllStaffSkillsAsync(sdate,edate);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllStaffKpi")]
        public async Task<IActionResult> GetAllStaffKpi(string sdate = "0", string edate = "0")
        {

            var result = await _service.GetAllStaffKpisAsync(sdate,edate);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("GetStaffSkillByID/{ID}")]
        public async Task<IActionResult> GetStaffSkillByID(int ID)
        {

            var result = await _service.GetStaffSkillByIDAsync(ID);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffID"></param>
        /// <returns></returns>

        [HttpGet("GetStaffSkillsByStaffID/{staffID}")]
        public async Task<IActionResult> GetStaffSkillsByStaffID(int staffID)
        {

            var result = await _service.getStaffSkillsByStaffIDAsync(staffID);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffID"></param>
        /// <returns></returns>

        [HttpGet("GetStaffKpisByStaffID/{staffID}")]
        public async Task<IActionResult> GetStaffKpisByStaffID(int staffID)
        {

            var result = await _service.getStaffKpisByStaffIDAsync(staffID);
            return Ok(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("GetStaffSkillorKpiByName/{name}")]
        public async Task<IActionResult> GetStaffSkillorKpiByName(string name)
        {

            var result = await _service.getStaffKpiorSkillByNameAsync(name);
            return Ok(result);
        }


    }
}
