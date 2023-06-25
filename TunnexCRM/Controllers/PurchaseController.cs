using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Domains;
using CRMSystem.Domains.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Presentation.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {

        private readonly IPurchaseService _service;
        public PurchaseController(IPurchaseService service) => _service = service;


        /// <summary>
        /// You must specify ExchangeCurrency
        /// You must enter the NairaEquivalent
        /// Enter invoice no from supplier
        /// Enter productID in item
        /// Enter each item quantity purchased
        /// Hardcode transactionType to true
        /// Enter a unit amount in foreign currency for each item in the cart
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SavePurchase")]
        public async Task<IActionResult> SavePurchase(Purchase data)
        {

            var result = await _service.MakePurchase(data);
            return Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        [HttpGet("GetPurchaseByInvoice/{invoiceNo}")]
        public async Task<IActionResult> GetPurchaseByInvoice(string invoiceNo)
        {
            var result = await _service.GetPurchaseByInvoiceNo(invoiceNo);
            return Ok(result);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("GetPurchasedByDate/{startDate}/{endDate}")]
        public async Task<IActionResult> GetPurchasedByDate(int supplierID = 0, string startDate = "0", string endDate = "0")
        {

            var result = await _service.GetPurchasesReportByDate(supplierID, startDate, endDate);

            return Ok(result);

        }

    }
}
