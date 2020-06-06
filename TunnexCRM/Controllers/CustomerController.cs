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
    public class CustomerController : ControllerBase
    {
        private readonly IRepo<Customer> _repo;
        private readonly ICustomerRepo _cRepo;
        public CustomerController(IRepo<Customer> repo, ICustomerRepo cRepo)
        {
            _repo = repo;
            _cRepo = cRepo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SaveCustomer")]
        public async Task<IActionResult> Save(Customer data)
        {
            var result = await _repo.insertAsync(data);
            return Ok(result);
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("UpdateCustomer")]
        public async Task<IActionResult> Update(Customer data)
        {
            var result = await _repo.updateAsync(data);
            return Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("GetCustomerByID/{ID}")]
        public async Task<IActionResult> GetCustomer(int ID)
        {
            var result = await _repo.getAsync(ID);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _repo.getAllAsync();
            return Ok(result);
        }

        [HttpGet("MostFrequentCustomers")]
        public async Task<IActionResult> MostFrequent()
        {
            var result = await _cRepo.MostFrequentCustomer();
            return Ok(result);
        }

        [HttpDelete("DeleteCustomer/{ID}")]
        public async Task<IActionResult> Delete(int ID)
        {

            await _repo.deleteAsync(ID);
            return Ok();
        }
    }
}