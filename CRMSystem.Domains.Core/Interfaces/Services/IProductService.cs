using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IProductService
    {
        Task<int> insertProductAsync(Product data);
        Task<int> updateProductAsync(Product data);
        Task<List<Product>> GetAllProducts(string type);
        Task<Product> GetProduct(int ID);
        Task DeleteProductAsync(int ID);
        Task<int> insertMultipleProductsAsync(List<Product> data);
        Task<List<Product>> GetAllAvailableProducts();
    }
}
