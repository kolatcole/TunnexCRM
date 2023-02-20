using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Domains
{
    public class Supplier:BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
