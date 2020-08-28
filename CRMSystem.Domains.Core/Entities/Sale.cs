using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRMSystem.Domains
{
    public class Sale: BaseEntity
    {
        public int CustomerID { get; set; }

        // invoice is supposed to be auto generated when a sale is made
        //was included initially 
        [JsonIgnore]
        public int InvoiceID { get; set; }
        [JsonIgnore]
        public int CartID { get; set; }
        public Invoice Invoice { get; set; }
        public Cart Cart { get; set; }
        public List<Payment> Payment { get; set; }
        // use to track either supply or sale so as to increase or reduce product qty as required
        // purchaase has the value of true, sale has false
       // public bool TransactionType { get; set; }



    }
}
