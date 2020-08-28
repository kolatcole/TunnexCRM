using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CRMSystem.Domains
{
    public class Purchase:BaseEntity
    {
        public int SupplierID { get; set; }

        
        public string InvoiceNo { get; set; }
        [JsonIgnore]
        public int CartID { get; set; }

        public Cart Cart { get; set; }

        public string ExchangeCurrency { get; set; }

        public decimal NairaEquivalent { get; set; }

        [JsonIgnore]
        public decimal TotalAmountForeign { get; set; }

        [JsonIgnore]
        public decimal TotalAmountNaira { get; set; }

        // use to track either supply or sale so as to increase or reduce product qty as required
        // purchaase has the value of true, sale has false
        //public bool TransactionType { get; set; }

    }
}
