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
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SaveCustomer")]
        public async Task<IActionResult> Save(Customer data)
        {
            var result = await _service.SaveCustomerAsync(data);
            return Ok(result);
        
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SaveMultipleCustomers")]
        public async Task<IActionResult> SaveMultipleCustomers(List<Customer> data)
        {
            var result = await _service.SaveMultipleCustomersAsync(data);
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
            var result = await _service.UpdateCustomerAsync(data);
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
            var result = await _service.getCustomerByID(ID);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _service.getAllCustomers();
            return Ok(result);
        }

        [HttpGet("MostFrequentCustomers")]
        public async Task<IActionResult> MostFrequent()
        {
            var result = await _service.MostFrequentCustomerAsync();
            return Ok(result);
        }

        [HttpPost("DeleteCustomer/{ID}")]
        public async Task<IActionResult> Delete(int ID)
        {

            await _service.DeleteCustomerAsync(ID);
            return Ok();
        }
    }
}