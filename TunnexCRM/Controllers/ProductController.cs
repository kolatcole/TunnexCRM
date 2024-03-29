﻿using System;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IProductRepo _pRepo;
        public ProductController(IProductService service, IProductRepo pRepo)
        {
            _service = service;
            _pRepo = pRepo;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SaveProduct")]
        public async Task<IActionResult> Save(Product data)
        {
             var result=await _service.insertProductAsync(data);
             return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("SaveMultipleProducts")]
        public async Task<IActionResult> SaveMultipleProducts(List<Product> data)
        {
            var result = await _service.insertMultipleProductsAsync(data);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> Update(Product data)
        {
            var result = await _service.updateProductAsync(data);
            return Ok(result);

        }
        [HttpGet("GetProductByID/{ID}")]
        public async Task<IActionResult> GetProductByID(int ID)
        {
            var result = await _service.GetProduct(ID);
            return Ok(result);
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts(string type="datedesc")
        {
            var result = await _service.GetAllProducts(type);
            return Ok(result);
        }
        [HttpGet("GetAllAvailableProducts")]
        public async Task<IActionResult> GetAllAvailableProducts()
        {
            var result = await _service.GetAllAvailableProducts();
            return Ok(result);
        }

        [HttpGet("BestSellingProducts")]
        public async Task<IActionResult> BestSellingProducts()
        {
            var result = await _pRepo.GetTopSellingProducts();
            return Ok(result);
        }

        [HttpPost("DeleteProduct/{ID}")]
        public async Task<IActionResult> Delete(int ID)
        {

            await _service.DeleteProductAsync(ID);
            return Ok();
        }
    }
}