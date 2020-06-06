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
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _service;
        public LeadController(ILeadService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SaveLead")]
        public async Task<IActionResult> SaveLead(Lead data)
        {
            var result = await _service.SaveLeadAsync(data);
            return Ok(result);

        }

        [HttpGet("GetLeadByID/{ID}")]
        public async Task<IActionResult> GetLead(int ID)
        {
            var result = await _service.getLeadByID(ID);
            return Ok(result);

        }
        [HttpGet("GetAllLeads")]
        public async Task<IActionResult> GetAllLeads()
        {
            var result = await _service.getAllLeads();
            return Ok(result);

        }

        [HttpPost("ConvertLeadToCustomer/{ID}")]
        public async Task<IActionResult> ConvertLeadToCustomer(int ID)
        {
            var result = await _service.ConvertLeadtoCustomerAsync(ID);
            return Ok(result);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete("DeleteLead/ID")]
        public async Task<IActionResult> Delete(int ID)
        {

            await _service.DeleteLeadAsync(ID);
            return Ok();
        }
    }
}