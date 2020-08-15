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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _pService;
        public PaymentController(IPaymentService pService)
        {
            _pService = pService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("MakePayment")]
        public async Task<IActionResult> Post(Payment data)
        {
            var result = await _pService.PayAsync(data);
            return Ok(result);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        [HttpGet("GetFreePayments")]
        public async Task<IActionResult> GetFreePayments(int customerID=0,string sDate="0",string eDate="0")
        {
            var result = await _pService.GetFreePayments(customerID, sDate, eDate);
            return Ok(result);

        }
        
    }
}