using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRMSystem.Domains
{
    public class Cart:BaseEntity
    {
        public string Code { get; set; }

        public List<Item> Items { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Amount { get; set; }
        //public int InvoiceID { get; set; }
        //public int SaleID { get; set; }

        // use to track either supply or sale so as to increase or reduce product qty as required
        // purchaase has the value of true, sale has false
        public bool TransactionType { get; set; }

    }
}
