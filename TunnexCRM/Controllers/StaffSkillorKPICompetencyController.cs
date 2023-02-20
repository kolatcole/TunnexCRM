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
        public async Task<IActionResult> GetAllbyStaffSkill(string sdate="0",string edate="0")
        {
            var result = await _service.GetAllOverAllSkill(sdate,edate);
            return Ok(result);
        }

        [HttpGet("GetAllbyStaffKPI")]
        public async Task<IActionResult> GetAllbyStaffKPI(string sdate = "0", string edate = "0")
        {
            var result = await _service.GetAllOverAllKpi(sdate,edate);
            return Ok(result);
        }
    }
}
 