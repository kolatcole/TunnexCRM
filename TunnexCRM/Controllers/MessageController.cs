using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Domains;
using CRMSystem.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Presentation.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IRepo<Message> _mRepo;
        public MessageController(IRepo<Message> mRepo)
        {
            _mRepo = mRepo;
        }

        [HttpPost("SaveMessage")]
        public async Task<IActionResult> SaveMessage(Message data)
        {
            var result = await _mRepo.insertAsync(data);
            return Ok(result);
        
        }

    }
}
