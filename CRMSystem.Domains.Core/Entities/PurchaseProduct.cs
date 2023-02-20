using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRMSystem.Domains
{
    public class PurchaseProduct
    {

        public int ID { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public int cartID { get; set; }

        public int productID { get; set; }
    }
}
