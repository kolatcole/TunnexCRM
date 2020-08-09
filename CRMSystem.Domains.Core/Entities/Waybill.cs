using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Domains
{
    public class Waybill
    {
        public int ID { get; set; }

        public DateTime DateCreated { get; set; }

        public int UserCreated { get; set; }

        public string InvoiceNo { get; set; }

        public List<WaybillProduct> WaybillProducts { get; set; }
    }
}
