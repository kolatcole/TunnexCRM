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
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public async Task<IActionResult> Save(StaffSkill data)
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
        public async Task<IActionResult> Update(StaffSkill data)
        {
            var result = await _service.UpdateStaffSkillAsync(data);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllStaffSkill")]
        public async Task<IActionResult> GetAllStaffSkill()
        {

            var result = await _service.GetAllStaffSkillsAsync();
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

    }
}
