using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Domains
{
    public class Invoice:BaseEntity
    {
        public int CustomerID { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string InvoiceNo { get; set; }

        public Cart Cart { get; set; }

        public int CartID { get; set; }

        public decimal Amount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }

        public bool IsPaid { get; set; }

        public User Cashier { get; set; }

        public string ExtData { get; set; }

        public decimal Tax { get; set; }

        public decimal TaxPercent { get; set; }

        public string TaxName { get; set; }

        public bool TaxInclusive { get; set; }

        public decimal Discount { get; set; }
    }
}
