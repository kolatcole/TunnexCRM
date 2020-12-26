using CRMSystem.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMSystem.Presentation.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffSkillorKPICompetencyController : ControllerBase
    {

        private readonly IStaffSkillorKPICompetencyService _service;

        public StaffSkillorKPICompetencyController(IStaffSkillorKPICompetencyService service)
        {
            _service = service;
        }

        [HttpGet("GetAllbyStaffSkill")]
        public async Task<IActionResult> GetAllbyStaffSkill()
        {
            var result = await _service.GetAllOverAllSkill();
            return Ok(result);
        }

        [HttpGet("GetAllbyStaffKPI")]
        public async Task<IActionResult> GetAllbyStaffKPI()
        {
            var result = await _service.GetAllOverAllKpi();
            return Ok(result);
        }
    }
}
 