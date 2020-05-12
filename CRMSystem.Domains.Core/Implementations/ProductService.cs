﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class ProductService : IProductService
    {
        private readonly IRepo<Product> _pRepo;
        private readonly IRepo<Price> _pcrepo;
        public ProductService(IRepo<Price> pcrepo, IRepo<Product> pRepo)
        {
            _pRepo = pRepo;
            _pcrepo = pcrepo;
        }

        public async Task<int> insertProductAsync(Product data)
        {

            //int PID = await _pcrepo.insertAsync(data.Price);

            //// save a product
           // data.PriceID = PID;
            int PRID = await _pRepo.insertAsync(data);

            return PRID;


        }
        public async Task<int> updateProductAsync(Product data)
        {
            var pid = await _pRepo.updateAsync(data);

            //if (pid > 0)
            //    await _pcrepo.updateAsync(data.Price);
            return pid;
            
        }
        public async Task<List<Product>> GetAllProducts()
        {
            var result = await _pRepo.getAllAsync();
            return result;
        }
        public async Task<Product> GetProduct(int ID)
        {
            var result = await _pRepo.getAsync(ID);
            return result;
        
        }
    }
}
