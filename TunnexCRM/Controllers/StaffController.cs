using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IRepo<Staff> _repo;
        public StaffController(IRepo<Staff> repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public async Task<IActionResult> Save(Staff data)
        {

            var result = await _repo.insertAsync(data);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("UpdateStaff")]
        public async Task<IActionResult> Update(Staff data)
        {

            var result = await _repo.updateAsync(data);
            return Ok(result);
        }

        [HttpGet("GetStaffByID/{ID}")]
        public async Task<IActionResult> GetStaffByID(int ID)
        {
            var result = await _repo.getAsync(ID);
            return Ok(result);
        }
        [HttpGet("GetAllStaff")]
        public async Task<IActionResult> GetAllStaff()
        {
            var result = await _repo.getAllAsync();
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost("DeleteStaff/{ID}")]
        public async Task<IActionResult> Delete(int ID)
        {

            await _repo.deleteAsync(ID);
            return Ok();
        }
    }
}
