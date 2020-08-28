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
    public class SupplierController : ControllerBase
    {
        private readonly IRepo<Supplier> _repo;
        public SupplierController(IRepo<Supplier> repo) => _repo = repo;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SaveSupplier")]
        public async Task<IActionResult> Save(Supplier data)
        {
            var result = await _repo.insertAsync(data);
            return Ok(result);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SaveMultipleSuppliers")]
        public async Task<IActionResult> SaveMultipleSuppliers(List<Supplier> data)
        {
            var result = await _repo.insertListAsync(data);
            return Ok(result);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("UpdateSupplier")]
        public async Task<IActionResult> Update(Supplier data)
        {
            var result = await _repo.updateAsync(data);
            return Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("GetSupplierByID/{ID}")]
        public async Task<IActionResult> GetSupplier(int ID)
        {
            var result = await _repo.getAsync(ID);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllSuppliers")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var result = await _repo.getAllAsync();
            return Ok(result);
        }


        [HttpPost("DeleteSupplier/{ID}")]
        public async Task<IActionResult> Delete(int ID)
        {

            await _repo.deleteAsync(ID);
            return Ok();
        }
    }
}