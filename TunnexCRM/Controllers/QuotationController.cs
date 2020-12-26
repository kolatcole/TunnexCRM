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
    public class QuotationController : ControllerBase
    {

        public readonly IQuotationService _service;

        public QuotationController(IQuotationService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("CreateQuotation")]
        public async Task<IActionResult> CreateQuotation(Quotation data)
        {
            var result = await _service.SaveQuotationAndProducts(data);
            return Ok(result);

        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("GetQuotationByCustomerStartandEndDate/{startDate}/{endDate}")]
        public async Task<IActionResult> GetQuotationByCustomerStartandEndDate(int customerID=0,string startDate="0", string endDate = "0")
        {

            var result = await _service.GetAllByCustomerAndDates(customerID, startDate, endDate);
            return Ok(result); 


        }

        [HttpPost("ChangeQuoteToSale")]
        public async Task<IActionResult> ChangeQuoteToSale(Quotation data, string Lpo,bool ToDeliver,decimal DeliveryFee,decimal discount )
        {

            var result = await _service.ChangeQuoteToSale(data, Lpo, ToDeliver, DeliveryFee, discount);
            return Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        //[HttpGet("GetQuotationByCustomer/{customerID}")]
        //public async Task<IActionResult> GetQuotationByCustomer(int customerID)
        //{

        //    var result = await _repo.getAllByCustomerAsync(customerID);
        //    return Ok(result);


        //}






    }
}
