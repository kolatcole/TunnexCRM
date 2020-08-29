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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Post(Role data)
        {
            var result = await _service.SaveRoleWithPrivileges(data);
            return Ok(result);
        
        }
        [HttpGet("GetRoleByID/{ID}")]
        public async Task<IActionResult> Get(int ID)
        {
            var result = await _service.GetRoleWithPrivileges(ID);
            return Ok(result);
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _service.GetAllRolesAsync();
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost("DeleteRole/{ID}")]
        public async Task<IActionResult> Delete(int ID)
        {

            await _service.DeleteRoleAsync(ID);
            return Ok();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("UpdateRole")]
        public async Task<IActionResult> UpdateRole(Role data)
        {

            await _service.UpdateRoleAsync(data);
            return Ok();
        }
    }


}