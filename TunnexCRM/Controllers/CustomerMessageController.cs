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
    public class CustomerMessageController : ControllerBase
    {
        private readonly IRepo<CustomerMessage> _cRepo;
        public CustomerMessageController(IRepo<CustomerMessage> cRepo)
        {
            _cRepo = cRepo;
        }

        [HttpPost("SaveCustomerMessage")]
        public async Task<IActionResult> SaveCustomerMessage(CustomerMessage data)
        {
            var result = await _cRepo.insertAsync(data);
            return Ok(result);

        }
    }
}
