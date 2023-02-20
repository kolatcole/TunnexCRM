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

        private readonly IRepo<SkillorKPI> _repo;
        private readonly ISkillorKPIRepo _skRepo;
        public SkillController(IRepo<SkillorKPI> repo,ISkillorKPIRepo skRepo)
        {
            _repo = repo;
            _skRepo = skRepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public async Task<IActionResult> Save(SkillorKPI data)
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
        public async Task<IActionResult> Update(SkillorKPI data)
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllKpis")]
        public async Task<IActionResult> GetAllKpis()
        {
            var result = await _skRepo.getAllKPIs();
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
