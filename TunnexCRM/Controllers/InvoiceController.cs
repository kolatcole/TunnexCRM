using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Presentation.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _service;
        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Debtor's List
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDebtorsList/{startDate}/{endDate}")]
        public async Task<IActionResult> GetDebtorsList(string startDate,string endDate)
        {

            

            DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sDate);
            DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime eDate);

            if (sDate <= DateTime.MinValue)
                sDate = DateTime.Now.StartOfDay();

            if (eDate <= DateTime.MinValue)
                eDate = DateTime.Now.EndOfDay();
            else
                eDate = eDate.EndOfDay();

            var result = await _service.getDebtorInvoice(sDate,eDate);
            return Ok(result);
        }

        [HttpGet("GetLastNumber")]
        public async Task<IActionResult> GetLastNumber()
        {
            var result = await _service.getLastAsync();
            return Ok(result);
        }

        [HttpGet("GetInvoice/{InvNumber}/{customerID}")]
        public async Task<IActionResult> GetInvoice(string InvNumber, int customerID)
        {
            var result = await _service.GetInvoiceByNumber(InvNumber, customerID);
            return Ok(result);
        }
    }
}