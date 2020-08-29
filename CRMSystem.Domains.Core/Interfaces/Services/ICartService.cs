using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface ICartService
    {
        Task<int> SaveCart(Cart data);

        Task<int> SaveProformaCart(Cart data);
    }
}
