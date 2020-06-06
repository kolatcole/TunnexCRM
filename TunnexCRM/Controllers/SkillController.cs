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
    public class SkillController : ControllerBase
    {

        private readonly IRepo<Skill> _repo;
        public SkillController(IRepo<Skill> repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public async Task<IActionResult> Save(Skill data)
        {
            var result = await _repo.insertAsync(data);
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Skill data)
        {
            var result = await _repo.updateAsync(data);
            return Ok(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllSkills")]
        public async Task<IActionResult> GetAllSkills()
        {
            var result = await _repo.getAllAsync();
            return Ok(result);
        }
        [HttpGet("GetSkillByID/{ID}")]
        public async Task<IActionResult> GetAllSkills(int ID)
        {
            var result = await _repo.getAsync(ID);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost("DeleteSkill/ID")]
        public async Task<IActionResult> Delete(int ID)
        {

            await _repo.deleteAsync(ID);
            return Ok();
        }
    }
}
