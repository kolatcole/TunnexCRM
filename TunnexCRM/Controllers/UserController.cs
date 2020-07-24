using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Domains;
using CRMSystem.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SaveUser")]
        public async Task<IActionResult> Save(User data)
        {
            var result = await _service.SaveUserAsync(data);
            if (result > 0)
                return Ok(result);
            return NotFound();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            var result = await _service.GetUserByNameandPasswordAsync(username,password);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> Update(User data)
        {
            var result = await _service.updateUserAsync(data);
            if (result > 0)
                return Ok(result);
            return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("GetUserByID/{ID}")]
        public async Task<IActionResult> GetUserByID(int ID)
        {
            var result = await _service.getUserAsync(ID);
            return Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _service.getAllUsersAsync();
            return Ok(result);

        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        [HttpPost("DeleteUser/{ID}")]
        public async Task<IActionResult> Delete(int ID)
        {

            await _service.deleteUserAsync(ID);
            return Ok();
        }
    }
}