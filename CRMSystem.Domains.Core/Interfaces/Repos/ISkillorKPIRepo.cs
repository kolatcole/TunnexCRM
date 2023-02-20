using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface ISkillorKPIRepo
    {
        Task<List<SkillorKPI>> getAllKPIs();
    }
}
