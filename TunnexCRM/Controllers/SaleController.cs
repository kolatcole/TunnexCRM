using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _service;
        public SaleController(ISaleService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        
        [HttpPost("SaveSale")]
        public async Task<IActionResult> Post(Sale data)
        {
            var result = await _service.Save(data);
            return Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("GetSaleByID/{ID}")]
        public async Task<IActionResult> Get(int ID)
        {
            var result = await _service.GetSaleByIDAsync(ID);
            return Ok(result);

        }
        [HttpGet("GetAllSales")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllSalesAsync();
            return Ok(result);

        }

        /// <summary>
        /// Fetch All Customer Sales Record
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [HttpGet("GetSalesByCustomerID/{customerID}")]
        public async Task<IActionResult> GetSalesByCustomerID(int customerID)
        {
            var result = await _service.GetSalesByCustomerIDAsync(customerID);
            return Ok(result);

        }

        [HttpGet("GetSingleDaySales/{date}")]
        public async Task<IActionResult> GetSingleDaySales(string date)
        {


            
            DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime edate);

           
            var result = await _service.GetSingleDaySalesAsync(edate);

            return Ok(result);

        }
    }
}