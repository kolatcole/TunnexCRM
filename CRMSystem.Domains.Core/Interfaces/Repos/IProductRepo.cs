using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IProductRepo
    {
        Task<List<Product>> GetTopSellingProducts();
        Task<List<Product>> getAllAvailableAsync();
    }
}
