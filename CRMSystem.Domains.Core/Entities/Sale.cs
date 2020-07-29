using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Domains
{
    public class Sale: BaseEntity
    {
        public int CustomerID { get; set; }

        // invoice is supposed to be auto generated when a sale is made

       // [JsonIgnore]
       //was included initially public int InvoiceID { get; set; }
        [JsonIgnore]
        public int CartID { get; set; }
        public Invoice Invoice { get; set; }
        public Cart Cart { get; set; }
        public List<Payment> Payment { get; set; }



    }
}
