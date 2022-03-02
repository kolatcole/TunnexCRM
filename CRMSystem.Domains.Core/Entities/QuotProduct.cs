using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Domains
{
    public class QuotProduct
    {
        public int ID { get; set; }

        public int QuotationID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
