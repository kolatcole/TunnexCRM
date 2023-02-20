using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Domains
{
    public class Quotation:BaseEntity
    {
        public List<QuotProduct> QuotProducts { get; set; }

        public int CustomerID { get; set; }
    }
}
