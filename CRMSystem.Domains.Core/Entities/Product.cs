using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRMSystem.Domains
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }

        public int TotalSold { get; set; }
        public int StockLevel { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
