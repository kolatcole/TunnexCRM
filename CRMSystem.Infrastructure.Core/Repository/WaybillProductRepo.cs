//using CRMSystem.Domains;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CRMSystem.Infrastructure
//{
//    public class WaybillProductRepo : IRepo<WaybillProduct>
//    {
//        private readonly TContext _context;
//        public WaybillProductRepo(TContext context)
//        {

//            _context = context;
//        }

//        public Task deleteAllAsync(List<WaybillProduct> data)
//        {
//            throw new NotImplementedException();
//        }

//        public Task deleteAsync(int ID)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<WaybillProduct>> getAllAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<WaybillProduct> getAsync(int ID)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<int> insertAsync(WaybillProduct data)
//        {
//            try
//            {
//                var product = new WaybillProduct
//                {
//                   ProductID=data.ProductID,
//                   WaybillID=data.WaybillID
//                };

//                await _context.WaybillProducts.AddAsync(product);
//                await _context.SaveChangesAsync();

//                return product.ID;


//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<int> insertListAsync(List<WaybillProduct> data)
//        {
//            try
//            {
                

//                await _context.WaybillProducts.AddRangeAsync(data);
//                await _context.SaveChangesAsync();

//                return data.Count;


//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public Task<int> updateAsync(WaybillProduct data)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}